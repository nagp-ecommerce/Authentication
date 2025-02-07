using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Application.DTOs;
using Authentication.Application.Interfaces;
using Authentication.Domain;
using ProductService.Application.Interfaces;
using BCrypt.Net;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Authentication.Application.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository<Account> accountRepository;
        private IConfiguration config;
        public AccountService(IAccountRepository<Account> _accountRepository, IConfiguration _config)
        {
            accountRepository = _accountRepository;
            config = _config;
        }
        public async Task<Response> Login(LoginDTO login)
        {
            var userAccount = await accountRepository.GetByUserEmail(login.Email);
            if (userAccount is null)
            {
                return new Response { Success = false, Message = "User does not exist, register to sign in" };
                throw new Exception("User not found");
            }
            bool verifyPassword = BCrypt.Net.BCrypt.Verify(login.Password, userAccount.Password);
            if(!verifyPassword)
                return new Response { Success = false, Message = "Invalid Credentials" };
            string token = GenerateJwtToken(userAccount);
            return new Response { Success = true, Message = token };

        }

        private string GenerateJwtToken(Account userAccount)
        {
            var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
            var cred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var role = userAccount.Role ?? "User";
            var claims = new List<Claim> { 
                new(ClaimTypes.Name, userAccount.Name!),
                new(ClaimTypes.Email, userAccount.Email!),
                new(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                    issuer: config["Authentication:Issuer"],
                    audience: config["Authentication:Audience"],   
                    claims,
                    expires:null,
                    signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Response> Register(CreateUserDTO userDto)
        {
            var userAccount = await accountRepository.GetByUserEmail(userDto.Email);
            if (userAccount is not null)
            {
                return new Response { Success = false, Message = "User already exist" };
                throw new Exception("User already exists");
            }
            await accountRepository.CreateAsync(new Account
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                AccountStatus = "ACTIVE",
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow,
                Role=userDto.Role!
            });
            return new Response { Success = true, Message = "User created successfully" };
        }


        public async Task<Response> UpdateProfile(string email, UpdateUserDTO user)
        {
            var userAccount = await accountRepository.GetByUserEmail(email);
            if (userAccount is null)
            {
                return new Response { Success = false, Message = "User does not exist" };
                throw new Exception("User does not exists");
            }
            if (!string.IsNullOrEmpty(user.Phone))
            {
                userAccount.Phone = user.Phone;
            }

            if (!string.IsNullOrEmpty(user.Name))
            {
                userAccount.Name = user.Name;
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                userAccount.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
            }
            await accountRepository.UpdateAsync(userAccount);
            return new Response { Success = true, Message = "User updated successfully" };
        }
    }
}
