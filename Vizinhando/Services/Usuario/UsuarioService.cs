using Microsoft.EntityFrameworkCore;
using Vizinhando.Data;
using Vizinhando.Models;
using Vizinhando.Models.Dtos;
using BCrypt.Net;

namespace Vizinhando.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<ResponseModel<UsuarioModel>> BuscarUsuarioPorId(int idUsuario)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.IdUsuario == idUsuario);

                if (usuario == null)
                {
                    resposta.Mensagem = "Nenhum usuário localizado com o ID informado.";
                }
                else
                {
                    resposta.Dados = usuario;
                    resposta.Mensagem = "Usuário localizado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Ocorreu um erro: {ex.Message}";
                resposta.Status = false;
            }
            return resposta;
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
        public async Task<ResponseModel<UsuarioModel>> CriarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            ResponseModel<UsuarioModel> resposta = new ResponseModel<UsuarioModel>();
            try
            {
                var usuario = new UsuarioModel()
                {
                    NomeUsuario = usuarioCriacaoDto.NomeUsuario,
                    EmailUsuario = usuarioCriacaoDto.EmailUsuario,
                    SenhaUsuario = BCrypt.Net.BCrypt.HashPassword(usuarioCriacaoDto.SenhaUsuario),
                    cepUsuario = usuarioCriacaoDto.cepUsuario
                };

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                resposta.Dados = usuario;
                resposta.Mensagem = "Usuário criado com sucesso!";

            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Ocorreu um erro ao criar o usuário: {ex.Message}";
            }

            return resposta;
        }
        public async Task<ResponseModel<UsuarioModel>> AutenticarUsuario(LoginDto loginDto)
        {
            var resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuarioBanco = await _context.Usuarios.FirstOrDefaultAsync(u => u.EmailUsuario == loginDto.EmailUsuario);

                if (usuarioBanco == null)
                {
                    resposta.Mensagem = "Email ou senha incorretos."; 
                    return resposta;
                }

                if (BCrypt.Net.BCrypt.Verify(loginDto.SenhaDigitada, usuarioBanco.SenhaUsuario))
                {
                    resposta.Mensagem = "Usuário autenticado com sucesso!";

                   
                    usuarioBanco.SenhaUsuario = "";
                    resposta.Dados = usuarioBanco;
                }
                else
                {
                    resposta.Mensagem = "Email ou senha incorretos.";
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = $"Ocorreu um erro: {ex.Message}";
                resposta.Status = false;
            }

            return resposta;
        }
        public async Task<ResponseModel<UsuarioModel>> EditarUsuario(int idUsuario, UsuarioEdicaoDto usuarioEdicaoDto)
        {
            var resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuarioBanco = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

                if (usuarioBanco == null)
                {
                    resposta.Mensagem = "Usuário não encontrado!";
                    return resposta;
                }

                if (!string.IsNullOrEmpty(usuarioEdicaoDto.NomeUsuario))
                {
                    usuarioBanco.NomeUsuario = usuarioEdicaoDto.NomeUsuario;
                }

                if (!string.IsNullOrEmpty(usuarioEdicaoDto.EmailUsuario))
                {
                    usuarioBanco.EmailUsuario = usuarioEdicaoDto.EmailUsuario;
                }

                if (!string.IsNullOrEmpty(usuarioEdicaoDto.cepUsuario))
                {
                    usuarioBanco.cepUsuario = usuarioEdicaoDto.cepUsuario;
                }

                if (!string.IsNullOrEmpty(usuarioEdicaoDto.SenhaUsuario))
                {
                    usuarioBanco.SenhaUsuario = BCrypt.Net.BCrypt.HashPassword(usuarioEdicaoDto.SenhaUsuario);
                }
                _context.Update(usuarioBanco);
                await _context.SaveChangesAsync();

                usuarioBanco.SenhaUsuario = "";
                resposta.Dados = usuarioBanco;
                resposta.Mensagem = "Usuário editado com sucesso!";
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Ocorreu um erro ao editar o usuário: {ex.Message}";
            }

            return resposta;
        }
        public async Task<ResponseModel<UsuarioModel>> DeletarUsuario(int idUsuario)
        {
            var resposta = new ResponseModel<UsuarioModel>();

            try
            {
                var usuarioBanco = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

                if (usuarioBanco == null)
                {
                    resposta.Mensagem = "Usuário não encontrado para deleção!";
                    return resposta;
                }

                _context.Remove(usuarioBanco);
                await _context.SaveChangesAsync();

                resposta.Mensagem = "Usuário deletado com sucesso!";
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Ocorreu um erro ao deletar o usuário: {ex.Message}";
            }

            return resposta;
        }
    }
}
