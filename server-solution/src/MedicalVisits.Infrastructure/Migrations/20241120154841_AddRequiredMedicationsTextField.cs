using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredMedicationsTextField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ca35cfa4-2362-4705-824b-ac8004a393a5" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ca35cfa4-2362-4705-824b-ac8004a393a5");

            migrationBuilder.RenameColumn(
                name: "RequiredMedications",
                table: "VisitRequests",
                newName: "RequiredMedicationsText");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "5ffcee28-2fed-471f-92d5-33b3cf9ae920" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5ffcee28-2fed-471f-92d5-33b3cf9ae920");

            migrationBuilder.RenameColumn(
                name: "RequiredMedicationsText",
                table: "VisitRequests",
                newName: "RequiredMedications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "16c7f6dc-ee47-410d-8f67-61868afa0332");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "65d79e9c-6107-4fd8-8458-1f2ac229a976");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "695ac0df-e5d6-4f29-8d9f-e6f5d926ddb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "c24886b1-870d-4a66-9a73-e602d2da93f2");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ca35cfa4-2362-4705-824b-ac8004a393a5", 0, "55f65b95-0eb1-4204-82b2-39232e9da8e8", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEPc3ZOUnVO2RaVB0gXTbuDmVcuQE4pSS3o0pMMcyJ3K5G6RgnLwIK1hX/WE305H5ug==", null, false, null, null, "6f840f00-f81e-4bc7-b819-bd4a35705bc4", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ca35cfa4-2362-4705-824b-ac8004a393a5" });
        }
    }
}
