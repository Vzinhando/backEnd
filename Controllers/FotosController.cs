using Microsoft.AspNetCore.Mvc;
using ApiDemoday.Data;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ApiDemoday.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class FotosController : ControllerBase
    {
        private readonly RailwayContext _context;
        private readonly Cloudinary _cloudinary;

        public FotosController(RailwayContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        [HttpPost("{id}/foto")]
        public async Task<IActionResult> UploadFotoUsuario(int id, IFormFile foto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            if (foto == null || foto.Length == 0) return BadRequest("Nenhum arquivo enviado.");

            await using var stream = foto.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(foto.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                return BadRequest(uploadResult.Error.Message);
            }

            usuario.FotoUsuario = uploadResult.SecureUrl.AbsoluteUri;
            await _context.SaveChangesAsync();

            return Ok(new { fotoUrl = usuario.FotoUsuario });
        }

    }
}