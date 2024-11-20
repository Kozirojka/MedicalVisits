using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMedicineFromDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "5ffcee28-2fed-471f-92d5-33b3cf9ae920" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5ffcee28-2fed-471f-92d5-33b3cf9ae920");

            migrationBuilder.DropColumn(
                name: "RequiredMedicationsText",
                table: "VisitRequests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a3d55aac-49b5-41f2-98c6-249615b863fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cca419e6-9605-4ee3-8d1e-98cb15a96c33");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "99ea09fc-3df0-482c-892f-178a57f2b557");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "cf15539e-6930-4960-9097-423a56983f12");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e425c663-c590-4edc-8d4c-1b777f967002", 0, "6a09e4dc-4d86-4bb6-88f2-624230b06ad2", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEBAenIheZwZSOJtWizng5y+e0iywXuSJkpJYvIqTlt+k2Z6UTfNWnkVTgnjBqwxVNQ==", null, false, null, null, "6eb2ce72-db98-4a5c-af55-7abadfe6c89c", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e425c663-c590-4edc-8d4c-1b777f967002" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e425c663-c590-4edc-8d4c-1b777f967002" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e425c663-c590-4edc-8d4c-1b777f967002");

            migrationBuilder.AddColumn<string>(
                name: "RequiredMedicationsText",
                table: "VisitRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "012db6a4-f6da-4971-ba95-26e2d592a380");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c74cd5fe-8fac-46df-8f2a-627a21a0b980");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "85784062-494e-49e9-82db-f1ad524838d4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "93a82215-767f-4f08-9ea3-4b4cf45331cd");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5ffcee28-2fed-471f-92d5-33b3cf9ae920", 0, "10e4d69b-bf22-4be8-ba6d-637eab320abe", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEDhlS4/q94t1jMSXmEtOHGvXpjdSJgZQ7k1vuKSXjB15qGO8JaQ0pkbVDRCgo6bR+Q==", null, false, null, null, "fbf0b583-c96a-42e8-884b-a9d8960345e6", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "5ffcee28-2fed-471f-92d5-33b3cf9ae920" });
        }
    }
}
