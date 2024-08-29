using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class changeSurname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Apellido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "Users",
                newName: "Surname");
        }
    }
}
