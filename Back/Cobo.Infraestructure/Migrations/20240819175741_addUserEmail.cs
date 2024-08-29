using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addUserEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(100)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Users",
                table: "Email");
        }
    }
}
