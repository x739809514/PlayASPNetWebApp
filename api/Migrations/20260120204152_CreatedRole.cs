using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CreatedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "User");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ef93c51-8f9d-4259-8205-ac896c651b51", "b796965e-55c6-490a-9e9d-4023f3e4236f", "Admin", "ADMIN" },
                    { "e3b96b9b-4583-4335-8fca-f3e464c0b90d", "ec0f3539-e5ce-44f6-a142-cc3b1f0a5aa5", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ef93c51-8f9d-4259-8205-ac896c651b51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3b96b9b-4583-4335-8fca-f3e464c0b90d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Admin", "81a515f4-a265-4ccb-ac6c-c2d3764a2572", "Admin", "ADMIN" },
                    { "User", "2cefedc4-8c85-4e4d-a763-98545f020245", "User", "USER" }
                });
        }
    }
}
