using BankApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Interfaces
{
    public interface IBankClientService
    {
        RegisteredClientDto RegisterNewClient(ClientRegisterDto newClient);
        LoginResponseDto Login(LoginDto dto);
    }
}
