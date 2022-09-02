using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAlgoTrader.Back.Repository.Implementation.Migrations
{
    public partial class transactionisadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosePrice",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "LastPrice",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "NumberTrade",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "PriceFirst",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "PriceMax",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "PriceMin",
                table: "Symbols");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberTrade = table.Column<int>(type: "int", nullable: false),
                    ClosePrice = table.Column<double>(type: "float", nullable: false),
                    LastPrice = table.Column<double>(type: "float", nullable: false),
                    PriceMin = table.Column<double>(type: "float", nullable: false),
                    PriceMax = table.Column<double>(type: "float", nullable: false),
                    PriceFirst = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Symbols_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SymbolId",
                table: "Transactions",
                column: "SymbolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.AddColumn<double>(
                name: "ClosePrice",
                table: "Symbols",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Symbols",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "LastPrice",
                table: "Symbols",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberTrade",
                table: "Symbols",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PriceFirst",
                table: "Symbols",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceMax",
                table: "Symbols",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceMin",
                table: "Symbols",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
