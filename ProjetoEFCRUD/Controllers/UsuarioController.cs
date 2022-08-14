using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    //[Route("apicrud/v{version:apiVersion}")]
    [ControllerName("Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService UsuarioService)
        {
            _usuarioService = UsuarioService;
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<List<Usuario>>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        public async Task<IActionResult> Get()
        {         
            return Ok( new Response<IEnumerable<Usuario>>(await _usuarioService.List()));
        }
        [HttpPost]
        [Route("insertusuario")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<string>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        public async Task<IActionResult> InsertUsuario([FromBody] Usuario user)
        {
            await _usuarioService.AddEntityAsync(user);

            return Ok(new Response<string>(null, "Inserido com sucesso", true));
        }
        [HttpDelete]
        [Route("deleteusuario")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<List<Usuario>>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        public async Task<IActionResult> Delete([FromBody] Usuario user)
        {
            await _usuarioService.DeleteEntityAsync(user);

            return Ok(new Response<string>(null, "Deletado com sucesso", true));
        }

        [HttpGet]
        [Route("ordernarusuario")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<List<Usuario>>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        public async Task<IActionResult> OrdernarUsuario()
        {        
            return Ok(new Response<IEnumerable<Usuario>>(await _usuarioService.OrdernarUsuario()));
        }

        [HttpGet]
        [Route("paginacaousuario")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<List<Usuario>>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        public async Task<IActionResult> PaginacaoUsuario([FromQuery] Pagination pagination)
        {
            return Ok(new Response<IEnumerable<Usuario>>(await _usuarioService.PaginacaoUsuario(pagination)));
        }
        [HttpGet]
        [Route("paginacaousuarioProc")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<List<Usuario>>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        public IActionResult PaginacaoUsuarioProc([FromQuery] Pagination pagination)
        {
            return Ok(new Response<IEnumerable<Usuario>>(_usuarioService.PaginacaoUsuarioProc(pagination)));
        }
    }
}
