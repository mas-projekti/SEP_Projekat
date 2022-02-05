using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.Enums;

namespace WebShop.Service.Contract.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? BirthDay { get; set; }
        public string ImageURL { get; set; }
        public UserType UserType { get; set; }
        public string MerchantId { get; set; }
        public string BankMerchantID { get; set; }
        public string MerchantPassword { get; set; }
        public string BankURL { get; set; }
    }
}
