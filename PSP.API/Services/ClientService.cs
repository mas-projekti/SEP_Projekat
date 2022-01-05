using AutoMapper;
using Common.Exceptions;
using Common.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PSP.API.Dto;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PSP.API.Services
{
    public class ClientService : IClientService
    {
        private readonly PaymentServiceProviderDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<ClientService> _log;
        private readonly IMapper _mapper;

        public ClientService(PaymentServiceProviderDbContext dbContext, IConfiguration config, ILogger<ClientService> log, IMapper mapper)
        {
            _dbContext = dbContext;
            _config = config;
            _log = log;
            _mapper = mapper;
        }

        public async Task<CreatedPspClientDto> CreateClient(NewPspClientDto newClient)
        {

            NewClientDto newClientRequest = new NewClientDto 
            {
                Bank = newClient.BankActive,
                Paypal = newClient.PaypalActive,
                Bitcoin = newClient.BitcoinActive,
                ClientName = newClient.ClientName
            };

            //Create client
            var http = new HttpClient
            {
                BaseAddress = _config.GetIdentityConfig().IdentityURL,
                Timeout = TimeSpan.FromSeconds(30),
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"/api/clients");
            request.Content = new StringContent(JsonConvert.SerializeObject(newClientRequest), Encoding.UTF8, "application/json");
            _log.LogInformation($"Sending request to create new client with name {newClient.ClientName}.");
            HttpResponseMessage response = await http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Client could not be created!");
            string content = await response.Content.ReadAsStringAsync();
            CreatedClientDto clientDto = JsonConvert.DeserializeObject<CreatedClientDto>(content);

            //Create client data in PSP
            PspClient client = new PspClient
            {
                BankActive = newClient.BankActive,
                PaypalActive = newClient.PaypalActive,
                BitcoinActive = newClient.BitcoinActive,
                ClientID = clientDto.ClientID,
                SettingsUpdatedCallback = newClient.SettingsUpdatedCallback,
                TransactionOutcomeCallback = newClient.TransactionOutcomeCallback
            };

            _dbContext.PspClients.Add(client);
            await _dbContext.SaveChangesAsync();


            CreatedPspClientDto createdPspClient = _mapper.Map<CreatedPspClientDto>(client);
            createdPspClient.ClientName = newClient.ClientName;
            createdPspClient.ClientSecret = clientDto.ClientSecret;

            return createdPspClient;


        }

        public async Task<PspClientDto> GetClient(int id)
        {
            return _mapper.Map<PspClientDto>(await _dbContext.PspClients.FindAsync(id));
        }

        public async Task<PspClientDto> UpdateClient(int id, PspClientDto newData)
        {
            PspClient client = await _dbContext.PspClients.FindAsync(id);
            if (client == null)
                throw new ClientDoesntExistException($"Client with id {id} does not exist.");

            client.Update(_mapper.Map<PspClient>(newData));

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PspClientDto>(client);

        }
    }
}
