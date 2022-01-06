using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class CreatedPspClientDto
    {
        public int Id { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public string TransactionOutcomeCallback { get; set; }
        public string SettingsUpdatedCallback { get; set; }
        public bool PaypalActive { get; set; }
        public bool BitcoinActive { get; set; }
        public bool BankActive { get; set; }

    }
}
