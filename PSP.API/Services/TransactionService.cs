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
    public class TransactionService : ITransactionService
    {
        private readonly PaymentServiceProviderDbContext _dbContext;
        private readonly IMapper _mapper;

        public TransactionService(PaymentServiceProviderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionDto> Get(Guid id)
        {
            Transaction transaction =  await _dbContext.Transactions.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
            TransactionDto transactionDto = CreateTransactionDto(transaction);
            return transactionDto;
        }

        public Task<List<TransactionDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionDto> Insert(List<ItemDto> items)
        {
            List<Item> transactionItems = _mapper.Map<List<Item>>(items);
            Guid newGuid = Guid.NewGuid();
            Transaction t = new Transaction() { Id = newGuid  };
            foreach (Item item in transactionItems)
            {
                item.Transaction = t;
                item.TransactionId = newGuid;
            }
            t.Items = transactionItems;

          
            await _dbContext.Transactions.AddAsync(t);
            await _dbContext.SaveChangesAsync();
        

            return CreateTransactionDto(t);

        }


        private TransactionDto CreateTransactionDto(Transaction transaction)
        {
            if (transaction == null)
                return null;

            List<ItemDto> items = _mapper.Map<List<ItemDto>>(transaction.Items);
            List<string> merchantIds = new List<string>();


            foreach(Item item in transaction.Items)
            {
                if(!merchantIds.Contains(item.MerchantId))
                    merchantIds.Add(item.MerchantId);
            }

            return new TransactionDto()
            {
                Id = transaction.Id,
                Currency = transaction.Items[0].Currency,
                Items = items,
                MerchantIds = merchantIds
            };

                
    }
    }
}
