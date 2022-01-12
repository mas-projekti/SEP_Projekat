using PSP.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PSP.API.Interfaces
{
    public interface IClientService
    {
        public Task<CreatedPspClientDto> CreateClient(NewPspClientDto newClient);
        public Task<PspClientDto> GetClient(int id);
        public PspClientDto GetClientByClientID(string id);
        public Task<PspClientDto> UpdateClient(int id, PspClientDto newData);
        public Task NotifyClientDataUpdatedAsync(int clientID);
        public Task NotifyClientTransactionFinishedAsync( Guid transactionId);
        public bool CheckUsersRightsToUpdate(ClaimsPrincipal principal, int clientId);
    }
}
