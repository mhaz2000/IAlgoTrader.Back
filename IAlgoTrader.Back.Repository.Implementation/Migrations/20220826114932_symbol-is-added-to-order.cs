using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAlgoTrader.Back.Repository.Implementation.Migrations
{
    public partial class symbolisaddedtoorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SymbolId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SymbolId",
                table: "Orders",
                column: "SymbolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Symbols_SymbolId",
                table: "Orders",
                column: "SymbolId",
                principalTable: "Symbols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Symbols_SymbolId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SymbolId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SymbolId",
                table: "Orders");
        }
    }
}
