using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Commentone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46b7a9c9-9942-4f93-9c70-ed0e365c4cfa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c2ea564-b747-4d6b-aee1-dec4ad48edf3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2003456d-d353-42f3-8ef3-5b37e3c869f1", null, "User", "USER" },
                    { "24cac2b8-c32e-49fb-b033-177058a3f0c8", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2003456d-d353-42f3-8ef3-5b37e3c869f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24cac2b8-c32e-49fb-b033-177058a3f0c8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46b7a9c9-9942-4f93-9c70-ed0e365c4cfa", null, "Admin", "ADMIN" },
                    { "5c2ea564-b747-4d6b-aee1-dec4ad48edf3", null, "User", "USER" }
                });
        }
    }
}
