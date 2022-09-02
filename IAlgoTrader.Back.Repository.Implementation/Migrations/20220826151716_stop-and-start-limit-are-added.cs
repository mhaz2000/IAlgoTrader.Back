using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAlgoTrader.Back.Repository.Implementation.Migrations
{
    public partial class stopandstartlimitareadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "OrderVolume",
                table: "Orders",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "AlgorithmType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "OrderStartLimit",
                table: "Orders",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OrderStopLimit",
                table: "Orders",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlgorithmType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStartLimit",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStopLimit",
                table: "Orders");

            migrationBuilder.AlterColumn<double>(
                name: "OrderVolume",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
