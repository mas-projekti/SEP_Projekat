using PSP.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetAll();
        Task<TransactionDto> Get(Guid id);
        Task<TransactionDto> Insert(List<ItemDto> items);
        Task Delete(Guid id);
    }
}
