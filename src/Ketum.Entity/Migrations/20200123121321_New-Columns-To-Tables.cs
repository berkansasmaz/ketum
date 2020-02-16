using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class NewColumnsToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Interval",
                table: "MonitorStepLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Interval",
                table: "MonitorStep",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "MonitorStep",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<int>(
                name: "MonitorTime",
                table: "Monitor",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "MonitorStepLog");

            migrationBuilder.DropColumn(
                name: "Interval",
                table: "MonitorStep");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MonitorStep");

            migrationBuilder.AlterColumn<short>(
                name: "MonitorTime",
                table: "Monitor",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
