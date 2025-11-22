using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ativos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class usersRelationsUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "id_usuario",
                table: "Ativos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ativos_id_usuario",
                table: "Ativos",
                column: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_Usuario_id_usuario",
                table: "Ativos",
                column: "id_usuario",
                principalTable: "Usuario",
                principalColumn: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ativos_Usuario_id_usuario",
                table: "Ativos");

            migrationBuilder.DropIndex(
                name: "IX_Ativos_id_usuario",
                table: "Ativos");

            migrationBuilder.DropColumn(
                name: "id_usuario",
                table: "Ativos");
        }
    }
}
