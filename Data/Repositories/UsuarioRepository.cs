using Microsoft.EntityFrameworkCore;
using BackEndDemoday.Data;
using BackEndDemoday.Entities;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SeuDbContext _context;

    public UsuarioRepository(SeuDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.EmailUsuario == email);
    }

    public async Task<Usuario> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        return usuario;
    }
    public void Atualizar(Usuario usuario)
    {
        _context.Entry(usuario).State = EntityState.Modified;
    }
    public void Remover(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
    }
    public async Task<bool> SalvarTudoAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}