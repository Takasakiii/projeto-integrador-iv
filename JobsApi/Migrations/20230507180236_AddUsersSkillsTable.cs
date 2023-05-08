using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsApi.Migrations
{
    public partial class AddUsersSkillsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartAt",
                table: "Works",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 5, 7, 18, 2, 36, 42, DateTimeKind.Unspecified).AddTicks(3847), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 5, 6, 3, 44, 51, 277, DateTimeKind.Unspecified).AddTicks(8849), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "UsersSkills",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<uint>(type: "int unsigned", nullable: false),
                    SkillId = table.Column<uint>(type: "int unsigned", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Years = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersSkills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSkills_SkillId",
                table: "UsersSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSkills_UserId_SkillId",
                table: "UsersSkills",
                columns: new[] { "UserId", "SkillId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersSkills");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartAt",
                table: "Works",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 5, 6, 3, 44, 51, 277, DateTimeKind.Unspecified).AddTicks(8849), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 5, 7, 18, 2, 36, 42, DateTimeKind.Unspecified).AddTicks(3847), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
