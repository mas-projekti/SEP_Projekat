using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure;
using WebShop.Models.DomainModels;
using WebShop.Repository.Contract.Interfaces;

namespace WebShop.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WebShopDbContext context) : base(context) { }

        public async Task<User> GetByUsernameAndPassword(string username, string password)
        {
            return await _context.Set<User>().Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();   
        }
    }
}
