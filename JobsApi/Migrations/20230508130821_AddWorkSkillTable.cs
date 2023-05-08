using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsApi.Migrations
{
    public partial class AddWorkSkillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartAt",
                table: "Works",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 5, 8, 13, 8, 20, 989, DateTimeKind.Unspecified).AddTicks(3241), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 5, 7, 18, 2, 36, 42, DateTimeKind.Unspecified).AddTicks(3847), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "WorksSkills",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkId = table.Column<uint>(type: "int unsigned", nullable: false),
                    SkillId = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorksSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorksSkills_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorksSkills_SkillId",
                table: "WorksSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_WorksSkills_WorkId_SkillId",
                table: "WorksSkills",
                columns: new[] { "WorkId", "SkillId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorksSkills");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartAt",
                table: "Works",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 5, 7, 18, 2, 36, 42, DateTimeKind.Unspecified).AddTicks(3847), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 5, 8, 13, 8, 20, 989, DateTimeKind.Unspecified).AddTicks(3241), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
