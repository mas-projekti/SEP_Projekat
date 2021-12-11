using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Service.Contract.Dto;

namespace WebShop.Service.Contract.Services
{
    public interface IUserService
    {  
        Task<UserDto> GetUserById(int userId);
        Task<UserDto> InsertUser(UserDto newUser);
        Task<UserDto> UpdateUser(UserDto user);
    }
}
