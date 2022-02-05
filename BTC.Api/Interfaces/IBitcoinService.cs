using BTC.Api.Dtos;
using BTC.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Interfaces
{
    public interface IBitcoinService
    {
        Task<BitcoinOrderResult> CreateBitcoinOrderAsync(BitcoinOrderDto order);
    }
}
