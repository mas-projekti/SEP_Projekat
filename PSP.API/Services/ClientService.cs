using AutoMapper;
using Common.Exceptions;
using Common.Identity;
using Common.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PSP.API.Dto;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using PSP.API.Models;
using PSP.API.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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
        private readonly HookSecretOptions _hookOptions;

        public ClientService(IOptions<HookSecretOptions> hookOptions, PaymentServiceProviderDbContext dbContext, IConfiguration config, ILogger<ClientService> log, IMapper mapper)
        {
            _dbContext = dbContext;
            _config = config;
            _log = log;
            _mapper = mapper;
            _hookOptions = hookOptions.Value;
        }

        public bool CheckUsersRightsToUpdate(ClaimsPrincipal principal, int clientId)
        {
            PspClient pspClient = _dbContext.PspClients.Find(clientId);

            if (pspClient.ClientID != principal.Claims.FirstOrDefault(x => x.Type == "client_id").Value)
                return false;
            return true;
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

            AesCryptoProvider provider = new AesCryptoProvider(_hookOptions.Key);
            //Create client data in PSP
            PspClient client = new PspClient
            {
                BankActive = newClient.BankActive,
                PaypalActive = newClient.PaypalActive,
                BitcoinActive = newClient.BitcoinActive,
                ClientID = clientDto.ClientID,
                ValidatingSecret = provider.EncryptString(newClient.ValidatingSecret),
                SettingsUpdatedCallback = newClient.SettingsUpdatedCallback,
                TransactionOutcomeCallback = newClient.TransactionOutcomeCallback
            };

            _dbContext.PspClients.Add(client);
            await _dbContext.SaveChangesAsync();
            await NotifyClientDataUpdatedAsync(client.Id);

            CreatedPspClientDto createdPspClient = _mapper.Map<CreatedPspClientDto>(client);
            createdPspClient.ClientName = newClient.ClientName;
            createdPspClient.ClientSecret = clientDto.ClientSecret;

            return createdPspClient;


        }

        public async Task<PspClientDto> GetClient(int id)
        {
            PspClientDto ret =  _mapper.Map<PspClientDto>(await _dbContext.PspClients.FindAsync(id));
            if(ret == null)
                throw new ClientDoesntExistException($"Client with id {id} does not exist.");
            return ret;
        }

        public PspClientDto GetClientByClientID(string id)
        {
            PspClientDto ret = _mapper.Map<PspClientDto>( _dbContext.PspClients.FirstOrDefault(x => x.ClientID == id));

            if (ret == null)
                throw new ClientDoesntExistException($"Client with clientID {id} does not exist.");
            return ret;
        }

        //SYNC
        public async Task NotifyClientDataUpdatedAsync(int clientID)
        {
            PspClient client = _dbContext.PspClients.Find(clientID);

            //Create client
            var http = new HttpClient
            {
                BaseAddress = new Uri(client.SettingsUpdatedCallback.Substring(0, client.SettingsUpdatedCallback.IndexOf('/', 8))),
                Timeout = TimeSpan.FromSeconds(30),
            };

            Dictionary<string, bool> options = new Dictionary<string, bool>();
            options.Add("paypal", client.PaypalActive);
            options.Add("bank", client.BankActive);
            options.Add("bitcoin", client.BitcoinActive);

            AesCryptoProvider provider = new AesCryptoProvider(_hookOptions.Key);
            SignatureProvider signer = new SignatureProvider(provider.DecryptString(client.ValidatingSecret));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, client.SettingsUpdatedCallback.Substring(client.SettingsUpdatedCallback.IndexOf('/')));
            request.Content = new StringContent(JsonConvert.SerializeObject(options), Encoding.UTF8, "application/json");
            request.Headers.Add("X-Sender-Signature", signer.SignString(await request.Content.ReadAsStringAsync()));
            _log.LogInformation($"Sending request to notify settings changed for client  {clientID}  with callback {request.RequestUri}.");
            HttpResponseMessage response = await http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                _log.LogError($"Client { request.RequestUri }could not be contacted!!");
            
        }

        //SYNC
        public async Task NotifyClientTransactionFinishedAsync( Guid transactionId)
        {
            Transaction transaction = _dbContext.Transactions.Find(transactionId);
            PspClient client = _dbContext.PspClients.First(x => x.Id == transaction.PspClientId);

            //Create client
            var http = new HttpClient
            {
                BaseAddress = new Uri(client.SettingsUpdatedCallback.Substring(0, client.TransactionOutcomeCallback.IndexOf('/', 8))),
                Timeout = TimeSpan.FromSeconds(30),
            };

            AesCryptoProvider provider = new AesCryptoProvider(_hookOptions.Key);
            SignatureProvider signer = new SignatureProvider(provider.DecryptString(client.ValidatingSecret));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, client.TransactionOutcomeCallback.Substring(client.TransactionOutcomeCallback.IndexOf('/', 8) +1));
            request.Content = new StringContent(JsonConvert.SerializeObject(transactionId), Encoding.UTF8, "application/json");
            request.Headers.Add("X-Sender-Signature", signer.SignString(await request.Content.ReadAsStringAsync()));
            _log.LogInformation($"Sending request to notify transaction finished for client  {client.ClientID}  with callback {request.RequestUri}.");
            HttpResponseMessage response = await http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                _log.LogError($"Client { request.RequestUri }could not be contacted!!");


        }

        public async Task<PspClientDto> UpdateClient(int id, PspClientDto newData)
        {
            PspClient client = await _dbContext.PspClients.FindAsync(id);
            if (client == null)
                throw new ClientDoesntExistException($"Client with id {id} does not exist.");

            client.Update(_mapper.Map<PspClient>(newData));

            await _dbContext.SaveChangesAsync();
            await NotifyClientDataUpdatedAsync(id);

            return _mapper.Map<PspClientDto>(client);
            

        }
    }
}
