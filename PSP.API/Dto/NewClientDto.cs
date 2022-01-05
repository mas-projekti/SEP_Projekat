using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class NewClientDto
    {
        public string ClientName { get; set; }
        public bool Bitcoin { get; set; }
        public bool Paypal { get; set; }
        public bool Bank { get; set; }
    }
}
