using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Repository.Contract.Interfaces;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;

namespace WebShop.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public Task<UserDto> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> InsertUser(UserDto newUser)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
