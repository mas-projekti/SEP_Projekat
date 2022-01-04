using PSP.API.Dto;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Services
{
    public class ClientService : IClientService
    {
        private readonly PaymentServiceProviderDbContext _dbContext;
        public CreatedClientDto CreateClient(NewPspClientDto newClient)
        {
            //Contact identity to create client

            return new CreatedClientDto();

            
        }
    }
}
