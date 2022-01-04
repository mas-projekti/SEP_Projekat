using Identity.API.Dto;
using Identity.API.Intefraces;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class ClientService : IClientService
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public ClientService(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        public async Task<CreatedClientDto> AddClientAsync(NewClientDto dto)
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[30];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string uniq = Convert.ToBase64String(buffer);
            List<string> scopes = new List<string>();

            if(dto.Bank)
            {
                scopes.Add("bank-api");
            }

            if (dto.Paypal)
            {
                scopes.Add("paypal-api");
            }

            if (dto.Bitcoin)
            {
                scopes.Add("bitcoin-api");
            }


            Client newClient = new Client
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientName = dto.ClientName,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                 {
                    new Secret(uniq.Sha256())
                 },
                AllowedScopes = scopes
            };

            _configurationDbContext.Clients.Add(newClient.ToEntity());

            await  _configurationDbContext.SaveChangesAsync();

            return new CreatedClientDto
            {
                ClientID = newClient.ClientId,
                ClientName = newClient.ClientName,
                ClientSecret = uniq
            };

        }
    }
}
