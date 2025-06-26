using System;
using System.Collections.Generic;
using BackEndDemoday.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Data;

public partial class SeuDbContext : DbContext
{
    public SeuDbContext(DbContextOptions<SeuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssinaturaPlano> AssinaturaPlanos { get; set; }

    public virtual DbSet<Avaliacao> Avaliacaos { get; set; }

    public virtual DbSet<ContratacaoServico> ContratacaoServicos { get; set; }

    public virtual DbSet<CupomDesconto> CupomDescontos { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<PerfilUsuario> PerfilUsuarios { get; set; }

    public virtual DbSet<Plano> Planos { get; set; }

    public virtual DbSet<ServicoOferecido> ServicoOferecidos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssinaturaPlano>(entity =>
        {
            entity.HasKey(e => e.IdAssinaturaPlano).HasName("PK__Assinatu__0D8DAB1EC7C70527");

            // ALTERADO DE getdate() PARA NOW()
            entity.Property(e => e.DataAssinatura).HasDefaultValueSql("NOW()");

            entity.HasOne(d => d.IdPlanoNavigation).WithMany(p => p.AssinaturaPlanos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assinatura_Plano");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AssinaturaPlanos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assinatura_Usuario");
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao).HasName("PK__Avaliaca__2A0C83123A7CE11B");

            // ALTERADO DE getdate() PARA NOW()
            entity.Property(e => e.DataAvaliacao).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<ContratacaoServico>(entity =>
        {
            entity.HasKey(e => e.IdContratacaoServico).HasName("PK__Contrata__8D88D7932FE978B5");

            // ALTERADO DE getdate() PARA NOW()
            entity.Property(e => e.DataContratacao).HasDefaultValueSql("NOW()");

            entity.HasOne(d => d.IdPerfilContratadoNavigation).WithMany(p => p.ContratacaoServicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratacao_PerfilContratado");

            entity.HasOne(d => d.IdUsuarioContratanteNavigation).WithMany(p => p.ContratacaoServicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratacao_UsuarioContratante");
        });

        modelBuilder.Entity<CupomDesconto>(entity =>
        {
            entity.HasKey(e => e.IdCupom).HasName("PK__CupomDes__9AD8BF9D617FB24F");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.IdPagamento).HasName("PK__Pagament__866960F6C4BF8598");

            entity.Property(e => e.DataPagamento).HasDefaultValueSql("NOW()");

            entity.HasOne(d => d.IdAssinaturaPlanoNavigation).WithMany(p => p.Pagamentos).HasConstraintName("FK_Pagamento_AssinaturaPlano");

            entity.HasOne(d => d.IdContratacaoServicoNavigation).WithMany(p => p.Pagamentos).HasConstraintName("FK_Pagamento_ContratacaoServico");

            entity.HasOne(d => d.IdCupomNavigation).WithMany(p => p.Pagamentos).HasConstraintName("FK_Pagamento_CupomDesconto");
        });

        modelBuilder.Entity<PerfilUsuario>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__Perfil_U__40F13B607DADE508");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PerfilUsuarios).HasConstraintName("FK_Perfil_Usuario_Usuario");
        });

        modelBuilder.Entity<Plano>(entity =>
        {
            entity.HasKey(e => e.IdPlano).HasName("PK__Plano__39B8602232BA9038");
        });

        modelBuilder.Entity<ServicoOferecido>(entity =>
        {
            entity.HasKey(e => e.IdServicoOferecido).HasName("PK__ServicoO__B42AD94B8C0AEC98");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.ServicoOferecidos).HasConstraintName("FK_Servico_Perfil");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6A1C27A4F");

            entity.Property(e => e.CepUsuario).IsFixedLength();

            // ALTERADO DE getdate() PARA NOW()
            entity.Property(e => e.DataCadastroUsuario).HasDefaultValueSql("NOW()");

            entity.Property(e => e.SexoUsuario).IsFixedLength();
        });

        modelBuilder.Entity<UsuarioAvaliaPerfil>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuarioAvaliador, e.IdPerfilAvaliado }).HasName("PK__Usuario___7112A8AE848BC099");

            entity.HasOne(d => d.IdAvaliacaoNavigation).WithMany(p => p.UsuarioAvaliaPerfils).HasConstraintName("FK_Avaliacao_Avaliacao");

            entity.HasOne(d => d.IdPerfilAvaliadoNavigation).WithMany(p => p.UsuarioAvaliaPerfils)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Perfil");

            entity.HasOne(d => d.IdUsuarioAvaliadorNavigation).WithMany(p => p.UsuarioAvaliaPerfils)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
