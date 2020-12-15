using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppTVMC.Migrations
{
    public partial class primeiramigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cep = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Uf = table.Column<string>(nullable: true),
                    Gps = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FluxoCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Cancelado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoCaixa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LancamentosCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Cancelado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentosCaixa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Cancelado = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PasswordConfirm = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IdCategoria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Cancelado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LoginPassword = table.Column<string>(nullable: true),
                    EnderecoId = table.Column<int>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    IsMensalista = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensalidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Cancelado = table.Column<bool>(nullable: false),
                    IdFiliado = table.Column<int>(nullable: false),
                    ValorMensalidade = table.Column<decimal>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    Pago = table.Column<bool>(nullable: false),
                    ValorPago = table.Column<decimal>(nullable: false),
                    NomeAfiliado = table.Column<string>(nullable: true),
                    AfiliadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensalidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensalidades_Clientes_AfiliadoId",
                        column: x => x.AfiliadoId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EnderecoId",
                table: "Clientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensalidades_AfiliadoId",
                table: "Mensalidades",
                column: "AfiliadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FluxoCaixa");

            migrationBuilder.DropTable(
                name: "LancamentosCaixa");

            migrationBuilder.DropTable(
                name: "Mensalidades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
