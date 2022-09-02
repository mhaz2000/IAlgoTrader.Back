using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAlgoTrader.Back.Repository.Implementation.Migrations
{
    public partial class tradeisadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApplied",
                table: "Orders",
                newName: "IsActive");

            migrationBuilder.AlterColumn<double>(
                name: "VolumePercentage",
                table: "Orders",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellVolume = table.Column<double>(type: "float", nullable: false),
                    BuyVolume = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Orders",
                newName: "IsApplied");

            migrationBuilder.AlterColumn<int>(
                name: "VolumePercentage",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
