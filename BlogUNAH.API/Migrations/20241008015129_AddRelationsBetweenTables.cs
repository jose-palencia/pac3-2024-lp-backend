using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogUNAH.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsBetweenTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "posts_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "posts_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "posts",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "posts",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_created_by",
                schema: "dbo",
                table: "tags",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tags_updated_by",
                schema: "dbo",
                table: "tags",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tags_created_by",
                schema: "dbo",
                table: "posts_tags",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tags_updated_by",
                schema: "dbo",
                table: "posts_tags",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_posts_created_by",
                schema: "dbo",
                table: "posts",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_posts_updated_by",
                schema: "dbo",
                table: "posts",
                column: "updated_by");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_created_by",
                schema: "dbo",
                table: "posts",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_updated_by",
                schema: "dbo",
                table: "posts",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_users_created_by",
                schema: "dbo",
                table: "posts_tags",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_users_updated_by",
                schema: "dbo",
                table: "posts_tags",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tags_users_created_by",
                schema: "dbo",
                table: "tags",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tags_users_updated_by",
                schema: "dbo",
                table: "tags",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_created_by",
                schema: "dbo",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_updated_by",
                schema: "dbo",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_users_created_by",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_users_updated_by",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_users_created_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_users_updated_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_created_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_updated_by",
                schema: "dbo",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_posts_tags_created_by",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropIndex(
                name: "IX_posts_tags_updated_by",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropIndex(
                name: "IX_posts_created_by",
                schema: "dbo",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_updated_by",
                schema: "dbo",
                table: "posts");

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "posts_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "posts_tags",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "posts",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "posts",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);
        }
    }
}
