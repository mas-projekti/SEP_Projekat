using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP.API.Dto;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Services
{
    public class ItemService : IItemService
    {
        private readonly PaymentServiceProviderDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemService(PaymentServiceProviderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemDto> Get(int id)
        {
            return _mapper.Map<ItemDto>(await _dbContext.Items.FindAsync(id));
        }

        public async Task<List<ItemDto>> GetAll()
        {
            return _mapper.Map<List<ItemDto>>(await _dbContext.Items.Include(x => x.Transaction).ToListAsync());
        }

        public async Task<ItemDto> Insert(ItemDto entity)
        {
            Item newItem = _mapper.Map<Item>(entity);

            newItem.Id = 0;

            await _dbContext.Items.AddAsync(newItem);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ItemDto>(newItem);
        }

    }
}
