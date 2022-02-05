using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Dto
{
    public class CreatedClientDto
    {
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }
        public string ClientID { get; set; }
    }
}
