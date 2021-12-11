using System;
using System.Collections.Generic;
using WebShop.Models.Enums;

namespace WebShop.Models.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string MerchantId { get; set; }
        public UserType UserType { get; set; }
        public DateTime? BirthDay { get; set; }
        public string ImageURL { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}
