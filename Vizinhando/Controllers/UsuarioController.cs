using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vizinhando.Models;
using Vizinhando.Models.Dtos;
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

        [HttpGet("BuscarUsuarioPorId/{idUsuario}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> BuscarUsuarioPorId(int idUsuario)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarioPorId(idUsuario);
            return Ok(usuarios);
        }
        [HttpPost("criarUsuario")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> CriarUsuario([FromBody] UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var resposta = await _usuarioInterface.CriarUsuario(usuarioCriacaoDto);
            if (resposta.Status)
            {
                return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = resposta.Dados.IdUsuario }, resposta);
            }
            return BadRequest(resposta);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> Login([FromBody] LoginDto loginDto)
        {
            var resposta = await _usuarioInterface.AutenticarUsuario(loginDto);

            if (resposta.Dados == null)
            {
                return Unauthorized(resposta);
            }

            return Ok(resposta);
        }
        [HttpPut("editarUsuario/{idUsuario}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> EditarUsuario(int idUsuario, [FromBody] UsuarioEdicaoDto usuarioEdicaoDto)
        {
            var resposta = await _usuarioInterface.EditarUsuario(idUsuario, usuarioEdicaoDto);

            if (resposta.Dados == null)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }
        [HttpDelete("deletarUsuario/{idUsuario}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> DeletarUsuario(int idUsuario)
        {
            var resposta = await _usuarioInterface.DeletarUsuario(idUsuario);

            if (resposta.Status)
            {
                return Ok(resposta);
            }

            return NotFound(resposta);
        }
    }
}
