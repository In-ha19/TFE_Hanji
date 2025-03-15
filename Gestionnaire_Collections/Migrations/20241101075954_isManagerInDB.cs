using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestionnaire_Collections.Migrations
{
    /// <inheritdoc />
    public partial class isManagerInDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_manager",
                table: "Family_Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_manager",
                table: "Family_Users");
        }
    }
}
