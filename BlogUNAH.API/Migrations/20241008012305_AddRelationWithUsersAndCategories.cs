using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogUNAH.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationWithUsersAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_categories_category_id",
                schema: "dbo",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_posts_post_id",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_tags_tag_id",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_claims_roles_RoleId",
                schema: "security",
                table: "roles_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_users_claims_users_UserId",
                schema: "security",
                table: "users_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_users_logins_users_UserId",
                schema: "security",
                table: "users_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_roles_RoleId",
                schema: "security",
                table: "users_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_users_UserId",
                schema: "security",
                table: "users_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_users_tokens_users_UserId",
                schema: "security",
                table: "users_tokens");

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "categories",
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
                table: "categories",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_created_by",
                schema: "dbo",
                table: "categories",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_categories_updated_by",
                schema: "dbo",
                table: "categories",
                column: "updated_by");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_users_created_by",
                schema: "dbo",
                table: "categories",
                column: "created_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_categories_users_updated_by",
                schema: "dbo",
                table: "categories",
                column: "updated_by",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_categories_category_id",
                schema: "dbo",
                table: "posts",
                column: "category_id",
                principalSchema: "dbo",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_posts_post_id",
                schema: "dbo",
                table: "posts_tags",
                column: "post_id",
                principalSchema: "dbo",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_tags_tag_id",
                schema: "dbo",
                table: "posts_tags",
                column: "tag_id",
                principalSchema: "dbo",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_roles_claims_roles_RoleId",
                schema: "security",
                table: "roles_claims",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_claims_users_UserId",
                schema: "security",
                table: "users_claims",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_logins_users_UserId",
                schema: "security",
                table: "users_logins",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_roles_RoleId",
                schema: "security",
                table: "users_roles",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_users_UserId",
                schema: "security",
                table: "users_roles",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_tokens_users_UserId",
                schema: "security",
                table: "users_tokens",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_users_created_by",
                schema: "dbo",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_users_updated_by",
                schema: "dbo",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_categories_category_id",
                schema: "dbo",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_posts_post_id",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_tags_tags_tag_id",
                schema: "dbo",
                table: "posts_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_claims_roles_RoleId",
                schema: "security",
                table: "roles_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_users_claims_users_UserId",
                schema: "security",
                table: "users_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_users_logins_users_UserId",
                schema: "security",
                table: "users_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_roles_RoleId",
                schema: "security",
                table: "users_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_users_UserId",
                schema: "security",
                table: "users_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_users_tokens_users_UserId",
                schema: "security",
                table: "users_tokens");

            migrationBuilder.DropIndex(
                name: "IX_categories_created_by",
                schema: "dbo",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_updated_by",
                schema: "dbo",
                table: "categories");

            migrationBuilder.AlterColumn<string>(
                name: "updated_by",
                schema: "dbo",
                table: "categories",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                schema: "dbo",
                table: "categories",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_categories_category_id",
                schema: "dbo",
                table: "posts",
                column: "category_id",
                principalSchema: "dbo",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_posts_post_id",
                schema: "dbo",
                table: "posts_tags",
                column: "post_id",
                principalSchema: "dbo",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_tags_tags_tag_id",
                schema: "dbo",
                table: "posts_tags",
                column: "tag_id",
                principalSchema: "dbo",
                principalTable: "tags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roles_claims_roles_RoleId",
                schema: "security",
                table: "roles_claims",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_claims_users_UserId",
                schema: "security",
                table: "users_claims",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_logins_users_UserId",
                schema: "security",
                table: "users_logins",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_roles_RoleId",
                schema: "security",
                table: "users_roles",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_users_UserId",
                schema: "security",
                table: "users_roles",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_tokens_users_UserId",
                schema: "security",
                table: "users_tokens",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
