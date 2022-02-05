using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Models
{
    public class BitcoinOrderResult
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public bool Do_not_convert { get; set; }
        public string Price_currency { get; set; }
        public string Price_amount { get; set; }
        public bool Lightning_network { get; set; }
        public string Receive_currency { get; set; }
        public string Receive_amount { get; set; }
        public DateTime Created_at { get; set; }
        public string Order_id { get; set; }
        public string Payment_url { get; set; }
        public string Underpaid_amount { get; set; }
        public string Overpaid_amount { get; set; }
        public bool Is_refundable { get; set; }
        public string Token { get; set; }


    }
}
