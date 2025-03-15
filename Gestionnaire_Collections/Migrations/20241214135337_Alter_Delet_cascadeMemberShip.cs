using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestionnaire_Collections.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Delet_cascadeMemberShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShip_Families_Families_FamilyId",
                table: "MemberShip_Families");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShip_Families_Families_FamilyId",
                table: "MemberShip_Families",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShip_Families_Families_FamilyId",
                table: "MemberShip_Families");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShip_Families_Families_FamilyId",
                table: "MemberShip_Families",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
