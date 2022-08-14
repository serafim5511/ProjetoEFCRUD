using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        private readonly ContextBase _data;

        public Repository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
            _data = new ContextBase(_OptionsBuilder);
        }

        public async Task AddEntityAsync(T Objeto)
        {

            await _data.Set<T>().AddAsync(Objeto);
            await _data.SaveChangesAsync();

        }
        public async Task<int?> AddEntityAndReturnIdAsync(T Objeto)
        {
            await _data.Set<T>().AddAsync(Objeto);
            await _data.SaveChangesAsync();
            return (int?)Objeto.GetType().GetProperty("Id").GetValue(Objeto, null);

        }
        public async Task DeleteEntityAsync(T Objeto)
        {
            _data.Set<T>().Remove(Objeto);
            await _data.SaveChangesAsync();
        }

        public async Task<T> GetSingleEntityAsync(int Id)
        {
            return await _data.Set<T>().FindAsync(Id);
        }

        public async Task<IEnumerable<T>> List()
        {
            return await _data.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task UpdateEntityAsync(T Objeto)
        {
            _data.Set<T>().Update(Objeto);
            await _data.SaveChangesAsync();
        }

    }
}
