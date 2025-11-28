using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ativos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Chamados_id_Ativo",
                table: "Chamados",
                column: "id_Ativo");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Ativos_id_Ativo",
                table: "Chamados",
                column: "id_ativo",
                principalTable: "Ativos",
                principalColumn: "id_ativo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
