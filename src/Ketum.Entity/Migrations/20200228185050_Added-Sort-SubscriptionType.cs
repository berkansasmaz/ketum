using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedSortSubscriptionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Sort",
                table: "SubscriptionTypeFeature",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "SubscriptionTypeFeature");
        }
    }
}
