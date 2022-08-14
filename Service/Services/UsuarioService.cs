﻿using Domain.Helpers;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IRepository<Usuario> repository, IUsuarioRepository usuarioRepository) : base(repository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> OrdernarUsuario()
        {
            return await _usuarioRepository.OrdernarUsuario();
        }

        public async Task<IEnumerable<Usuario>> PaginacaoUsuario(Pagination pagination)
        {
            return await _usuarioRepository.PaginacaoUsuario(pagination);
        }
        public IEnumerable<Usuario> PaginacaoUsuarioProc(Pagination pagination)
        {
            return _usuarioRepository.PaginacaoUsuarioProc(pagination);
        }
    }
}
