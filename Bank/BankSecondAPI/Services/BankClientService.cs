using AutoMapper;
using BankApi.Dto;
using BankApi.Exceptions;
using BankApi.Infrastructure;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BankApi.Services
{
    public class BankClientService : IBankClientService
    {
        private readonly BankDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly TokenKeyOptions _secretKey;

        public BankClientService(BankDbContext dbContext, IMapper mapper, IOptions<TokenKeyOptions> secretKey)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _secretKey = secretKey.Value;
        }

        public LoginResponseDto Login(LoginDto dto)
        {
            BankClient client = _dbContext.BankClients.FirstOrDefault(x => x.Username == dto.Username);

            if(client == null)
            {
                throw new UserNonexistingException("User does not exist");
            }

            if(BCryptNet.Verify(dto.Password, client.Password))
            {
                LoginResponseDto response = _mapper.Map<LoginResponseDto>(client);
                response.Token = CreateToken(client);
                response.IsSuccess = true;
                return response;
            }

            throw new PasswordInvalidException("Wrong password");
        }

        public RegisteredClientDto RegisterNewClient(ClientRegisterDto newClient)
        {
            byte[] bufferID = new byte[30];
            byte[] bufferPass = new byte[100];
            string merchantId = "";
            string merchantPass = "";

            if( newClient.ClientType == Enums.ClientType.Merchant)
            {
                RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
                cryptoRandomDataGenerator.GetBytes(bufferID);
                merchantId = Convert.ToBase64String(bufferID);

                cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
                cryptoRandomDataGenerator.GetBytes(bufferPass);
                merchantPass = Convert.ToBase64String(bufferPass);
            }

            BankAccount bankAccount = new BankAccount();


            BankClient client = new BankClient 
            { 
                Name = newClient.Name,
                Username = newClient.Username,
                LastName = newClient.LastName,
                ClientType = newClient.ClientType,
                Password = BCryptNet.HashPassword(newClient.Password, BCryptNet.GenerateSalt()),
                MerchantID = merchantId,
                MerchantPassword = merchantPass,
                BankAccount = bankAccount
            };

            _dbContext.BankClients.Add(client);
            _dbContext.SaveChanges();

            return _mapper.Map<RegisteredClientDto>(client);

        }

        private string CreateToken(BankClient user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, user.ClientType.ToString())); //Add user type to claim
            claims.Add(new Claim(ClaimTypes.Name, user.Name)); //Add name 
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName)); //Add lastname
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); //Add ID

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44355",
                audience: "http://localhost:44355",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
