using PSP.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Interfaces
{
    public interface IItemService
    {
        Task<List<ItemDto>> GetAll();
        Task<ItemDto> Get(int id);
        Task<ItemDto> Insert(ItemDto entity);
        Task Delete(int id);
    }
}
