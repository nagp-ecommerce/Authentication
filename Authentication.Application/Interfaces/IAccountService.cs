using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Application.DTOs;
using Authentication.Domain;
using ProductService.Application.Interfaces;

namespace Authentication.Application.Interfaces
{
    public interface IAccountService
    {
        public Task<Response> UpdateProfile(string email, UpdateUserDTO user);
        //public Task<ProductReview> AddProductReview();
        public Task<Response> Login(LoginDTO login);
        public Task<Response> Register(CreateUserDTO user);

    }
}
