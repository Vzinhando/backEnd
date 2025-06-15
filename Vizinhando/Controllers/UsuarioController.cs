using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vizinhando.Models;
using Vizinhando.Services.Usuario;

namespace Vizinhando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet("ListarUsuario")]
        public async Task<ActionResult<ResponseModel<List<UsuarioModel>>>> ListarUsuario()
        {
            var usuario = await _usuarioInterface.ListarUsuario();
            return Ok(usuario);
        }
    }
}
