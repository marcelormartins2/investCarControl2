using Microsoft.EntityFrameworkCore.Migrations;

namespace InvestCarControl.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParceiroId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ParceiroId",
                table: "AspNetUserRoles",
                column: "ParceiroId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ParceiroId",
                table: "AspNetUserRoles",
                column: "ParceiroId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_ParceiroId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_ParceiroId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ParceiroId",
                table: "AspNetUserRoles");
        }
    }
}
