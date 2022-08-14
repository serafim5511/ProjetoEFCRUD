using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IServices<T> where T : class
    {
        Task AddEntityAsync(T Objeto);
        Task<int?> AddEntityAndReturnIdAsync(T Objeto);
        Task UpdateEntityAsync(T Objeto);
        Task DeleteEntityAsync(T Objeto);
        //Task<IEnumerable<T>> GetSingleEntityAsync(int Id);
        Task<T> GetSingleEntityAsync(int Id);
        Task<IEnumerable<T>> List();
    }
}
