using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogPostLike2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogPostLikes_BlogPosts_BlogPostId",
                table: "blogPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blogPostLikes",
                table: "blogPostLikes");

            migrationBuilder.RenameTable(
                name: "blogPostLikes",
                newName: "BlogPostLikes");

            migrationBuilder.RenameIndex(
                name: "IX_blogPostLikes_BlogPostId",
                table: "BlogPostLikes",
                newName: "IX_BlogPostLikes_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostLikes",
                table: "BlogPostLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostLikes",
                table: "BlogPostLikes");

            migrationBuilder.RenameTable(
                name: "BlogPostLikes",
                newName: "blogPostLikes");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostLikes_BlogPostId",
                table: "blogPostLikes",
                newName: "IX_blogPostLikes_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blogPostLikes",
                table: "blogPostLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_blogPostLikes_BlogPosts_BlogPostId",
                table: "blogPostLikes",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
