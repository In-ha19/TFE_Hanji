using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestionnaire_Collections.Migrations
{
    /// <inheritdoc />
    public partial class UNIQUEfAMILYnAME : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Families",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Families_Name",
                table: "Families",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Families_Name",
                table: "Families");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
