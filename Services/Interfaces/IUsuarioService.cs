using BackEndDemoday.DTOs.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUsuarioService
{
    Task<UsuarioDto> ObterPorIdAsync(int id);
    Task<IEnumerable<UsuarioDto>> ObterTodosAsync();
    Task<UsuarioDto> CriarAsync(CriarUsuarioDto criarUsuarioDto);
    Task<bool> AtualizarAsync(int id, AtualizarUsuarioDto atualizarUsuarioDto);
    Task<bool> DeletarAsync(int id);
}