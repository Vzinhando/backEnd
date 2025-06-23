using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEndDemoday.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    idAvaliacao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricaoAvaliacao = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true),
                    estrelasAvaliacao = table.Column<decimal>(type: "numeric(3,1)", nullable: false),
                    dataAvaliacao = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Avaliaca__2A0C83123A7CE11B", x => x.idAvaliacao);
                });

            migrationBuilder.CreateTable(
                name: "CupomDesconto",
                columns: table => new
                {
                    idCupom = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigoCupom = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    tipoCupom = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    porcentagemDescontoCupom = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    statusCupom = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    dataValidadeCupom = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CupomDes__9AD8BF9D617FB24F", x => x.idCupom);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    idPlano = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipoPlano = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    valorPlano = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    statusPlano = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plano__39B8602232BA9038", x => x.idPlano);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomeUsuario = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    emailUsuario = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    senhaUsuario = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    tipoUsuario = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    dataCadastroUsuario = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    dataNascimentoUsuario = table.Column<DateOnly>(type: "date", nullable: true),
                    sexoUsuario = table.Column<string>(type: "character(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    cepUsuario = table.Column<string>(type: "character(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    enderecoUsuario = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    cidadeUsuario = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    telefoneUsuario = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__645723A6A1C27A4F", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "AssinaturaPlano",
                columns: table => new
                {
                    idAssinaturaPlano = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<int>(type: "integer", nullable: false),
                    idPlano = table.Column<int>(type: "integer", nullable: false),
                    dataAssinatura = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    dataExpiracao = table.Column<DateOnly>(type: "date", nullable: true),
                    statusAssinatura = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assinatu__0D8DAB1EC7C70527", x => x.idAssinaturaPlano);
                    table.ForeignKey(
                        name: "FK_Assinatura_Plano",
                        column: x => x.idPlano,
                        principalTable: "Plano",
                        principalColumn: "idPlano");
                    table.ForeignKey(
                        name: "FK_Assinatura_Usuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Perfil_Usuario",
                columns: table => new
                {
                    idPerfil = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<int>(type: "integer", nullable: false),
                    nomePerfil = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    categoriaPerfil = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    descricaoPerfil = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true),
                    fotoPerfilUrl = table.Column<string>(type: "text", unicode: false, nullable: true),
                    redeSocialPerfil = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    tipoLocalUsuario = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Perfil_U__40F13B607DADE508", x => x.idPerfil);
                    table.ForeignKey(
                        name: "FK_Perfil_Usuario_Usuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContratacaoServico",
                columns: table => new
                {
                    idContratacaoServico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuarioContratante = table.Column<int>(type: "integer", nullable: false),
                    idPerfilContratado = table.Column<int>(type: "integer", nullable: false),
                    dataContratacao = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    valorCombinado = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    statusContratacao = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contrata__8D88D7932FE978B5", x => x.idContratacaoServico);
                    table.ForeignKey(
                        name: "FK_Contratacao_PerfilContratado",
                        column: x => x.idPerfilContratado,
                        principalTable: "Perfil_Usuario",
                        principalColumn: "idPerfil");
                    table.ForeignKey(
                        name: "FK_Contratacao_UsuarioContratante",
                        column: x => x.idUsuarioContratante,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ServicoOferecido",
                columns: table => new
                {
                    idServicoOferecido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idPerfil = table.Column<int>(type: "integer", nullable: false),
                    nomeServico = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    descricaoServico = table.Column<string>(type: "character varying(500)", unicode: false, maxLength: 500, nullable: true),
                    precoBase = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServicoO__B42AD94B8C0AEC98", x => x.idServicoOferecido);
                    table.ForeignKey(
                        name: "FK_Servico_Perfil",
                        column: x => x.idPerfil,
                        principalTable: "Perfil_Usuario",
                        principalColumn: "idPerfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Avalia_Perfil",
                columns: table => new
                {
                    idUsuarioAvaliador = table.Column<int>(type: "integer", nullable: false),
                    idPerfilAvaliado = table.Column<int>(type: "integer", nullable: false),
                    idAvaliacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario___7112A8AE848BC099", x => new { x.idUsuarioAvaliador, x.idPerfilAvaliado });
                    table.ForeignKey(
                        name: "FK_Avaliacao_Avaliacao",
                        column: x => x.idAvaliacao,
                        principalTable: "Avaliacao",
                        principalColumn: "idAvaliacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Perfil",
                        column: x => x.idPerfilAvaliado,
                        principalTable: "Perfil_Usuario",
                        principalColumn: "idPerfil");
                    table.ForeignKey(
                        name: "FK_Avaliacao_Usuario",
                        column: x => x.idUsuarioAvaliador,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    idPagamento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idContratacaoServico = table.Column<int>(type: "integer", nullable: true),
                    idAssinaturaPlano = table.Column<int>(type: "integer", nullable: true),
                    idCupom = table.Column<int>(type: "integer", nullable: true),
                    valorBruto = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    valorDesconto = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    valorFinal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    formaPagamento = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    idTransacao = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    statusPagamento = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    dataPagamento = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pagament__866960F6C4BF8598", x => x.idPagamento);
                    table.ForeignKey(
                        name: "FK_Pagamento_AssinaturaPlano",
                        column: x => x.idAssinaturaPlano,
                        principalTable: "AssinaturaPlano",
                        principalColumn: "idAssinaturaPlano");
                    table.ForeignKey(
                        name: "FK_Pagamento_ContratacaoServico",
                        column: x => x.idContratacaoServico,
                        principalTable: "ContratacaoServico",
                        principalColumn: "idContratacaoServico");
                    table.ForeignKey(
                        name: "FK_Pagamento_CupomDesconto",
                        column: x => x.idCupom,
                        principalTable: "CupomDesconto",
                        principalColumn: "idCupom");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturaPlano_idPlano",
                table: "AssinaturaPlano",
                column: "idPlano");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturaPlano_idUsuario",
                table: "AssinaturaPlano",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ContratacaoServico_idPerfilContratado",
                table: "ContratacaoServico",
                column: "idPerfilContratado");

            migrationBuilder.CreateIndex(
                name: "IX_ContratacaoServico_idUsuarioContratante",
                table: "ContratacaoServico",
                column: "idUsuarioContratante");

            migrationBuilder.CreateIndex(
                name: "UQ_Cupom_Codigo",
                table: "CupomDesconto",
                column: "codigoCupom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_idAssinaturaPlano",
                table: "Pagamento",
                column: "idAssinaturaPlano");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_idContratacaoServico",
                table: "Pagamento",
                column: "idContratacaoServico");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_idCupom",
                table: "Pagamento",
                column: "idCupom");

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_Usuario_idUsuario",
                table: "Perfil_Usuario",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoOferecido_idPerfil",
                table: "ServicoOferecido",
                column: "idPerfil");

            migrationBuilder.CreateIndex(
                name: "UQ_Usuario_Email",
                table: "Usuario",
                column: "emailUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Avalia_Perfil_idAvaliacao",
                table: "Usuario_Avalia_Perfil",
                column: "idAvaliacao");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Avalia_Perfil_idPerfilAvaliado",
                table: "Usuario_Avalia_Perfil",
                column: "idPerfilAvaliado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "ServicoOferecido");

            migrationBuilder.DropTable(
                name: "Usuario_Avalia_Perfil");

            migrationBuilder.DropTable(
                name: "AssinaturaPlano");

            migrationBuilder.DropTable(
                name: "ContratacaoServico");

            migrationBuilder.DropTable(
                name: "CupomDesconto");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropTable(
                name: "Perfil_Usuario");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
