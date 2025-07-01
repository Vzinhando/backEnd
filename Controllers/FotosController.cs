using Microsoft.AspNetCore.Mvc;
using ApiDemoday.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiDemoday.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class FotosController : ControllerBase
    {
        private readonly RailwayContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FotosController(RailwayContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("{id}/foto")]
        public async Task<IActionResult> UploadFotoUsuario(int id, IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("Nenhum arquivo de foto foi enviado.");
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (!string.IsNullOrEmpty(usuario.FotoUsuario))
            {
                var caminhoFotoAntiga = Path.Combine(_hostEnvironment.WebRootPath, "imagens", usuario.FotoUsuario);
                if (System.IO.File.Exists(caminhoFotoAntiga))
                {
                    System.IO.File.Delete(caminhoFotoAntiga);
                }
            }
            var extensao = Path.GetExtension(foto.FileName);
            var nomeArquivoUnico = $"{Guid.NewGuid()}{extensao}";

            var caminhoParaSalvar = Path.Combine(_hostEnvironment.WebRootPath, "imagens", nomeArquivoUnico);

            Directory.CreateDirectory(Path.GetDirectoryName(caminhoParaSalvar));

            using (var stream = new FileStream(caminhoParaSalvar, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
            usuario.FotoUsuario = nomeArquivoUnico;
            await _context.SaveChangesAsync();

            return Ok(new { fotoPath = $"/imagens/{nomeArquivoUnico}" });
        }

        [HttpDelete("{id}/foto")]
        public async Task<IActionResult> DeletarFotoUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (string.IsNullOrEmpty(usuario.FotoUsuario))
            {
                return BadRequest("O usuário não possui uma foto para deletar.");
            }

            var caminhoFoto = Path.Combine(_hostEnvironment.WebRootPath, "imagens", usuario.FotoUsuario);
            if (System.IO.File.Exists(caminhoFoto))
            {
                System.IO.File.Delete(caminhoFoto);
            }

            usuario.FotoUsuario = null;
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}