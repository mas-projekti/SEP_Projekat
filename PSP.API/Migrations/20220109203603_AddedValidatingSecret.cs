using Microsoft.EntityFrameworkCore.Migrations;

namespace PSP.API.Migrations
{
    public partial class AddedValidatingSecret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValidatingSecret",
                table: "PspClients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidatingSecret",
                table: "PspClients");
        }
    }
}
