using Vizinhando.Models;
using Vizinhando.Models.Dtos;

namespace Vizinhando.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> ListarUsuario();
        Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int idUsuario);
        Task<ResponseModel<UsuarioModel>> CriarUsuario(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<ResponseModel<UsuarioModel>> AutenticarUsuario(LoginDto loginDto);
        Task<ResponseModel<UsuarioModel>> EditarUsuario(int idUsuario, UsuarioEdicaoDto usuarioEdicaoDto);
        Task<ResponseModel<UsuarioModel>> DeletarUsuario(int idUsuario);
    }
}
