using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public List<Item> Items { get; set; }
    }
}
