using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Service.Contract.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string CategoryType { get; set; }
        public int Warranty { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string ImageURL { get; set; }
        public int UserId { get; set; }
    }
}
