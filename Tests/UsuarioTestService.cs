using Domain.Interfaces.Repositories;
using Domain.Models;
using NSubstitute;
using Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UsuarioTestService
    {
        private readonly IUsuarioRepository _usuario;
        public UsuarioTestService()
        {
            _usuario = Substitute.For<IUsuarioRepository>();
        }
        private UsuarioService CreateService()
        {
            return new UsuarioService(_usuario);
        }
        [Fact]
        public async Task GetUsuarioAsunc_WhenValueExitsResponse()
        {
            var service = CreateService();

            var usuario = new List<Usuario>() { new Usuario() { Nome = "teste" } };

            _usuario.List().Returns(usuario);

            var result = await service.List();

            Assert.NotNull(result);
            Assert.IsType<List<Usuario>>(result);
            Assert.NotNull(result);
            Assert.NotNull(result);
        }
    }
}
