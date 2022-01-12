using Microsoft.EntityFrameworkCore.Migrations;

namespace PSP.API.Migrations
{
    public partial class FixedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PspClientId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PspClientId",
                table: "Transactions",
                column: "PspClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PspClients_PspClientId",
                table: "Transactions",
                column: "PspClientId",
                principalTable: "PspClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PspClients_PspClientId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PspClientId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PspClientId",
                table: "Transactions");
        }
    }
}
