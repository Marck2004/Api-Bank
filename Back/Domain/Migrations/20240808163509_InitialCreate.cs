using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuenta_Usuarios",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Account");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Account",
                column: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts",
                table: "Users",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameTable(
                name: "Transacction",
                newName: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
