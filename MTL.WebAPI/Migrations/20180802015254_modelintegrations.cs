using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MTL.WebAPI.Migrations
{
    public partial class modelintegrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Memories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "timeLineId",
                table: "Memories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Memories_timeLineId",
                table: "Memories",
                column: "timeLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memories_TimeLines_timeLineId",
                table: "Memories",
                column: "timeLineId",
                principalTable: "TimeLines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memories_TimeLines_timeLineId",
                table: "Memories");

            migrationBuilder.DropIndex(
                name: "IX_Memories_timeLineId",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Memories");

            migrationBuilder.DropColumn(
                name: "timeLineId",
                table: "Memories");
        }
    }
}
