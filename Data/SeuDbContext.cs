using BackEndDemoday.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Data;

public partial class SeuDbContext : DbContext
{
    public SeuDbContext(DbContextOptions<SeuDbContext> options)
        : base(options)
    {
    }

    // Nomes dos DbSets corrigidos para o plural padrão em português
    public virtual DbSet<AssinaturaPlano> AssinaturaPlanos { get; set; }
    public virtual DbSet<Avaliacao> Avaliacoes { get; set; } // Corrigido de Avaliacaos
    public virtual DbSet<ContratacaoServico> ContratacaoServicos { get; set; }
    public virtual DbSet<CupomDesconto> CupomDescontos { get; set; }
    public virtual DbSet<Pagamento> Pagamentos { get; set; }
    public virtual DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
    public virtual DbSet<Plano> Planos { get; set; }
    public virtual DbSet<ServicoOferecido> ServicoOferecidos { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<UsuarioAvaliaPerfil> UsuarioAvaliaPerfis { get; set; } // Corrigido de UsuarioAvaliaPerfils

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // O método OnModelCreating agora fica muito mais limpo!
        // A convenção de nomes (snake_case) é aplicada automaticamente pelo pacote.
        // Nomes de tabelas, colunas, chaves primárias e estrangeiras são padronizados.

        // --- Configurações que AINDA SÃO NECESSÁRIAS ---

        // Configuração de valores padrão para colunas de data
        modelBuilder.Entity<AssinaturaPlano>(entity =>
        {
            entity.Property(e => e.DataAssinatura).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.Property(e => e.DataAvaliacao).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<ContratacaoServico>(entity =>
        {
            entity.Property(e => e.DataContratacao).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            // Definimos explicitamente o tipo de coluna para timestamp para garantir
            entity.Property(e => e.DataPagamento)
                  .HasColumnType("timestamp without time zone")
                  .HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.DataCadastroUsuario).HasDefaultValueSql("NOW()");

            // Definimos um índice único para o email, garantindo que não haja emails duplicados
            entity.HasIndex(e => e.EmailUsuario).IsUnique();
        });

        // Configuração da chave primária composta para a tabela de junção
        modelBuilder.Entity<UsuarioAvaliaPerfil>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuarioAvaliador, e.IdPerfilAvaliado });
        });

        // O método parcial é mantido para extensibilidade
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}