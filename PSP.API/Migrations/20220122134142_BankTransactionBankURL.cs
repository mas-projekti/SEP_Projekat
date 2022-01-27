using Microsoft.EntityFrameworkCore.Migrations;

namespace PSP.API.Migrations
{
    public partial class BankTransactionBankURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransaction_Transactions_TransactionId",
                table: "BankTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankTransaction",
                table: "BankTransaction");

            migrationBuilder.RenameTable(
                name: "BankTransaction",
                newName: "BankTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_BankTransaction_TransactionId",
                table: "BankTransactions",
                newName: "IX_BankTransactions_TransactionId");

            migrationBuilder.AddColumn<string>(
                name: "BankURL",
                table: "BankTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankTransactions",
                table: "BankTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_Transactions_TransactionId",
                table: "BankTransactions",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_Transactions_TransactionId",
                table: "BankTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankTransactions",
                table: "BankTransactions");

            migrationBuilder.DropColumn(
                name: "BankURL",
                table: "BankTransactions");

            migrationBuilder.RenameTable(
                name: "BankTransactions",
                newName: "BankTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_BankTransactions_TransactionId",
                table: "BankTransaction",
                newName: "IX_BankTransaction_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankTransaction",
                table: "BankTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransaction_Transactions_TransactionId",
                table: "BankTransaction",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
