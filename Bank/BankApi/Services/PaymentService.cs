using AutoMapper;
using BankApi.Dto;
using BankApi.Exceptions;
using BankApi.Infrastructure;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Options;
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

        public PaymentService(BankDbContext dbContext, IMapper mapper, IOptions<PaymentCardOptions> paymentCardOptions)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _paymentCardOptions = paymentCardOptions.Value;
        }

        public PaymentResultDto AuthorizePayment(PayWithCardDto dto)
        {
            if(!dto.CardNumber.StartsWith(_paymentCardOptions.Prefix))
            {
                //Slucaj kad nije iz iste banke
                return null; //za sada

            }
            //Validate number ovde treba uraditi
            PaymentCard paymentCard = _dbContext.PaymentCards.FirstOrDefault(x => x.CardNumber == dto.CardNumber);
            return null;
            
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
