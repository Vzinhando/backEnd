using Microsoft.EntityFrameworkCore;
using Vizinhando.Data;
using Vizinhando.Models;

namespace Vizinhando.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context) 
        {
            _context = context;
        }
        public Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<UsuarioModel>>> ListarUsuario()
        {
                ResponseModel<List<UsuarioModel>> resposta = new ResponseModel<List<UsuarioModel>>();
            try
            {
                var usuario = await _context.Usuarios.ToListAsync();
                resposta.Dados = usuario;
                resposta.Mensagem = "Usuario Cadastrado!";

                return resposta;
            }
            catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
