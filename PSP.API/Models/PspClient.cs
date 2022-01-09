using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Models
{
    public class PspClient
    {
        public int Id { get; set; }
        public string TransactionOutcomeCallback { get; set; }
        public string SettingsUpdatedCallback { get; set; }
        public string ClientID { get; set; }
        public bool PaypalActive { get; set; }
        public bool BitcoinActive { get; set; }
        public bool BankActive { get; set; }
        public List<Transaction> Transactions { get; set; }

        public PspClient Update(PspClient newData)
        {
            this.BankActive = newData.BankActive;
            this.BitcoinActive = newData.BitcoinActive;
            this.PaypalActive = newData.PaypalActive;
            this.SettingsUpdatedCallback = newData.SettingsUpdatedCallback;
            this.TransactionOutcomeCallback = newData.TransactionOutcomeCallback;

            return this;
        }


    }
}
