using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionId = table.Column<Guid>(nullable: false),
                    SubscriptionTypeId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PaymentPeriod = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionFeature",
                columns: table => new
                {
                    SubscriptionFeatureId = table.Column<Guid>(nullable: false),
                    SubscriptionTypeFeatureId = table.Column<Guid>(nullable: false),
                    SubscriptionTypeId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ValueUsed = table.Column<string>(nullable: true),
                    ValueRemained = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionFeature", x => x.SubscriptionFeatureId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionType",
                columns: table => new
                {
                    SubscriptionTypeId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionType", x => x.SubscriptionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTypeFeature",
                columns: table => new
                {
                    SubscriptionTypeFeatureId = table.Column<Guid>(nullable: false),
                    SubscriptionTypeId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    IsFeature = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypeFeature", x => x.SubscriptionTypeFeatureId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "SubscriptionFeature");

            migrationBuilder.DropTable(
                name: "SubscriptionType");

            migrationBuilder.DropTable(
                name: "SubscriptionTypeFeature");
        }
    }
}
