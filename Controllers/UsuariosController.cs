using BackEndDemoday.DTOs;
using BackEndDemoday.DTOs.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(int id) 
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] CriarUsuarioDto criarUsuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var novoUsuario = await _usuarioService.CriarAsync(criarUsuarioDto);
        if (novoUsuario == null)
        {
            return BadRequest("O e-mail informado já está em uso.");
        }

        return CreatedAtAction(nameof(GetUsuario), new { id = novoUsuario.IdUsuario }, novoUsuario);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();
        return Ok(usuarios);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] AtualizarUsuarioDto atualizarUsuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resultado = await _usuarioService.AtualizarAsync(id, atualizarUsuarioDto);
        if (!resultado)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var resultado = await _usuarioService.DeletarAsync(id);
        if (!resultado)
        {
            return NotFound();
        }

        return NoContent();
    }
}