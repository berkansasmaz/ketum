using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Subscription",
                table => new
                {
                    SubscriptionId = table.Column<Guid>(),
                    SubscriptionTypeId = table.Column<Guid>(),
                    UserId = table.Column<Guid>(),
                    StartDate = table.Column<DateTime>(),
                    EndDate = table.Column<DateTime>(),
                    PaymentPeriod = table.Column<short>()
                },
                constraints: table => { table.PrimaryKey("PK_Subscription", x => x.SubscriptionId); });

            migrationBuilder.CreateTable(
                "SubscriptionFeature",
                table => new
                {
                    SubscriptionFeatureId = table.Column<Guid>(),
                    SubscriptionTypeFeatureId = table.Column<Guid>(),
                    SubscriptionTypeId = table.Column<Guid>(),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ValueUsed = table.Column<string>(nullable: true),
                    ValueRemained = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SubscriptionFeature", x => x.SubscriptionFeatureId); });

            migrationBuilder.CreateTable(
                "SubscriptionType",
                table => new
                {
                    SubscriptionTypeId = table.Column<Guid>(),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsPaid = table.Column<bool>(),
                    Price = table.Column<decimal>()
                },
                constraints: table => { table.PrimaryKey("PK_SubscriptionType", x => x.SubscriptionTypeId); });

            migrationBuilder.CreateTable(
                "SubscriptionTypeFeature",
                table => new
                {
                    SubscriptionTypeFeatureId = table.Column<Guid>(),
                    SubscriptionTypeId = table.Column<Guid>(),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    IsFeature = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypeFeature", x => x.SubscriptionTypeFeatureId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Subscription");

            migrationBuilder.DropTable(
                "SubscriptionFeature");

            migrationBuilder.DropTable(
                "SubscriptionType");

            migrationBuilder.DropTable(
                "SubscriptionTypeFeature");
        }
    }
}