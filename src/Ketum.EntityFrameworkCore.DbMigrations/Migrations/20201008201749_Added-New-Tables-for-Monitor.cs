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
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    MonitorStatus = table.Column<byte>(nullable: false),
                    TestStatus = table.Column<byte>(nullable: false),
                    UpTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadTime = table.Column<int>(nullable: false),
                    MonitorTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KtmMonitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KtmMonitorSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    MonitorId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(maxLength: 256, nullable: false),
                    Interval = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Status = table.Column<byte>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    MonitorStepId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    Log = table.Column<string>(maxLength: 512, nullable: true),
                    Interval = table.Column<int>(nullable: false)
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
                column: "MonitorStepId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitorStepLogs_TenantId",
                table: "KtmMonitorStepLogs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_KtmMonitorSteps_MonitorId",
                table: "KtmMonitorSteps",
                column: "MonitorId");

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
