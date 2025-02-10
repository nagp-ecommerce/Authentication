using System;
using System.Collections.Generic;
using ProductService.Application.Interfaces;

namespace Authentication.Application.Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        public Task<Response> CreateAsync(T Entity);
        // adding a new entity to the database
        public Task<Response> UpdateAsync(T Entity);
        //Updates an existing entity in database
        public Task<Response> DeleteAsync(T Entity);
        // Deletes an existing entity in database
        public Task<IEnumerable<T>> GetAllAsync();
        // Retrieves all entities from database

    }
}
