using AutoMapper;
using BankApi.Dto;
using BankApi.Exceptions;
using BankApi.Infrastructure;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly BankDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PaymentCardOptions _paymentCardOptions;
        private readonly IPaymentCardService _paymentCardService;
        private readonly object balanceLock = new object();
        public PaymentService(BankDbContext dbContext, IMapper mapper, IOptions<PaymentCardOptions> paymentCardOptions, IPaymentCardService paymentCardService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _paymentCardOptions = paymentCardOptions.Value;
            _paymentCardService = paymentCardService;
        }

        public PaymentResultDto AuthorizePayment(PayWithCardDto dto)
        {
            if(!dto.CardNumber.StartsWith(_paymentCardOptions.Prefix))
            {
                //Slucaj kad nije iz iste banke
                return null; //za sada

            }
            PaymentCardDto paymentCardDto = new PaymentCardDto
            {
                ExipiringDate = dto.ExipiringDate,
                CardNumber = dto.CardNumber,
                CardHolderLastName = dto.CardHolderLastName,
                CardHolderName = dto.CardHolderName,
                SecurityCode = dto.SecurityCode

            };
            _paymentCardService.ValidatePaymentCard(paymentCardDto);

            Transaction transaction = _dbContext.Transactions.Include(x => x.BankClient).FirstOrDefault(x => x.PaymentId == dto.PaymentId);
            if (transaction == null)
                throw new InvalidTransactionException("Transaction does not exist");

            if (transaction.IsCompleted)
                throw new InvalidTransactionException("Transaction already completed");

            BankClient payer = _dbContext.PaymentCards.Include(x => x.BankClient).FirstOrDefault(x => x.CardNumber == paymentCardDto.CardNumber).BankClient;
            BankAccount payerAccount = _dbContext.BankAccounts.FirstOrDefault(x => x.BankClientId == payer.Id);
            lock (balanceLock)
            {
                if (payerAccount.MoneyAmount - payerAccount.ReservedMoneyAmount < transaction.Amount)
                {
                    throw new InsufficientFundsException("Not enough money to process transaction.");
                }
                payerAccount.ReservedMoneyAmount += transaction.Amount;
                transaction.IsCompleted = true;
                _dbContext.SaveChanges();
            }

            PaymentResultDto result = new PaymentResultDto
            {
                PaymentID = transaction.PaymentId,
                IsSuccessFull = true,
                MerchantOrderID  = transaction.MerchantOrderID,
                IsWithinSameBank  = true,
                AcquirerOrderID  = null,
                AcquirerTimestamp  = null
            };
            

            return result;
            
        }

        public PaymentRequestResponseDto CreatePaymentTransaction(PaymentRequestDto request)
        {
            Random rand = new Random(100);
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
                PaymentURL = "www.google.com" //ovo promeniti obavezno kasnije
            };

            return response;

        }
    }
}
