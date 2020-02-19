using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ketum.Entity.Migrations
{
    public partial class AddedNewFieldstoMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                "UserId",
                "Monitor",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "UserId",
                "Monitor");
        }
    }
}