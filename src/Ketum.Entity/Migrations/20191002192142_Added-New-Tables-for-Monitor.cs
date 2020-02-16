using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedNewTablesforMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitor",
                columns: table => new
                {
                    MonitorId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MonitorStatus = table.Column<short>(nullable: false),
                    TestStatus = table.Column<short>(nullable: false),
                    LastCheckDate = table.Column<DateTime>(nullable: false),
                    UpTime = table.Column<decimal>(nullable: false),
                    LoadTime = table.Column<int>(nullable: false),
                    MonitorTime = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor", x => x.MonitorId);
                });

            migrationBuilder.CreateTable(
                name: "MonitorStep",
                columns: table => new
                {
                    MonitorStepId = table.Column<Guid>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    Type = table.Column<short>(nullable: false),
                    Settings = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorStep", x => x.MonitorStepId);
                });

            migrationBuilder.CreateTable(
                name: "MonitorStepLog",
                columns: table => new
                {
                    MonitorStepLogId = table.Column<Guid>(nullable: false),
                    MonitorStepId = table.Column<Guid>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    Log = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorStepLog", x => x.MonitorStepLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitor");

            migrationBuilder.DropTable(
                name: "MonitorStep");

            migrationBuilder.DropTable(
                name: "MonitorStepLog");
        }
    }
}
