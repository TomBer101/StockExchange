using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RitzpaStockExchange.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommandInitiator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Buyer",
                table: "Trades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Seller",
                table: "Trades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitiatorId",
                table: "Commands",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_InitiatorId",
                table: "Commands",
                column: "InitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Users_InitiatorId",
                table: "Commands",
                column: "InitiatorId",
                principalTable: "Users",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Users_InitiatorId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_InitiatorId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "Buyer",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Seller",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "InitiatorId",
                table: "Commands");
        }
    }
}
