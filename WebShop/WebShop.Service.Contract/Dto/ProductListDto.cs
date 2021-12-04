using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Service.Contract.Dto
{
    public class ProductListDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
