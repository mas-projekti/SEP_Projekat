using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.Enums;

namespace WebShop.Models.DomainModels
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public CategoryType CategoryType  { get; set; }
        public int Warranty { get; set; }
        public string Description { get; set; }       
        public int Amount { get; set; }
        public string ImageURL { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }

    }
}
