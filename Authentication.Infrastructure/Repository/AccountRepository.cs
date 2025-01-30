using System.Linq.Expressions;
using Authentication.Application.Interfaces;
using Authentication.Domain;
using Authentication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;

namespace Authentication.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository<Account>
    {
        private AuthenticationDbContext dbContext;
        private ILogger<AccountRepository> logger;
        public AccountRepository(AuthenticationDbContext _dbContext, ILogger<AccountRepository> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
        }
        public async Task<Response> CreateAsync(Account Entity)
        {
            try
            {
                await dbContext.Accounts.AddAsync(Entity);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{Entity.Name} created succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while creating the Account");
                return new Response { Success = false, Message = "Error occured while creating the Account" };
            }

        }

        public async Task<Response> DeleteAsync(Account Entity)
        {
            try
            {
                var Account = await GetByIdAsync(Entity.UserId);
                if (Account == null)
                    return new Response { Success = false, Message = "Account not found" };

                dbContext.Accounts.Remove(Account);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{Entity.Name} deleted succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while deleting the Account");
                return new Response { Success = false, Message = "Error occured while deleting the Account" };
            }
        }

        public async Task<Response> UpdateAsync(Account Entity)
        {
            try
            {
                var Account = await GetByIdAsync(Entity.UserId);
                if (Account == null)
                    return new Response { Success = false, Message = "Account not found" };

                dbContext.Accounts.Update(Account);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{Entity.Name} updated succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while updating the Account");
                return new Response { Success = false, Message = "Error occured while updating the Account" };
            }
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            try
            {
                return await dbContext.Accounts.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching all Accounts");
                return null!;
            }
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            try
            {
                var Account = await dbContext.Accounts.FindAsync(id);
                return Account ?? null!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching all Accounts");
                return null!;
            }
        }

        public async Task<Account> GetAsync(Expression<Func<Account, bool>> predicate)
        {
            try
            {
                var Account = await dbContext.Accounts.Where(predicate).FirstOrDefaultAsync();
                return Account ?? null!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while retrieving all Accounts");
                return null!;
            }
        }

        public async Task<Account> GetByUserEmail(string email)
        {
            var Account = await dbContext.Accounts.FirstOrDefaultAsync(u => u.Email == email);
            return Account ?? null!;
        }
    }
}
