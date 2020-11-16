using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Migrations
{
    public partial class AddedNewTablesforMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KtmMonitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    MonitorStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KtmMonitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KtmMonitorSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KtmMonitorSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KtmMonitorSteps_KtmMonitors_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "KtmMonitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KtmMonitorStepLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MonitorStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Log = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Interval = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KtmMonitorStepLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KtmMonitorStepLogs_KtmMonitorSteps_MonitorStepId",
                        column: x => x.MonitorStepId,
                        principalTable: "KtmMonitorSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitors_TenantId_Name",
                table: "KtmMonitors",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitorStepLogs_MonitorStepId",
                table: "KtmMonitorStepLogs",
                column: "MonitorStepId");

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitorStepLogs_TenantId",
                table: "KtmMonitorStepLogs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitorSteps_MonitorId",
                table: "KtmMonitorSteps",
                column: "MonitorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitorSteps_TenantId_Url",
                table: "KtmMonitorSteps",
                columns: new[] { "TenantId", "Url" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KtmMonitorStepLogs");

            migrationBuilder.DropTable(
                name: "KtmMonitorSteps");

            migrationBuilder.DropTable(
                name: "KtmMonitors");
        }
    }
}
