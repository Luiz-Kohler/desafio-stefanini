using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class CriandoIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pessoa_Id",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Cidade_Id",
                table: "Cidade");

            migrationBuilder.CreateIndex(
                name: "ix_cpf_ativo",
                table: "Pessoa",
                columns: new[] { "CPF", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_id_ativo1",
                table: "Pessoa",
                columns: new[] { "Id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_id_ativo",
                table: "Cidade",
                columns: new[] { "Id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_nome_uf_ativo",
                table: "Cidade",
                columns: new[] { "Nome", "UF", "ativo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_cpf_ativo",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "ix_id_ativo1",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "ix_id_ativo",
                table: "Cidade");

            migrationBuilder.DropIndex(
                name: "ix_nome_uf_ativo",
                table: "Cidade");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Id",
                table: "Pessoa",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_Id",
                table: "Cidade",
                column: "Id");
        }
    }
}
