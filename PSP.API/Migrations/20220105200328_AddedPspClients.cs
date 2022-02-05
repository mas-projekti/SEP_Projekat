using Microsoft.EntityFrameworkCore.Migrations;

namespace PSP.API.Migrations
{
    public partial class AddedPspClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PspClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionOutcomeCallback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SettingsUpdatedCallback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaypalActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    BitcoinActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BankActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PspClients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PspClients");
        }
    }
}
