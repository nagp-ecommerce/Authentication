using System.Linq.Expressions;
using Authentication.Application.Interfaces;
using Authentication.Domain;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;

namespace Authentication.Infrastructure.Repository
{
    public class UserRepository 
    : IUserRepository<User>
    {
        private AuthenticationDbContext dbContext;
        private ILogger<UserRepository> logger;
        public UserRepository(AuthenticationDbContext _dbContext, ILogger<UserRepository> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
        }
        public async Task<Response> CreateAsync(User Entity)
        {
            try
            {
                await dbContext.Users.AddAsync(Entity);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{Entity.UserId} created succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while creating the User");
                return new Response { Success = false, Message = $"Error occured while creating the User {ex.Message}" };
            }

        }

        public async Task<Response> DeleteAsync(User Entity)
        {
            try
            {
                var User = await GetByIdAsync(Entity.UserId);
                if (User == null)
                    return new Response { Success = false, Message = "User not found" };

                dbContext.Users.Remove(User);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{Entity.UserId} deleted succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while deleting the User");
                return new Response { Success = false, Message = $"Error occured while deleting the User {ex.Message}" };
            }
        }

        public async Task<Response> UpdateAsync(User Entity)
        {
            try
            {
                var User = await GetByIdAsync(Entity.UserId);
                if (User == null)
                    return new Response { Success = false, Message = "User not found" };

                dbContext.Users.Update(User);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{Entity.UserId} updated succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while updating the User");
                return new Response { Success = false, Message = $"Error occured while updating the User {ex.Message}" };
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching all Users");
                return null!;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var User = await dbContext.Users.FindAsync(id);
                return User ?? null!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching all Users");
                return null!;
            }
        }

    }
}
