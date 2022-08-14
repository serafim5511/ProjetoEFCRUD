
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<IEnumerable<Usuario>> OrdernarUsuario();
        Task<IEnumerable<Usuario>> PaginacaoUsuario(Pagination pagination);
        IEnumerable<Usuario> PaginacaoUsuarioProc(Pagination pagination);
    }
}
