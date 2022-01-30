using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DomainModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description{ get; set; }
        public string SubscriptionPeriod { get; set; }
        public string PlanId { get; set; }
        public User User { get; set; }
        public int? UserID { get; set; }
    }
}
