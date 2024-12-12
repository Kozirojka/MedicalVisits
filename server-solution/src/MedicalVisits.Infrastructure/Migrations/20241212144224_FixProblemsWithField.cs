using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProblemsWithField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "4816d032-4f8e-43ca-841e-f402b3917446" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4816d032-4f8e-43ca-841e-f402b3917446");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ee81f2e5-ca09-4653-81f9-1b88134634fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "96c3cf11-235f-408f-b93d-6138df94cb17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "ed5b1acf-d8bf-46d6-b42e-5e51cbaf9ab4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "905b99f5-8079-48c6-bef9-2af1ded0cba4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b", 0, "81c1aaff-cc44-4fc8-a3b9-428261f4f202", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEOWOla5e4Q8t2SKTHliFekkGK/c1QjoS20db+ut5B8g1K4cKqNmDZNsrHUFF526Fkg==", null, false, null, null, "f49b4cd7-598f-46dd-b1cf-d1cc61526529", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "54d9a6cb-26ac-404b-9a13-f4f7b609c73b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "aaf3aa29-5f58-4b15-a9b7-cb6e7179a8a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "dee8169f-30b3-4319-9ffe-ec69cac29fc4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "bed0a49c-8cf0-49bd-a787-04a866451689");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4816d032-4f8e-43ca-841e-f402b3917446", 0, "33abefb5-9f77-468c-90b6-9896aa20d48a", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEO/zPjChEY9kU7Cp3Ccc3mYXNSJtJpjfX6L3QqhRv22x+tp13QcfIwkfwe1sPgzsEw==", null, false, null, null, "871f30ae-4693-43f3-8352-a087163b9f36", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4816d032-4f8e-43ca-841e-f402b3917446" });
        }
    }
}
