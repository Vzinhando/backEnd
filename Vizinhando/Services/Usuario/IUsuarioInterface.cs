using Vizinhando.Models;

namespace Vizinhando.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> ListarUsuario();
        Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int idUsuario);
    }
}
