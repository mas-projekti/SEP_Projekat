using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public int PspClientId { get; set; }
        public PspClient Client { get; set; }
        public List<Item> Items { get; set; }
        public BankTransaction BankTransaction { get; set; }
    }
}
