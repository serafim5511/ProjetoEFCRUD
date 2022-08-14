using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BaseService<T> : IServices<T> where T : class
    {
        public IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<int?> AddEntityAndReturnIdAsync(T Objeto)
        {
            return await _repository.AddEntityAndReturnIdAsync(Objeto);
        }

        public async Task AddEntityAsync(T Objeto)
        {
            await _repository.AddEntityAsync(Objeto);
        }

        public async Task DeleteEntityAsync(T Objeto)
        {
            await _repository.DeleteEntityAsync(Objeto);
        }

        public async Task<T> GetSingleEntityAsync(int Id)
        {
            return await _repository.GetSingleEntityAsync(Id);
        }

        public async Task<IEnumerable<T>> List()
        {
            return await _repository.List();
        }

        public async Task UpdateEntityAsync(T Objeto)
        {
            await _repository.UpdateEntityAsync(Objeto);
        }
    }
}
