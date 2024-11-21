using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotNullbeCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "05cb2638-dfe9-4c29-aad2-77acbf8f07f1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05cb2638-dfe9-4c29-aad2-77acbf8f07f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9aebb599-b171-47d8-bcf2-9c291b3844d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6f3291f8-d3ee-4f50-ae69-5d233cb35afe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "6a353281-d15a-4cda-b3f9-a37619b77024");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "82dd5c24-6dd8-4a4b-9e1b-76f2b9acf9ef");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2a3044ae-2233-4da9-9984-faff17403f0d", 0, "746f76d0-a170-48d7-a5e6-3eb4f1c9d36e", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEF8SqCB/fYPnyiVZ+/MwJl+LPpwaz/Kof5Af+pf4HxfVh/JRTtHUKWekxQlG3r0WBw==", null, false, null, null, "05cf7f27-6575-4851-b582-20d944ff93ea", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "2a3044ae-2233-4da9-9984-faff17403f0d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "2a3044ae-2233-4da9-9984-faff17403f0d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a3044ae-2233-4da9-9984-faff17403f0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "7fa743b5-4f37-4d8c-9111-9891cb14a843");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f0ba484e-b686-432d-bfb3-d4060e2b32bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "9b807dc7-ae84-4951-97a2-2cee9403608e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "b9e8ee15-f64f-4a35-a496-db03b3dfe6f7");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "05cb2638-dfe9-4c29-aad2-77acbf8f07f1", 0, "2917d737-233c-4b97-9d63-acb81b44ac34", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEOoKX+ShItSSJIx65qV3nF13UtFCN0DXx+6hAdQdNm4qY/Lq6DuDkfUjPpk29DeziA==", null, false, null, null, "e1744517-c61d-4e5f-87c2-6f2d48ef231e", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "05cb2638-dfe9-4c29-aad2-77acbf8f07f1" });
        }
    }
}
