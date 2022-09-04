
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
        Task<IEnumerable<Usuario>> PaginacaoUsuarioProc(Pagination pagination);
        Task<bool> PasswordSignInAsync(string email, string senha);
        Task<Usuario> GetUsuarioAuthenticate(string email, string senha);
    }
    
}
