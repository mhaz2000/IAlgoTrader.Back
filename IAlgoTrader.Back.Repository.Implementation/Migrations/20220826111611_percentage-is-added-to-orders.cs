using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAlgoTrader.Back.Repository.Implementation.Migrations
{
    public partial class percentageisaddedtoorders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderVolumne",
                table: "Orders",
                newName: "OrderVolume");

            migrationBuilder.AlterColumn<int>(
                name: "OrderLength",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "OrderAmount",
                table: "Orders",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "VolumePercentage",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumePercentage",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderVolume",
                table: "Orders",
                newName: "OrderVolumne");

            migrationBuilder.AlterColumn<int>(
                name: "OrderLength",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "OrderAmount",
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
