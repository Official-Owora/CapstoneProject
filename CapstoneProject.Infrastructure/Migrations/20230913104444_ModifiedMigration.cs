using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b870354f-ce63-42ea-853b-0175f05a0872");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e94f5c54-a3cf-43c9-88ae-27f24356971a");

            migrationBuilder.AlterColumn<int>(
                name: "YearsOfExperience",
                table: "Mentor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Mentee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47f0e315-431a-47de-9cc7-9bb174d82ac3", null, "Mentor", "MENTOR" },
                    { "eb11a599-b371-40a9-bab0-01161e215252", null, "Mentee", "MENTEE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47f0e315-431a-47de-9cc7-9bb174d82ac3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb11a599-b371-40a9-bab0-01161e215252");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Mentee");

            migrationBuilder.AlterColumn<string>(
                name: "YearsOfExperience",
                table: "Mentor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b870354f-ce63-42ea-853b-0175f05a0872", null, "Mentee", "MENTEE" },
                    { "e94f5c54-a3cf-43c9-88ae-27f24356971a", null, "Mentor", "MENTOR" }
                });
        }
    }
}
