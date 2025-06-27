using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEndDemoday.Migrations
{
    /// <inheritdoc />
    public partial class V1_TheFinalSchema : Migration
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
                    dataAvaliacao = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avaliacao", x => x.idAvaliacao);
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
                    table.PrimaryKey("pk_cupom_desconto", x => x.idCupom);
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
                    table.PrimaryKey("pk_plano", x => x.idPlano);
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
                    dataCadastroUsuario = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "NOW()"),
                    dataNascimentoUsuario = table.Column<DateOnly>(type: "date", nullable: true),
                    sexoUsuario = table.Column<string>(type: "character varying(1)", unicode: false, maxLength: 1, nullable: true),
                    cepUsuario = table.Column<string>(type: "character varying(9)", unicode: false, maxLength: 9, nullable: true),
                    enderecoUsuario = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    cidadeUsuario = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    telefoneUsuario = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "AssinaturaPlano",
                columns: table => new
                {
                    idAssinaturaPlano = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<int>(type: "integer", nullable: false),
                    idPlano = table.Column<int>(type: "integer", nullable: false),
                    dataAssinatura = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "NOW()"),
                    dataExpiracao = table.Column<DateOnly>(type: "date", nullable: true),
                    statusAssinatura = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assinatura_plano", x => x.idAssinaturaPlano);
                    table.ForeignKey(
                        name: "fk_assinatura_plano_plano_id_plano",
                        column: x => x.idPlano,
                        principalTable: "Plano",
                        principalColumn: "idPlano",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assinatura_plano_usuario_id_usuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
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
                    table.PrimaryKey("pk_perfil_usuario", x => x.idPerfil);
                    table.ForeignKey(
                        name: "fk_perfil_usuario_usuario_id_usuario",
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
                    dataContratacao = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "NOW()"),
                    valorCombinado = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    statusContratacao = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contratacao_servico", x => x.idContratacaoServico);
                    table.ForeignKey(
                        name: "fk_contratacao_servico_perfil_usuario_id_perfil_contratado",
                        column: x => x.idPerfilContratado,
                        principalTable: "Perfil_Usuario",
                        principalColumn: "idPerfil",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contratacao_servico_usuario_id_usuario_contratante",
                        column: x => x.idUsuarioContratante,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
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
                    table.PrimaryKey("pk_servico_oferecido", x => x.idServicoOferecido);
                    table.ForeignKey(
                        name: "fk_servico_oferecido_perfil_usuario_id_perfil",
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
                    table.PrimaryKey("pk_usuario_avalia_perfil", x => new { x.idUsuarioAvaliador, x.idPerfilAvaliado });
                    table.ForeignKey(
                        name: "fk_usuario_avalia_perfil_avaliacao_id_avaliacao",
                        column: x => x.idAvaliacao,
                        principalTable: "Avaliacao",
                        principalColumn: "idAvaliacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_avalia_perfil_perfil_usuario_id_perfil_avaliado",
                        column: x => x.idPerfilAvaliado,
                        principalTable: "Perfil_Usuario",
                        principalColumn: "idPerfil",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_avalia_perfil_usuario_id_usuario_avaliador",
                        column: x => x.idUsuarioAvaliador,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
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
                    dataPagamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pagamento", x => x.idPagamento);
                    table.ForeignKey(
                        name: "fk_pagamento_assinatura_plano_id_assinatura_plano",
                        column: x => x.idAssinaturaPlano,
                        principalTable: "AssinaturaPlano",
                        principalColumn: "idAssinaturaPlano");
                    table.ForeignKey(
                        name: "fk_pagamento_contratacao_servico_id_contratacao_servico",
                        column: x => x.idContratacaoServico,
                        principalTable: "ContratacaoServico",
                        principalColumn: "idContratacaoServico");
                    table.ForeignKey(
                        name: "fk_pagamento_cupom_desconto_id_cupom",
                        column: x => x.idCupom,
                        principalTable: "CupomDesconto",
                        principalColumn: "idCupom");
                });

            migrationBuilder.CreateIndex(
                name: "ix_assinatura_plano_id_plano",
                table: "AssinaturaPlano",
                column: "idPlano");

            migrationBuilder.CreateIndex(
                name: "ix_assinatura_plano_id_usuario",
                table: "AssinaturaPlano",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "ix_contratacao_servico_id_perfil_contratado",
                table: "ContratacaoServico",
                column: "idPerfilContratado");

            migrationBuilder.CreateIndex(
                name: "ix_contratacao_servico_id_usuario_contratante",
                table: "ContratacaoServico",
                column: "idUsuarioContratante");

            migrationBuilder.CreateIndex(
                name: "ix_cupom_desconto_codigo_cupom",
                table: "CupomDesconto",
                column: "codigoCupom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pagamento_id_assinatura_plano",
                table: "Pagamento",
                column: "idAssinaturaPlano");

            migrationBuilder.CreateIndex(
                name: "ix_pagamento_id_contratacao_servico",
                table: "Pagamento",
                column: "idContratacaoServico");

            migrationBuilder.CreateIndex(
                name: "ix_pagamento_id_cupom",
                table: "Pagamento",
                column: "idCupom");

            migrationBuilder.CreateIndex(
                name: "ix_perfil_usuario_id_usuario",
                table: "Perfil_Usuario",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "ix_servico_oferecido_id_perfil",
                table: "ServicoOferecido",
                column: "idPerfil");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_email_usuario",
                table: "Usuario",
                column: "emailUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuario_avalia_perfil_id_avaliacao",
                table: "Usuario_Avalia_Perfil",
                column: "idAvaliacao");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_avalia_perfil_id_perfil_avaliado",
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
