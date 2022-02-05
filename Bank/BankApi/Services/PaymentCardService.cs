using AutoMapper;
using BankApi.Dto;
using BankApi.Exceptions;
using BankApi.Infrastructure;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Options;
using BankApi.Security;
using Microsoft.EntityFrameworkCore;
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
        private readonly EncryptionKeyOptions _encryptionKeyOptions;
        private static Random random = new Random();

        public PaymentCardService(IMapper mapper, BankDbContext dbContext, IOptions<EncryptionKeyOptions> encryptionKeyOptions, IOptions<PaymentCardOptions> paymentCardOptions)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _paymentCardOptions = paymentCardOptions.Value;
            _encryptionKeyOptions = encryptionKeyOptions.Value;
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
            AesCryptoProvider aes = new AesCryptoProvider(_encryptionKeyOptions.Key);
            PaymentCard card = new PaymentCard
            { 
                ExipiringDate = DateTime.Now.AddYears(5),
                CardNumber = aes.EncryptString(ccnumber),
                SecurityCode = aes.EncryptString(secNum),
                BankClient = user

            };

            _dbContext.PaymentCards.Add(card);
            _dbContext.SaveChanges();

            card.CardNumber = aes.DecryptString(card.CardNumber);
            card.SecurityCode = aes.DecryptString(card.SecurityCode);
            PaymentCardDto cardReturn = _mapper.Map<PaymentCardDto>(card);
            cardReturn.CardHolderName = card.BankClient.Name;
            cardReturn.CardHolderLastName = card.BankClient.LastName;

            return cardReturn;

        }

        public bool ValidatePaymentCard(PaymentCardDto dto)
        {
            AesCryptoProvider provider = new AesCryptoProvider(_encryptionKeyOptions.Key);
            //Validate number ovde treba uraditi

            PaymentCard paymentCard = _dbContext.PaymentCards.Include(x => x.BankClient).FirstOrDefault(x => x.BankClient.Name == dto.CardHolderName && x.BankClient.LastName == dto.CardHolderLastName);

            if (paymentCard == null)
                throw new InvalidCardDataException("Card does not exist");

            paymentCard.CardNumber = provider.DecryptString(paymentCard.CardNumber);
            paymentCard.SecurityCode = provider.DecryptString(paymentCard.SecurityCode);
            //Check card number by Luhns algorithm
            int nDigits = dto.CardNumber.Length;

            int nSum = 0;
            bool isSecond = false;
            for (int i = nDigits - 1; i >= 0; i--)
            {

                int d = dto.CardNumber[i] - '0';

                if (isSecond == true)
                    d = d * 2;

                // We add two digits to handle
                // cases that make two digits
                // after doubling
                nSum += d / 10;
                nSum += d % 10;

                isSecond = !isSecond;
            }
            if(nSum % 10 != 0)
                throw new InvalidCardDataException("Card number is invalid.");

            if (paymentCard.ExipiringDate.Year < DateTime.Now.Year || (paymentCard.ExipiringDate.Month < DateTime.Now.Month && paymentCard.ExipiringDate.Year == DateTime.Now.Year))//Expired
                throw new InvalidCardDataException("Card is expired");

            if (paymentCard.SecurityCode != dto.SecurityCode)
                throw new InvalidCardDataException("Invalid security code. Payment failed.");

            if(paymentCard.BankClient.Name != dto.CardHolderName || paymentCard.BankClient.LastName != dto.CardHolderLastName)
                throw new InvalidCardDataException("Invalid card holder data.");

            paymentCard.CardNumber = provider.EncryptString(paymentCard.CardNumber);
            paymentCard.SecurityCode = provider.EncryptString(paymentCard.SecurityCode);

            return true;
        }
    }
}
