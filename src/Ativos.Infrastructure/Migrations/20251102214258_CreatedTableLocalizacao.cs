using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ativos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTableLocalizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_Localizacao_id_localizacao",
                table: "Ativos",
                column: "id_localizacao",
                principalTable: "Localizacao",
                principalColumn: "id_localizacao",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ativos_id_localizacao",
                table: "Ativos");

            migrationBuilder.AddColumn<string>(
                name: "Computador",
                table: "Ativos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Notebook",
                table: "Ativos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
