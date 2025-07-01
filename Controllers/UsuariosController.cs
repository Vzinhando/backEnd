using ApiDemoday.Data;
using ApiDemoday.DTOs.Usuario;
using ApiDemoday.Models;
using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("cadastro")] // para fazer o cadastro 
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

        [HttpPost("login")] // faz o teste e tenta entrar LOGIN
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

            return Ok(new {token, usuario });
        }


        [HttpGet] // pega todas as informações da tabela
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

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usuario))] // MUDANÇA: Agora retorna o usuário
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchUsuario(int id, [FromBody] UsuarioUpdateDto dadosAtualizados)
        {
            Console.WriteLine($"[PATCH /api/usuarios/{id}] - Requisição recebida com objeto simples.");

            var usuarioNoBanco = await _context.Usuarios.FindAsync(id);
            if (usuarioNoBanco == null)
            {
                Console.WriteLine($"[PATCH] Erro: Usuário com ID {id} não encontrado.");
                return NotFound();
            }
            Console.WriteLine($"[PATCH] Usuário com ID {id} encontrado.");
            usuarioNoBanco.NomeUsuario = dadosAtualizados.NomeUsuario;
            usuarioNoBanco.CepUsuario = dadosAtualizados.CepUsuario;
            await _context.SaveChangesAsync();
            Console.WriteLine($"[PATCH] Alterações para o usuário {id} salvas no banco.");

            return Ok(usuarioNoBanco);
        }

        [HttpDelete("{id}")] // apaga dados por ID
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