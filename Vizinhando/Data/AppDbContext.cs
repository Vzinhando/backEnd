using Microsoft.EntityFrameworkCore;
using Vizinhando.Models;

namespace Vizinhando.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // 
        {
        }


        public DbSet<UsuarioModel> Usuarios { get; set; } // Criando tabela 
    }
}
