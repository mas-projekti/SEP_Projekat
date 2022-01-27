using AutoMapper;
using BankApi.Dto;
using BankApi.Exceptions;
using BankApi.Infrastructure;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BankDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PaymentCardOptions _paymentCardOptions;
        private readonly PccOptions _pccOptions;
        private readonly IPaymentCardService _paymentCardService;
        private readonly object balanceLock = new object();
        public PaymentService(BankDbContext dbContext, IMapper mapper, IOptions<PaymentCardOptions> paymentCardOptions, IOptions<PccOptions> pccOptions, IPaymentCardService paymentCardService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _paymentCardOptions = paymentCardOptions.Value;
            _pccOptions = pccOptions.Value;
            _paymentCardService = paymentCardService;
        }

        public async Task<PaymentResultDto> AuthorizePayment(PayWithCardDto dto)
        {
            Transaction transaction = _dbContext.Transactions.Include(x => x.BankClient).FirstOrDefault(x => x.PaymentId == dto.PaymentId);
            BankAccount merchantAccount = _dbContext.BankAccounts.FirstOrDefault(x => x.BankClientId == transaction.BankClientId);
            if (transaction == null)
                throw new InvalidTransactionException("Transaction does not exist");

            if (transaction.IsCompleted)
                throw new InvalidTransactionException("Transaction already completed");

            if (!dto.CardNumber.StartsWith(_paymentCardOptions.Prefix))
            {
                //Slucaj kad nije iz iste banke
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                int acquirerId = rand.Next(000000000, 999999999); //Ne skalira kako treba i nije skroz random al bolje ne znam
                TransBankPaymentRequestDto transBankPayment = new TransBankPaymentRequestDto 
                {
                    PaymentCardNumber = dto.CardNumber,
                    CardHolderName = dto.CardHolderName,
                    CardHolderLastName = dto.CardHolderLastName,
                    SecurityCode = dto.SecurityCode,
                    ExpirationDate = dto.ExipiringDate,
                    Amount  = transaction.Amount,
                    AcquirerOrderId = acquirerId,
                    AcquirerTimestamp = DateTime.Now
                };

                TransBankPaymentResponseDto response = await Redirect(transBankPayment);
                if (response == null)
                    return new PaymentResultDto
                    {
                        IsSuccessFull = false,
                        IsWithinSameBank = false

                    };

                lock (balanceLock)
                {
                    merchantAccount.MoneyAmount += transaction.Amount;
                    transaction.IsCompleted = true;
                    _dbContext.SaveChanges();
                }

                PaymentResultDto resultDto = new PaymentResultDto
                {
                    PaymentID = transaction.PaymentId,
                    IsSuccessFull = true,
                    MerchantOrderID = transaction.MerchantOrderID,
                    IsWithinSameBank = false,
                    AcquirerOrderID = response.AcquirerOrderId,
                    AcquirerTimestamp = response.AcquirerTimestamp,
                    SuccessURL = transaction.SuccessURL,
                    ErrorURL = transaction.ErrorURL,
                    FailedURL = transaction.FailedURL
                };

            }
            
            //Unutar iste banke
            PaymentCardDto paymentCardDto = new PaymentCardDto
            {
                ExipiringDate = dto.ExipiringDate,
                CardNumber = dto.CardNumber,
                CardHolderLastName = dto.CardHolderLastName,
                CardHolderName = dto.CardHolderName,
                SecurityCode = dto.SecurityCode

            };
            _paymentCardService.ValidatePaymentCard(paymentCardDto);
            BankClient payer = _dbContext.PaymentCards.Include(x => x.BankClient).FirstOrDefault(x => x.CardNumber == dto.CardNumber).BankClient;
            BankAccount payerAccount = _dbContext.BankAccounts.FirstOrDefault(x => x.BankClientId == payer.Id);
            lock (balanceLock)
            {
                if (payerAccount.MoneyAmount < transaction.Amount)
                {
                    throw new InsufficientFundsException("Not enough money to process transaction.");
                }
                payerAccount.MoneyAmount -= transaction.Amount;
                merchantAccount.MoneyAmount += transaction.Amount;
                transaction.IsCompleted = true;
                _dbContext.SaveChanges();
            }

            PaymentResultDto result = new PaymentResultDto
            {
                PaymentID = transaction.PaymentId,
                IsSuccessFull = true,
                MerchantOrderID = transaction.MerchantOrderID,
                IsWithinSameBank = true,
                AcquirerOrderID = null,
                AcquirerTimestamp = null,
                SuccessURL = transaction.SuccessURL,
                ErrorURL = transaction.ErrorURL,
                FailedURL = transaction.FailedURL
            };
            

            return result;
            
        }

        public PaymentRequestResponseDto CreatePaymentTransaction(PaymentRequestDto request)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int paymentID = rand.Next(000000000, 999999999); //Ne skalira kako treba i nije skroz random al bolje ne znam
            BankClient bankClient = _dbContext.BankClients.FirstOrDefault(x => x.MerchantID == request.MerchantID);

            if (bankClient == null)
                throw new UserNonexistingException($"Merchant with ID {request.MerchantID} does not exist.");

            Transaction transaction = new Transaction
            {
                PaymentId = paymentID,
                BankClient = bankClient,
                IsCompleted = false,
                Amount = request.Amount,
                MerchantOrderID = request.MerchantOrderID,
                MerchantTimestamp = request.MerchantTimestamp,
                SuccessURL = request.SuccessURL,
                ErrorURL = request.ErrorURL,
                FailedURL = request.FailedURL

            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            PaymentRequestResponseDto response = new PaymentRequestResponseDto
            {
                PaymentID  = transaction.PaymentId,
                PaymentURL = $"http://localhost:4200/payment/card/pay/{transaction.PaymentId}" 
            };

            return response;

        }

        public async Task<TransBankPaymentResponseDto> ProcessExternalPayment(TransBankPaymentRequestDto request)
        {
            PaymentCardDto paymentCardDto = new PaymentCardDto
            {
                ExipiringDate = request.ExpirationDate,
                CardNumber = request.PaymentCardNumber,
                CardHolderLastName = request.CardHolderLastName,
                CardHolderName = request.CardHolderName,
                SecurityCode = request.SecurityCode

            };
            _paymentCardService.ValidatePaymentCard(paymentCardDto);

            BankClient payer = _dbContext.PaymentCards.Include(x => x.BankClient).FirstOrDefault(x => x.CardNumber == request.PaymentCardNumber).BankClient;
            BankAccount payerAccount = _dbContext.BankAccounts.FirstOrDefault(x => x.BankClientId == payer.Id);
            lock (balanceLock)
            {
                if (payerAccount.MoneyAmount < request.Amount)
                {
                    throw new InsufficientFundsException("Not enough money to process transaction.");
                }
                payerAccount.MoneyAmount -= request.Amount;
                _dbContext.SaveChanges();
            }

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            int issuerId = rand.Next(000000000, 999999999); //Ne skalira kako treba i nije skroz random al bolje ne znam


            return new TransBankPaymentResponseDto
            {
                PaymentCardNumber = request.PaymentCardNumber,
                AcquirerTimestamp = request.AcquirerTimestamp,
                AcquirerOrderId = request.AcquirerOrderId,
                IssuerOrderId = issuerId,
                IssuerTimestamp = DateTime.Now,
                IsSuccessfull = true
        
            };
        }

        public async Task<TransBankPaymentResponseDto> Redirect(TransBankPaymentRequestDto dto)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_pccOptions.BaseURL),
                Timeout = TimeSpan.FromSeconds(60),
            };

            HttpRequestMessage redirectRequest = new HttpRequestMessage(HttpMethod.Post, _pccOptions.Route);

            redirectRequest.Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(redirectRequest);
            if (!response.IsSuccessStatusCode)
                return null;
            string content = await response.Content.ReadAsStringAsync();
            TransBankPaymentResponseDto responseDto = JsonConvert.DeserializeObject<TransBankPaymentResponseDto>(content);

            return responseDto;

        }
    }
}
