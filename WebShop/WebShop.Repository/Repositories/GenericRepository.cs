using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure;
using WebShop.Repository.Contract.Interfaces;

namespace WebShop.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly WebShopDbContext _context;
        public GenericRepository(WebShopDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Get(int id) => await _context.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();


        public async Task<T> Update(int id, T entity)
        {       
            var findEntity = await _context.Set<T>().FindAsync(id);
            if (findEntity == null)
            {
                return findEntity;
            }

            _context.Entry(findEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
