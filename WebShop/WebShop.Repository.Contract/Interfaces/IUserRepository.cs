using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;

namespace WebShop.Repository.Contract.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByUsernameAndPassword(string username, string password);

        public Task<User> GetByUsername(string username);
    }
}
