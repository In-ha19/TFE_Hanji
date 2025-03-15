using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestionnaire_Collections.Migrations
{
    /// <inheritdoc />
    public partial class EnleverimagedeArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ArticleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Articles");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_ArticleId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArticleId",
                table: "Images",
                column: "ArticleId",
                unique: true);
        }
    }
}
