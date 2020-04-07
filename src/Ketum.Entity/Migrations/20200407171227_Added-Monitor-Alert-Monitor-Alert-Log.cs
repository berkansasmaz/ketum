using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedMonitorAlertMonitorAlertLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitorAlertLog",
                columns: table => new
                {
                    MonitorAlertLogId = table.Column<Guid>(nullable: false),
                    MonitorAlertId = table.Column<Guid>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    Log = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorAlertLog", x => x.MonitorAlertLogId);
                });

            migrationBuilder.CreateTable(
                name: "MonitorAlerts",
                columns: table => new
                {
                    MonitorAlertId = table.Column<Guid>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ChannelType = table.Column<short>(nullable: false),
                    Settings = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorAlerts", x => x.MonitorAlertId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionFeature_SubscriptionTypeFeatureId",
                table: "SubscriptionFeature",
                column: "SubscriptionTypeFeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionFeature_SubscriptionTypeFeature_SubscriptionTyp~",
                table: "SubscriptionFeature",
                column: "SubscriptionTypeFeatureId",
                principalTable: "SubscriptionTypeFeature",
                principalColumn: "SubscriptionTypeFeatureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionFeature_SubscriptionTypeFeature_SubscriptionTyp~",
                table: "SubscriptionFeature");

            migrationBuilder.DropTable(
                name: "MonitorAlertLog");

            migrationBuilder.DropTable(
                name: "MonitorAlerts");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionFeature_SubscriptionTypeFeatureId",
                table: "SubscriptionFeature");
        }
    }
}
