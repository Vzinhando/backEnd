// COPIE E COLE TUDO A PARTIR DAQUI
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
                name: "avaliacoes",
                columns: table => new
                {
                    id_avaliacao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao_avaliacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    estrelas_avaliacao = table.Column<decimal>(type: "numeric(3,1)", nullable: false),
                    data_avaliacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avaliacoes", x => x.id_avaliacao);
                });

            migrationBuilder.CreateTable(
                name: "cupom_descontos",
                columns: table => new
                {
                    id_cupom = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_cupom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    tipo_cupom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    porcentagem_desconto_cupom = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    status_cupom = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    data_validade_cupom = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cupom_descontos", x => x.id_cupom);
                });

            migrationBuilder.CreateTable(
                name: "planos",
                columns: table => new
                {
                    id_plano = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_plano = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    valor_plano = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    status_plano = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planos", x => x.id_plano);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome_usuario = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email_usuario = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    senha_usuario = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tipo_usuario = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    data_cadastro_usuario = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "NOW()"),
                    data_nascimento_usuario = table.Column<DateOnly>(type: "date", nullable: true),
                    sexo_usuario = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    cep_usuario = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    endereco_usuario = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cidade_usuario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    telefone_usuario = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "assinatura_planos",
                columns: table => new
                {
                    id_assinatura_plano = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_plano = table.Column<int>(type: "integer", nullable: false),
                    data_assinatura = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    data_expiracao = table.Column<DateOnly>(type: "date", nullable: true),
                    status_assinatura = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assinatura_planos", x => x.id_assinatura_plano);
                    table.ForeignKey(
                        name: "fk_assinatura_planos_planos_id_plano",
                        column: x => x.id_plano,
                        principalTable: "planos",
                        principalColumn: "id_plano",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assinatura_planos_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "perfil_usuarios",
                columns: table => new
                {
                    id_perfil = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    nome_perfil = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    categoria_perfil = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    descricao_perfil = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    foto_perfil_url = table.Column<string>(type: "text", nullable: true),
                    rede_social_perfil = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    tipo_local_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_perfil_usuarios", x => x.id_perfil);
                    table.ForeignKey(
                        name: "fk_perfil_usuarios_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contratacao_servicos",
                columns: table => new
                {
                    id_contratacao_servico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario_contratante = table.Column<int>(type: "integer", nullable: false),
                    id_perfil_contratado = table.Column<int>(type: "integer", nullable: false),
                    data_contratacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    valor_combinado = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    status_contratacao = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contratacao_servicos", x => x.id_contratacao_servico);
                    table.ForeignKey(
                        name: "fk_contratacao_servicos_perfil_usuarios_id_perfil_contratado",
                        column: x => x.id_perfil_contratado,
                        principalTable: "perfil_usuarios",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contratacao_servicos_usuarios_id_usuario_contratante",
                        column: x => x.id_usuario_contratante,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servico_oferecidos",
                columns: table => new
                {
                    id_servico_oferecido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_perfil = table.Column<int>(type: "integer", nullable: false),
                    nome_servico = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descricao_servico = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    preco_base = table.Column<decimal>(type: "numeric(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servico_oferecidos", x => x.id_servico_oferecido);
                    table.ForeignKey(
                        name: "fk_servico_oferecidos_perfil_usuarios_id_perfil",
                        column: x => x.id_perfil,
                        principalTable: "perfil_usuarios",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_avalia_perfis",
                columns: table => new
                {
                    id_usuario_avaliador = table.Column<int>(type: "integer", nullable: false),
                    id_perfil_avaliado = table.Column<int>(type: "integer", nullable: false),
                    id_avaliacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario_avalia_perfis", x => new { x.id_usuario_avaliador, x.id_perfil_avaliado });
                    table.ForeignKey(
                        name: "fk_usuario_avalia_perfis_avaliacoes_id_avaliacao",
                        column: x => x.id_avaliacao,
                        principalTable: "avaliacoes",
                        principalColumn: "id_avaliacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_avalia_perfis_perfil_usuarios_id_perfil_avaliado",
                        column: x => x.id_perfil_avaliado,
                        principalTable: "perfil_usuarios",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuario_avalia_perfis_usuarios_id_usuario_avaliador",
                        column: x => x.id_usuario_avaliador,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pagamentos",
                columns: table => new
                {
                    id_pagamento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_contratacao_servico = table.Column<int>(type: "integer", nullable: true),
                    id_assinatura_plano = table.Column<int>(type: "integer", nullable: true),
                    id_cupom = table.Column<int>(type: "integer", nullable: true),
                    valor_bruto = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    valor_desconto = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    valor_final = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    forma_pagamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    id_transacao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status_pagamento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    data_pagamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pagamentos", x => x.id_pagamento);
                    table.ForeignKey(
                        name: "fk_pagamentos_assinatura_planos_id_assinatura_plano",
                        column: x => x.id_assinatura_plano,
                        principalTable: "assinatura_planos",
                        principalColumn: "id_assinatura_plano");
                    table.ForeignKey(
                        name: "fk_pagamentos_contratacao_servicos_id_contratacao_servico",
                        column: x => x.id_contratacao_servico,
                        principalTable: "contratacao_servicos",
                        principalColumn: "id_contratacao_servico");
                    table.ForeignKey(
                        name: "fk_pagamentos_cupom_descontos_id_cupom",
                        column: x => x.id_cupom,
                        principalTable: "cupom_descontos",
                        principalColumn: "id_cupom");
                });

            migrationBuilder.CreateIndex(
                name: "ix_assinatura_planos_id_plano",
                table: "assinatura_planos",
                column: "id_plano");

            migrationBuilder.CreateIndex(
                name: "ix_assinatura_planos_id_usuario",
                table: "assinatura_planos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_contratacao_servicos_id_perfil_contratado",
                table: "contratacao_servicos",
                column: "id_perfil_contratado");

            migrationBuilder.CreateIndex(
                name: "ix_contratacao_servicos_id_usuario_contratante",
                table: "contratacao_servicos",
                column: "id_usuario_contratante");

            migrationBuilder.CreateIndex(
                name: "ix_cupom_descontos_codigo_cupom",
                table: "cupom_descontos",
                column: "codigo_cupom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pagamentos_id_assinatura_plano",
                table: "pagamentos",
                column: "id_assinatura_plano");

            migrationBuilder.CreateIndex(
                name: "ix_pagamentos_id_contratacao_servico",
                table: "pagamentos",
                column: "id_contratacao_servico");

            migrationBuilder.CreateIndex(
                name: "ix_pagamentos_id_cupom",
                table: "pagamentos",
                column: "id_cupom");

            migrationBuilder.CreateIndex(
                name: "ix_perfil_usuarios_id_usuario",
                table: "perfil_usuarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_servico_oferecidos_id_perfil",
                table: "servico_oferecidos",
                column: "id_perfil");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_email_usuario",
                table: "usuarios",
                column: "email_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuario_avalia_perfis_id_avaliacao",
                table: "usuario_avalia_perfis",
                column: "id_avaliacao");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_avalia_perfis_id_perfil_avaliado",
                table: "usuario_avalia_perfis",
                column: "id_perfil_avaliado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pagamentos");

            migrationBuilder.DropTable(
                name: "servico_oferecidos");

            migrationBuilder.DropTable(
                name: "usuario_avalia_perfis");

            migrationBuilder.DropTable(
                name: "assinatura_planos");

            migrationBuilder.DropTable(
                name: "contratacao_servicos");

            migrationBuilder.DropTable(
                name: "cupom_descontos");

            migrationBuilder.DropTable(
                name: "avaliacoes");

            migrationBuilder.DropTable(
                name: "planos");

            migrationBuilder.DropTable(
                name: "perfil_usuarios");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}