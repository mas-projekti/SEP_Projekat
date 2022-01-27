using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Repository.Contract.Interfaces;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;
using WebShop.Models.Generator;
using WebShop.Models.DomainModels;

namespace WebShop.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(int userId) => _mapper.Map<UserDto>(await _userRepository.Get(userId));

        public async Task<UserDto> InsertUser(UserDto newUser)
        {
            newUser.Salt = Degenerator.GenerateRandomString();
            newUser.Password = Degenerator.GeneratePasswordWithSalt(newUser.Password + newUser.Salt);
            await _userRepository.Add(_mapper.Map<User>(newUser));
            return newUser;
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _userRepository.GetByUsername(username);
            if (!User.isPasswordMatch(password, user)) throw new ArgumentNullException();
            UserDto userDTO = _mapper.Map<UserDto>(user);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"))); // IZ .env izvuci info
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userDTO.Username),
                new Claim(ClaimTypes.Role, userDTO.UserType.ToString()),
                new Claim(ClaimTypes.Actor, userDTO.ImageURL),
                new Claim(ClaimTypes.SerialNumber, userDTO.Id.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("BACKEND_DOMAIN"),
                audience: Environment.GetEnvironmentVariable("FRONTEND_DOMAIN"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
