using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class NewColumnsToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "Interval",
                "MonitorStepLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Interval",
                "MonitorStep",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                "Status",
                "MonitorStep",
                nullable: false,
                defaultValue: (short) 0);

            migrationBuilder.AlterColumn<int>(
                "MonitorTime",
                "Monitor",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Interval",
                "MonitorStepLog");

            migrationBuilder.DropColumn(
                "Interval",
                "MonitorStep");

            migrationBuilder.DropColumn(
                "Status",
                "MonitorStep");

            migrationBuilder.AlterColumn<short>(
                "MonitorTime",
                "Monitor",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}