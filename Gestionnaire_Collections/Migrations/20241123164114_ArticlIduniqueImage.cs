using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestionnaire_Collections.Migrations
{
    /// <inheritdoc />
    public partial class ArticlIduniqueImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ArticleId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ArticleId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId");
        }
    }
}
