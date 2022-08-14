using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService : IServices<Usuario>
    {
        Task<IEnumerable<Usuario>> OrdernarUsuario(); 
        Task<IEnumerable<Usuario>> PaginacaoUsuario(Pagination pagination); 
        IEnumerable<Usuario> PaginacaoUsuarioProc(Pagination pagination);


    }
}
