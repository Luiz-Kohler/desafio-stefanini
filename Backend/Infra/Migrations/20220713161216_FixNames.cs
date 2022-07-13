using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class FixNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Pessoa",
                newName: "CRIADO_EM");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "Pessoa",
                newName: "ATIVO");

            migrationBuilder.RenameColumn(
                name: "ultima_atualizacao_em",
                table: "Pessoa",
                newName: "ULTIMA_ATUALZIACAO_EM");

            migrationBuilder.RenameColumn(
                name: "criado_em",
                table: "Cidade",
                newName: "CRIADO_EM");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "Cidade",
                newName: "ATIVO");

            migrationBuilder.RenameColumn(
                name: "ultima_atualizacao_em",
                table: "Cidade",
                newName: "ULTIMA_ATUALZIACAO_EM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CRIADO_EM",
                table: "Pessoa",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "ATIVO",
                table: "Pessoa",
                newName: "ativo");

            migrationBuilder.RenameColumn(
                name: "ULTIMA_ATUALZIACAO_EM",
                table: "Pessoa",
                newName: "ultima_atualizacao_em");

            migrationBuilder.RenameColumn(
                name: "CRIADO_EM",
                table: "Cidade",
                newName: "criado_em");

            migrationBuilder.RenameColumn(
                name: "ATIVO",
                table: "Cidade",
                newName: "ativo");

            migrationBuilder.RenameColumn(
                name: "ULTIMA_ATUALZIACAO_EM",
                table: "Cidade",
                newName: "ultima_atualizacao_em");
        }
    }
}
