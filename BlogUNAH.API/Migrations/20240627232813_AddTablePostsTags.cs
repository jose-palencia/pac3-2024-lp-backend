using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogUNAH.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePostsTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts_tags",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    post_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tag_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_posts_tags_posts_post_id",
                        column: x => x.post_id,
                        principalSchema: "dbo",
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_posts_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "dbo",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_tags_post_id",
                schema: "dbo",
                table: "posts_tags",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_tags_tag_id",
                schema: "dbo",
                table: "posts_tags",
                column: "tag_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts_tags",
                schema: "dbo");
        }
    }
}
