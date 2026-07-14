using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3877cfbf-fa1e-40c6-86b1-08e2039a82db", null, "Administrator", "ADMINISTRATOR" },
                    { "9777cb41-7ddf-4ca2-968d-a3047a90efc2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1e7b9958-af07-4b1d-a04f-d4f52d5422c9", 0, "9fd1a044-d78e-4b95-a481-a140fb49039e", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEDnYUex7o84yUO78vJDLMbQLhnIDYmWT9msNmxyjt5Jh5NQHczGQ+rg2rp6Vf3yz5g==", null, false, "4ea3ae3a-3b90-4f2f-a11e-33527d320a56", false, "user@bookstore.com" },
                    { "26222e5b-7980-4e6b-970b-974b73c3a26a", 0, "51427087-9373-4894-957f-66446c9da6d6", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEP0wpDPifmklk2MbpJiX9XkLxYR2xhC8Xj60wr5Hjx37Mw/984Z48cs844byZc7hkQ==", null, false, "237cc198-04a4-4c5a-8989-217fecd5134d", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9777cb41-7ddf-4ca2-968d-a3047a90efc2", "1e7b9958-af07-4b1d-a04f-d4f52d5422c9" },
                    { "3877cfbf-fa1e-40c6-86b1-08e2039a82db", "26222e5b-7980-4e6b-970b-974b73c3a26a" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9777cb41-7ddf-4ca2-968d-a3047a90efc2", "1e7b9958-af07-4b1d-a04f-d4f52d5422c9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3877cfbf-fa1e-40c6-86b1-08e2039a82db", "26222e5b-7980-4e6b-970b-974b73c3a26a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3877cfbf-fa1e-40c6-86b1-08e2039a82db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9777cb41-7ddf-4ca2-968d-a3047a90efc2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e7b9958-af07-4b1d-a04f-d4f52d5422c9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26222e5b-7980-4e6b-970b-974b73c3a26a");
        }
    }
}
