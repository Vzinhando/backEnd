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

        [HttpDelete("{id}/foto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletarFotoUsuario(int id)
        {
            // 1. Encontra o usuário no banco
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // 2. Verifica se existe uma URL de foto para deletar
            if (string.IsNullOrEmpty(usuario.FotoUsuario))
            {
                return BadRequest("O usuário não possui uma foto para deletar.");
            }

            // 3. Extrai o "Public ID" da URL da imagem
            // Ex: de "https://.../upload/v123/nome_arquivo.jpg" para "nome_arquivo"
            var uri = new Uri(usuario.FotoUsuario);
            var publicId = Path.GetFileNameWithoutExtension(uri.Segments.Last());

            // 4. Cria os parâmetros de deleção para o Cloudinary
            var deletionParams = new DeletionParams(publicId);

            // 5. Envia a requisição de deleção para o Cloudinary
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            if (deletionResult.Error != null)
            {
                // Se a deleção no Cloudinary falhar, retorna um erro
                // (Isso pode acontecer se a imagem já foi deletada, por exemplo)
                // Mesmo assim, vamos continuar para limpar o banco de dados.
                Console.WriteLine($"Erro ao deletar imagem do Cloudinary: {deletionResult.Error.Message}");
            }

            // 6. Remove a referência da URL do banco de dados
            usuario.FotoUsuario = null;
            await _context.SaveChangesAsync();

            // 7. Retorna sucesso
            return NoContent();
        }
    }
}