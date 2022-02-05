using AutoMapper;
using PSP.API.Dto;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<PspClient, CreatedPspClientDto>().ReverseMap();
            CreateMap<PspClient, PspClientDto>().ReverseMap();
            CreateMap<BankTransaction, BankTransactionDto>().ReverseMap();
            CreateMap<BankTransaction, BankPaymentRequestDto>().ReverseMap();
            CreateMap<SubscriptionTransaction, SubscriptionTransactionDto>().ReverseMap();
        }
    }
}
