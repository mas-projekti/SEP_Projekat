using BTC.Api.Dtos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Interfaces
{
    public interface IDataConverter
    {
        Task<JObject> RepackOrderToJObjectAsync(BitcoinOrderDto dto);
    }
}
