using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Berber.Migrations
{
    /// <inheritdoc />
    public partial class update_worker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkMissions_Workers_WorkerId",
                table: "WorkMissions");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "WorkingHourId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_AspNetUsers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkingHourId",
                table: "AspNetUsers",
                column: "WorkingHourId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_WorkerId",
                table: "WorkingHours",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WorkingHours_WorkingHourId",
                table: "AspNetUsers",
                column: "WorkingHourId",
                principalTable: "WorkingHours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkMissions_AspNetUsers_WorkerId",
                table: "WorkMissions",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WorkingHours_WorkingHourId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkMissions_AspNetUsers_WorkerId",
                table: "WorkMissions");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkingHourId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkingHourId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkMissions_Workers_WorkerId",
                table: "WorkMissions",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
