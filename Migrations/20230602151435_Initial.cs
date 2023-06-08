using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RitzpaStockExchange.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TradeCycle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockName);
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentStockRate = table.Column<int>(type: "int", nullable: true),
                    CommandType = table.Column<int>(type: "int", nullable: false),
                    CommandWay = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BuyStockName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SellStockName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Commands_Stocks_BuyStockName",
                        column: x => x.BuyStockName,
                        principalTable: "Stocks",
                        principalColumn: "StockName");
                    table.ForeignKey(
                        name: "FK_Commands_Stocks_SellStockName",
                        column: x => x.SellStockName,
                        principalTable: "Stocks",
                        principalColumn: "StockName");
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    StockPrice = table.Column<int>(type: "int", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_Stocks_StockName",
                        column: x => x.StockName,
                        principalTable: "Stocks",
                        principalColumn: "StockName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_BuyStockName",
                table: "Commands",
                column: "BuyStockName");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_SellStockName",
                table: "Commands",
                column: "SellStockName");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_StockName",
                table: "Trades",
                column: "StockName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
