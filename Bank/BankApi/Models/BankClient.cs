
using BankApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class BankClient
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public List<PaymentCard> PaymentCards { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MerchantID { get; set; }
        public string MerchantPassword { get; set; }
        public ClientType ClientType { get; set; }
       
    }
}
