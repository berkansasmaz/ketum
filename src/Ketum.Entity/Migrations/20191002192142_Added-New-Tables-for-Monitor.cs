using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedNewTablesforMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Monitor",
                table => new
                {
                    MonitorId = table.Column<Guid>(),
                    CreatedDate = table.Column<DateTime>(),
                    UpdatedDate = table.Column<DateTime>(),
                    Name = table.Column<string>(nullable: true),
                    MonitorStatus = table.Column<short>(),
                    TestStatus = table.Column<short>(),
                    LastCheckDate = table.Column<DateTime>(),
                    UpTime = table.Column<decimal>(),
                    LoadTime = table.Column<int>(),
                    MonitorTime = table.Column<short>()
                },
                constraints: table => { table.PrimaryKey("PK_Monitor", x => x.MonitorId); });

            migrationBuilder.CreateTable(
                "MonitorStep",
                table => new
                {
                    MonitorStepId = table.Column<Guid>(),
                    MonitorId = table.Column<Guid>(),
                    Type = table.Column<short>(),
                    Settings = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_MonitorStep", x => x.MonitorStepId); });

            migrationBuilder.CreateTable(
                "MonitorStepLog",
                table => new
                {
                    MonitorStepLogId = table.Column<Guid>(),
                    MonitorStepId = table.Column<Guid>(),
                    MonitorId = table.Column<Guid>(),
                    StartDate = table.Column<DateTime>(),
                    EndDate = table.Column<DateTime>(),
                    Status = table.Column<short>(),
                    Log = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_MonitorStepLog", x => x.MonitorStepLogId); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Monitor");

            migrationBuilder.DropTable(
                "MonitorStep");

            migrationBuilder.DropTable(
                "MonitorStepLog");
        }
    }
}