using ApiDemoday.DTOs.Usuario;
using ApiDemoday.Data;
using ApiDemoday.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ApiDemoday.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly RailwayContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(RailwayContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("cadastro")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioExibicaoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDto usuarioCadastroDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.EmailUsuario == usuarioCadastroDto.EmailUsuario))
            {
                return BadRequest("Este e-mail já está em uso.");
            }

            var usuario = _mapper.Map<Usuario>(usuarioCadastroDto);

            usuario.TipoUsuario = "Comum";
            usuario.DataCadastroUsuario = DateOnly.FromDateTime(DateTime.UtcNow);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var usuarioExibicao = _mapper.Map<UsuarioExibicaoDto>(usuario);

            return CreatedAtAction(nameof(GetUsuarioPorId), new { id = usuario.IdUsuario }, usuarioExibicao);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.EmailUsuario == usuarioLoginDto.EmailUsuario);

            if (usuario == null || usuario.SenhaUsuario != usuarioLoginDto.SenhaUsuario)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var token = GerarTokenJwt(usuario); 

            return Ok(new { token });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioExibicaoDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var usuariosDto = _mapper.Map<List<UsuarioExibicaoDto>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioExibicaoDto>> GetUsuarioPorId(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = _mapper.Map<UsuarioExibicaoDto>(usuario);
            return Ok(usuarioDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GerarTokenJwt(Usuario usuario)
        {
            return $"TOKEN_SIMULADO_PARA_{usuario.EmailUsuario}_ID_{usuario.IdUsuario}_ROLE_{usuario.TipoUsuario}";
        }
    }
}