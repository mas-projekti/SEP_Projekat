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
using System.Threading;
using System.Threading.Tasks;

namespace BankApi.Services
{
    public class PaymentCardService : IPaymentCardService
    {
        private readonly IMapper _mapper;
        private readonly BankDbContext _dbContext;
        private readonly PaymentCardOptions _paymentCardOptions;
        private static Random random = new Random();

        public PaymentCardService(IMapper mapper, BankDbContext dbContext, IOptions<PaymentCardOptions> paymentCardOptions)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _paymentCardOptions = paymentCardOptions.Value;
        }

        public PaymentCardDto GeneratePaymentCard(int id)
        {
            BankClient user = _dbContext.BankClients.FirstOrDefault(x => x.Id == id);

            if(user == null)
            {
                throw new UserNonexistingException($"User with id {id} does not exist.");
            }

            string ccnumber = _paymentCardOptions.Prefix;
            int length = 16;
            const string chars = "0123456789";
            string secNum =  new string(Enumerable.Repeat(chars, 3)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            while (ccnumber.Length < (length- 1))
            {
                double rnd = (new Random().NextDouble() * 1.0f - 0f);

                ccnumber += Math.Floor(rnd * 10);

                //sleep so we get a different seed

                Thread.Sleep(20);
            }


            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();

            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum

            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (length - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            PaymentCard card = new PaymentCard
            { 
                ExipiringDate = DateTime.Now.AddYears(5),
                CardNumber = ccnumber,
                SecurityCode = secNum,
                BankClient = user

            };

            _dbContext.PaymentCards.Add(card);
            _dbContext.SaveChanges();

            PaymentCardDto cardReturn = _mapper.Map<PaymentCardDto>(card);
            cardReturn.CardHolderName = card.BankClient.Name;
            cardReturn.CardHolderLastName = card.BankClient.LastName;

            return cardReturn;

        }
    }
}
