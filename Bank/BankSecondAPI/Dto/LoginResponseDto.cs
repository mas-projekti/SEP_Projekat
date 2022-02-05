using BankApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Dto
{
    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public ClientType ClientType { get; set; }
    }
}
