using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    UF = table.Column<string>(type: "CHAR(2)", nullable: false),
                    criado_em = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ultima_atualizacao_em = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    CPF = table.Column<string>(type: "CHAR(11)", nullable: false),
                    Data_Nascimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CidadeId = table.Column<long>(type: "bigint", nullable: false),
                    criado_em = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ultima_atualizacao_em = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoa_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_Id",
                table: "Cidade",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CidadeId",
                table: "Pessoa",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Id",
                table: "Pessoa",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Cidade");
        }
    }
}
