using Microsoft.EntityFrameworkCore.Migrations;

namespace InvestCarControl.Migrations
{
    public partial class parceiros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "parceiro",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "parceiro",
                type: "varchar(45)",
                nullable: true);
        }
    }
}
