using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogUNAH.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "security",
                table: "users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "security",
                table: "users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "resfesh_token",
                schema: "security",
                table: "users",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "resfesh_token_expire",
                schema: "security",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resfesh_token",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resfesh_token_expire",
                schema: "security",
                table: "users");
        }
    }
}
