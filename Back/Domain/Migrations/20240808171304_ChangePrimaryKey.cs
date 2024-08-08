using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: true
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Account",
                table: "Account",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
