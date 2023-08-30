using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4eb7d120-c718-4023-a1dd-7a76251cd514", null, "Mentee", "MENTEE" },
                    { "d3e37637-5c79-485e-86a7-01e86ae0061a", null, "Mentor", "MENTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4eb7d120-c718-4023-a1dd-7a76251cd514");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3e37637-5c79-485e-86a7-01e86ae0061a");
        }
    }
}
