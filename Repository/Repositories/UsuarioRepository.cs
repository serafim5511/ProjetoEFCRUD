using Domain.Helpers;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        private readonly ContextBase _data;
        public UsuarioRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
            _data = new ContextBase(_OptionsBuilder);
        }

        public async Task<IEnumerable<Usuario>> OrdernarUsuario()
        {
            return await _data.Usuario.AsNoTracking().OrderByDescending(x => x.DtNasc).ToListAsync();
        }
        public async Task<IEnumerable<Usuario>> PaginacaoUsuario(Pagination pagination)
        {
            pagination.ValidatePage();
            string query = "SELECT * FROM [Usuario] ORDER BY Id OFFSET {0} rows fetch next {1} rows only";
            string queryFormat = string.Format(query, pagination.Page, pagination.PageSize);
            return await _data.Usuario.FromSqlRaw(queryFormat).ToListAsync();

        }
        public async Task<IEnumerable<Usuario>> PaginacaoUsuarioProc(Pagination pagination)
        {
            pagination.ValidatePage();

            return await _data.Usuario.FromSqlRaw("PR_Pagination @page,@pageSize", 
            new SqlParameter()
            {
                Value = pagination.Page,
                ParameterName = "@page"
            },
            new SqlParameter()
            {
                Value = pagination.PageSize,
                ParameterName = "@pageSize"
            }
                ).ToListAsync();
            //var command = _data.Database.GetDbConnection().CreateCommand();
            //command.CommandText = "PR_Pagination";
            //command.CommandType = CommandType.StoredProcedure;

            //var param = new List<SqlParameter>()
            //        {
            //            new SqlParameter()
            //            {
            //                Value = pagination.Page,
            //                ParameterName = "page"
            //            },
            //            new SqlParameter()
            //            {
            //                Value = pagination.PageSize,
            //                ParameterName = "pageSize"
            //            }
            //        };


            //param.ForEach(x => command.Parameters.Add(x));

            //var list = new List<Usuario>();

            //_data.Database.OpenConnection();

            //using (var result = command.ExecuteReader())
            //{
            //    while (result.Read())
            //    {
            //        list.Add(new Usuario()
            //        {
            //            Id = result.GetInt32(0),
            //            Nome = result.GetString(1),
            //            DtNasc = result.GetDateTime(2)
            //        });
            //    }
            //}
            //_data.Database.CloseConnection();

            //return list;

        }

        public async Task<bool> PasswordSignInAsync(string email, string senha)
        {
            return await _data.Usuario.AsNoTracking().Where(x => x.Email == email && x.Password == senha).AnyAsync();
        }
        public async Task<Usuario> GetUsuarioAuthenticate (string email, string senha)
        {
            return await _data.Usuario.AsNoTracking().Where(x => x.Email == email && x.Password == senha).SingleOrDefaultAsync();
        }
    }
}
