using Newtonsoft.Json.Linq;
using Paypal.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Interfaces
{
    public interface IDataAdapter
    {
        JObject RepackOrderToJObject(CreateOrderDto dto);
    }
}
