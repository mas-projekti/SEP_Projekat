using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Service.Contract.Dto
{
    public class PaymentOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSupported { get; set; }
    }
}
