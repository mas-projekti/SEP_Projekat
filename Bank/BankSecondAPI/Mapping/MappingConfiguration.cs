using AutoMapper;
using BankApi.Dto;
using BankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<BankClient, RegisteredClientDto>().ReverseMap();
            CreateMap<BankClient, LoginResponseDto>().ReverseMap();
            CreateMap<BankAccount, BankAccountDto>().ReverseMap();
            CreateMap<PaymentCard, PaymentCardDto>().ReverseMap();
            CreateMap<PaymentRequestDto, Transaction>().ReverseMap();
            CreateMap<TransactionDto, Transaction>().ReverseMap();

        }
    }
}
