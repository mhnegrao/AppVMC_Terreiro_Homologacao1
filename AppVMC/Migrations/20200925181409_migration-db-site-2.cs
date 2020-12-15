using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppVMC.Migrations
{
    public partial class migrationdbsite2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaixaId",
                table: "LancamentosCaixa",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "LancamentosCaixa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DescricaoLancamento",
                table: "LancamentosCaixa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdMovimento",
                table: "LancamentosCaixa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoLancto",
                table: "LancamentosCaixa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorLancto",
                table: "LancamentosCaixa",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataMovimento",
                table: "FluxoCaixa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "FluxoCaixa",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoAnterior",
                table: "FluxoCaixa",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoAtual",
                table: "FluxoCaixa",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosCaixa_CaixaId",
                table: "LancamentosCaixa",
                column: "CaixaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LancamentosCaixa_FluxoCaixa_CaixaId",
                table: "LancamentosCaixa",
                column: "CaixaId",
                principalTable: "FluxoCaixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LancamentosCaixa_FluxoCaixa_CaixaId",
                table: "LancamentosCaixa");

            migrationBuilder.DropIndex(
                name: "IX_LancamentosCaixa_CaixaId",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "CaixaId",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "DescricaoLancamento",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "IdMovimento",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "TipoLancto",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "ValorLancto",
                table: "LancamentosCaixa");

            migrationBuilder.DropColumn(
                name: "DataMovimento",
                table: "FluxoCaixa");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "FluxoCaixa");

            migrationBuilder.DropColumn(
                name: "SaldoAnterior",
                table: "FluxoCaixa");

            migrationBuilder.DropColumn(
                name: "SaldoAtual",
                table: "FluxoCaixa");
        }
    }
}
