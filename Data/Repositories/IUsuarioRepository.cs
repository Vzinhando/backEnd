using BackEndDemoday.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUsuarioRepository
{
    Task<Usuario> ObterPorIdAsync(int id);
    Task<Usuario> ObterPorEmailAsync(string email);
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task<Usuario> AdicionarAsync(Usuario usuario);
    void Atualizar(Usuario usuario);
    void Remover(Usuario usuario);
    Task<bool> SalvarTudoAsync();
}