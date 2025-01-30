using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Authentication.Application.DTOs;
using Authentication.Domain;
using ProductService.Application.Interfaces;

namespace Authentication.Application.Interfaces
{
    public interface IAccountRepository<T> where T : class
    {
        public Task<Response> CreateAsync(T Entity);
        // adding a new entity to the database
        public Task<Response> UpdateAsync(T Entity);
        //Updates an existing entity in database
        public Task<Response> DeleteAsync(T Entity);
        // Deletes an existing entity in database
        public Task<IEnumerable<T>> GetAllAsync();
        // Retrieves all entities from database
        public Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        // to query data asynchronously based on a condition or filter

        public Task<T> GetByIdAsync(int id);
        // Retrieve by Id

        public Task<T> GetByUserEmail(string email);

    }
}
