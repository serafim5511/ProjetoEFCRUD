using API.Models;
using API.Token;
using Domain.Helpers;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [Authorize]
        [Produces("application/json")]
        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Response<List<Usuario>>), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(Error), Description = "")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "testeee")]
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
        public async Task<IActionResult> PaginacaoUsuarioProc([FromQuery] Pagination pagination)
        {
            return Ok(new Response<IEnumerable<Usuario>>(await _usuarioService.PaginacaoUsuarioProc(pagination)));
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            var resultado = await
                _usuarioService.PasswordSignInAsync(login.email, login.senha);

            if (resultado)
            {
                // Recupera Usuário Logado
                var userCurrent = await _usuarioService.GetUsuarioAuthenticate(login.email, login.senha);
                var idUsuario = userCurrent.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - Canal Dev Net Core")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("idUsuario", idUsuario.ToString())
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
