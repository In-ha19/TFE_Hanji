using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestionnaire_Collections.Migrations
{
    /// <inheritdoc />
    public partial class changementMemberShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShip_Families_AspNetUsers_ManagerId",
                table: "MemberShip_Families");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "MemberShip_Families",
                newName: "FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberShip_Families_ManagerId",
                table: "MemberShip_Families",
                newName: "IX_MemberShip_Families_FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShip_Families_Families_FamilyId",
                table: "MemberShip_Families",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberShip_Families_Families_FamilyId",
                table: "MemberShip_Families");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "MemberShip_Families",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberShip_Families_FamilyId",
                table: "MemberShip_Families",
                newName: "IX_MemberShip_Families_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShip_Families_AspNetUsers_ManagerId",
                table: "MemberShip_Families",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
