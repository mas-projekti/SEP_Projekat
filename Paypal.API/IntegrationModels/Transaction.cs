using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Transaction
    {
        public Amount amount { get; set; }
        public Payee payee { get; set; }
        public ItemList item_list { get; set; }
        public RelatedResources[] related_resources { get; set; }

    }
}
