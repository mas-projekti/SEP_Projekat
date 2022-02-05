using Identity.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Intefraces
{
    public interface IClientService
    {
        public Task<CreatedClientDto> AddClientAsync(NewClientDto dto);
    }
}
