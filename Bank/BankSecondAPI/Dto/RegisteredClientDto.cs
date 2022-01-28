using BankApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Dto
{
    public class RegisteredClientDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MerchantID { get; set; }
        public string MerchantPassword { get; set; }
        public ClientType ClientType { get; set; }
        public BankAccountDto BankAccount { get; set; }
    }
}
