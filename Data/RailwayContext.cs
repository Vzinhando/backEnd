using System;
using System.Collections.Generic;
using ApiDemoday.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiDemoday.Data;

public partial class RailwayContext : DbContext
{
    public RailwayContext()
    {
    }

    public RailwayContext(DbContextOptions<RailwayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assinaturaplano> Assinaturaplanos { get; set; }

    public virtual DbSet<Avaliacao> Avaliacaos { get; set; }

    public virtual DbSet<Contratacaoservico> Contratacaoservicos { get; set; }

    public virtual DbSet<Cupomdesconto> Cupomdescontos { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<PerfilUsuario> PerfilUsuarios { get; set; }

    public virtual DbSet<Plano> Planos { get; set; }

    public virtual DbSet<Servicooferecido> Servicooferecidos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=yamanote.proxy.rlwy.net;port=47486;database=railway;uid=root;pwd=LHdOhaXYTdmuClUCiKAAiBgDLzyeGwzP", ServerVersion.Parse("9.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Assinaturaplano>(entity =>
        {
            entity.HasKey(e => e.IdAssinaturaPlano).HasName("PRIMARY");

            entity.ToTable("assinaturaplano");

            entity.HasIndex(e => e.IdPlano, "FK_Assinatura_Plano");

            entity.HasIndex(e => e.IdUsuario, "FK_Assinatura_Usuario");

            entity.Property(e => e.IdAssinaturaPlano).HasColumnName("idAssinaturaPlano");
            entity.Property(e => e.DataAssinatura)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("dataAssinatura");
            entity.Property(e => e.DataExpiracao).HasColumnName("dataExpiracao");
            entity.Property(e => e.IdPlano).HasColumnName("idPlano");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.StatusAssinatura)
                .HasMaxLength(30)
                .HasColumnName("statusAssinatura");

            entity.HasOne(d => d.IdPlanoNavigation).WithMany(p => p.Assinaturaplanos)
                .HasForeignKey(d => d.IdPlano)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assinatura_Plano");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Assinaturaplanos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assinatura_Usuario");
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao).HasName("PRIMARY");

            entity.ToTable("avaliacao");

            entity.Property(e => e.IdAvaliacao).HasColumnName("idAvaliacao");
            entity.Property(e => e.DataAvaliacao)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("dataAvaliacao");
            entity.Property(e => e.DescricaoAvaliacao)
                .HasMaxLength(500)
                .HasColumnName("descricaoAvaliacao");
            entity.Property(e => e.EstrelasAvaliacao)
                .HasPrecision(3, 1)
                .HasColumnName("estrelasAvaliacao");
        });

        modelBuilder.Entity<Contratacaoservico>(entity =>
        {
            entity.HasKey(e => e.IdContratacaoServico).HasName("PRIMARY");

            entity.ToTable("contratacaoservico");

            entity.HasIndex(e => e.IdPerfilContratado, "FK_Contratacao_PerfilContratado");

            entity.HasIndex(e => e.IdUsuarioContratante, "FK_Contratacao_UsuarioContratante");

            entity.Property(e => e.IdContratacaoServico).HasColumnName("idContratacaoServico");
            entity.Property(e => e.DataContratacao)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("dataContratacao");
            entity.Property(e => e.IdPerfilContratado).HasColumnName("idPerfilContratado");
            entity.Property(e => e.IdUsuarioContratante).HasColumnName("idUsuarioContratante");
            entity.Property(e => e.StatusContratacao)
                .HasMaxLength(30)
                .HasColumnName("statusContratacao");
            entity.Property(e => e.ValorCombinado)
                .HasPrecision(10, 2)
                .HasColumnName("valorCombinado");

            entity.HasOne(d => d.IdPerfilContratadoNavigation).WithMany(p => p.Contratacaoservicos)
                .HasForeignKey(d => d.IdPerfilContratado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratacao_PerfilContratado");

            entity.HasOne(d => d.IdUsuarioContratanteNavigation).WithMany(p => p.Contratacaoservicos)
                .HasForeignKey(d => d.IdUsuarioContratante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratacao_UsuarioContratante");
        });

        modelBuilder.Entity<Cupomdesconto>(entity =>
        {
            entity.HasKey(e => e.IdCupom).HasName("PRIMARY");

            entity.ToTable("cupomdesconto");

            entity.HasIndex(e => e.CodigoCupom, "UQ_Cupom_Codigo").IsUnique();

            entity.Property(e => e.IdCupom).HasColumnName("idCupom");
            entity.Property(e => e.CodigoCupom)
                .HasMaxLength(50)
                .HasColumnName("codigoCupom");
            entity.Property(e => e.DataValidadeCupom).HasColumnName("dataValidadeCupom");
            entity.Property(e => e.PorcentagemDescontoCupom)
                .HasPrecision(5, 2)
                .HasColumnName("porcentagemDescontoCupom");
            entity.Property(e => e.StatusCupom)
                .HasMaxLength(30)
                .HasColumnName("statusCupom");
            entity.Property(e => e.TipoCupom)
                .HasMaxLength(50)
                .HasColumnName("tipoCupom");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.IdPagamento).HasName("PRIMARY");

            entity.ToTable("pagamento");

            entity.HasIndex(e => e.IdAssinaturaPlano, "FK_Pagamento_AssinaturaPlano");

            entity.HasIndex(e => e.IdContratacaoServico, "FK_Pagamento_ContratacaoServico");

            entity.HasIndex(e => e.IdCupom, "FK_Pagamento_CupomDesconto");

            entity.Property(e => e.IdPagamento).HasColumnName("idPagamento");
            entity.Property(e => e.DataPagamento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dataPagamento");
            entity.Property(e => e.FormaPagamento)
                .HasMaxLength(50)
                .HasColumnName("formaPagamento");
            entity.Property(e => e.IdAssinaturaPlano).HasColumnName("idAssinaturaPlano");
            entity.Property(e => e.IdContratacaoServico).HasColumnName("idContratacaoServico");
            entity.Property(e => e.IdCupom).HasColumnName("idCupom");
            entity.Property(e => e.IdTransacao)
                .HasMaxLength(255)
                .HasColumnName("idTransacao");
            entity.Property(e => e.StatusPagamento)
                .HasMaxLength(30)
                .HasColumnName("statusPagamento");
            entity.Property(e => e.ValorBruto)
                .HasPrecision(10, 2)
                .HasColumnName("valorBruto");
            entity.Property(e => e.ValorDesconto)
                .HasPrecision(10, 2)
                .HasColumnName("valorDesconto");
            entity.Property(e => e.ValorFinal)
                .HasPrecision(10, 2)
                .HasColumnName("valorFinal");

            entity.HasOne(d => d.IdAssinaturaPlanoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdAssinaturaPlano)
                .HasConstraintName("FK_Pagamento_AssinaturaPlano");

            entity.HasOne(d => d.IdContratacaoServicoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdContratacaoServico)
                .HasConstraintName("FK_Pagamento_ContratacaoServico");

            entity.HasOne(d => d.IdCupomNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdCupom)
                .HasConstraintName("FK_Pagamento_CupomDesconto");
        });

        modelBuilder.Entity<PerfilUsuario>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PRIMARY");

            entity.ToTable("perfil_usuario");

            entity.HasIndex(e => e.IdUsuario, "FK_Perfil_Usuario_Usuario");

            entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");
            entity.Property(e => e.CategoriaPerfil)
                .HasMaxLength(50)
                .HasColumnName("categoriaPerfil");
            entity.Property(e => e.DescricaoPerfil)
                .HasMaxLength(500)
                .HasColumnName("descricaoPerfil");
            entity.Property(e => e.FotoPerfilUrl)
                .HasColumnType("text")
                .HasColumnName("fotoPerfilUrl");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NomePerfil)
                .HasMaxLength(100)
                .HasColumnName("nomePerfil");
            entity.Property(e => e.RedeSocialPerfil)
                .HasMaxLength(255)
                .HasColumnName("redeSocialPerfil");
            entity.Property(e => e.TipoLocalUsuario)
                .HasMaxLength(50)
                .HasColumnName("tipoLocalUsuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PerfilUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Perfil_Usuario_Usuario");
        });

        modelBuilder.Entity<Plano>(entity =>
        {
            entity.HasKey(e => e.IdPlano).HasName("PRIMARY");

            entity.ToTable("plano");

            entity.Property(e => e.IdPlano).HasColumnName("idPlano");
            entity.Property(e => e.StatusPlano)
                .HasMaxLength(30)
                .HasColumnName("statusPlano");
            entity.Property(e => e.TipoPlano)
                .HasMaxLength(50)
                .HasColumnName("tipoPlano");
            entity.Property(e => e.ValorPlano)
                .HasPrecision(10, 2)
                .HasColumnName("valorPlano");
        });

        modelBuilder.Entity<Servicooferecido>(entity =>
        {
            entity.HasKey(e => e.IdServicoOferecido).HasName("PRIMARY");

            entity.ToTable("servicooferecido");

            entity.HasIndex(e => e.IdPerfil, "FK_Servico_Perfil");

            entity.Property(e => e.IdServicoOferecido).HasColumnName("idServicoOferecido");
            entity.Property(e => e.DescricaoServico)
                .HasMaxLength(500)
                .HasColumnName("descricaoServico");
            entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");
            entity.Property(e => e.NomeServico)
                .HasMaxLength(100)
                .HasColumnName("nomeServico");
            entity.Property(e => e.PrecoBase)
                .HasPrecision(10, 2)
                .HasColumnName("precoBase");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.Servicooferecidos)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("FK_Servico_Perfil");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.EmailUsuario, "UQ_Usuario_Email").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.CepUsuario)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("cepUsuario");
            entity.Property(e => e.CidadeUsuario)
                .HasMaxLength(100)
                .HasColumnName("cidadeUsuario");
            entity.Property(e => e.DataCadastroUsuario)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("dataCadastroUsuario");
            entity.Property(e => e.DataNascimentoUsuario).HasColumnName("dataNascimentoUsuario");
            entity.Property(e => e.EmailUsuario).HasColumnName("emailUsuario");
            entity.Property(e => e.EnderecoUsuario)
                .HasMaxLength(255)
                .HasColumnName("enderecoUsuario");
            entity.Property(e => e.NomeUsuario)
                .HasMaxLength(150)
                .HasColumnName("nomeUsuario");
            entity.Property(e => e.SenhaUsuario)
                .HasMaxLength(255)
                .HasColumnName("senhaUsuario");
            entity.Property(e => e.SexoUsuario)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sexoUsuario");
            entity.Property(e => e.TelefoneUsuario)
                .HasMaxLength(20)
                .HasColumnName("telefoneUsuario");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(30)
                .HasColumnName("tipoUsuario");
        });

        modelBuilder.Entity<UsuarioAvaliaPerfil>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuarioAvaliador, e.IdPerfilAvaliado })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("usuario_avalia_perfil");

            entity.HasIndex(e => e.IdAvaliacao, "FK_Avaliacao_Avaliacao");

            entity.HasIndex(e => e.IdPerfilAvaliado, "FK_Avaliacao_Perfil");

            entity.Property(e => e.IdUsuarioAvaliador).HasColumnName("idUsuarioAvaliador");
            entity.Property(e => e.IdPerfilAvaliado).HasColumnName("idPerfilAvaliado");
            entity.Property(e => e.IdAvaliacao).HasColumnName("idAvaliacao");

            entity.HasOne(d => d.IdAvaliacaoNavigation).WithMany(p => p.UsuarioAvaliaPerfils)
                .HasForeignKey(d => d.IdAvaliacao)
                .HasConstraintName("FK_Avaliacao_Avaliacao");

            entity.HasOne(d => d.IdPerfilAvaliadoNavigation).WithMany(p => p.UsuarioAvaliaPerfils)
                .HasForeignKey(d => d.IdPerfilAvaliado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Perfil");

            entity.HasOne(d => d.IdUsuarioAvaliadorNavigation).WithMany(p => p.UsuarioAvaliaPerfils)
                .HasForeignKey(d => d.IdUsuarioAvaliador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
