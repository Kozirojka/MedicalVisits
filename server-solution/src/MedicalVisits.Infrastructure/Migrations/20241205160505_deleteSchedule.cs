using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_DoctorProfileId_DayOfWeek",
                table: "WorkSchedules");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "fee2c627-d972-4789-85c1-026cec03bac0" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fee2c627-d972-4789-85c1-026cec03bac0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "afca9325-1a7e-4096-b5db-e0a44aa7f1fc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f29473d2-94aa-428e-b53e-d69325646558");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a0753bfd-1043-48a6-8de2-5ab20c3f496b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "31671f51-302e-4acf-a94e-87641f968d5f");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "652234d8-4bd8-4e40-a3bb-162421c16339", 0, "175926b0-6ac3-4795-8725-79404a0c6a79", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEHCPhrnGm6d4fW7vAlRIrfKXiKcdPvnSGQQXMMG6BJDHfR6cUXpHo2n5T40KhA7lEQ==", null, false, null, null, "6e048393-37e2-4f69-80c0-7f233bb0a10f", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "652234d8-4bd8-4e40-a3bb-162421c16339" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_DoctorProfileId",
                table: "WorkSchedules",
                column: "DoctorProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_DoctorProfileId",
                table: "WorkSchedules");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "652234d8-4bd8-4e40-a3bb-162421c16339" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "652234d8-4bd8-4e40-a3bb-162421c16339");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "86bb4dcc-2ec5-4664-9e47-e80374715a4f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "54305e7a-9e87-4d48-a833-e1dce9ac34ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b1eee93e-abd7-4101-b1e1-1ea67a01507a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "c52fb2ea-d1dd-4f73-bdf9-d5953ee8fea4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fee2c627-d972-4789-85c1-026cec03bac0", 0, "d0ec78b9-dc7b-47f9-9b3e-c0bc00f31a5c", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEO4fJFuVNqDi+xV9nBIko9UMgQJjMgbnzORtaO6vReexzKiyc05v+xwjz+Fnif6SKg==", null, false, null, null, "078951a4-c982-495d-8f42-999e75261a9b", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "fee2c627-d972-4789-85c1-026cec03bac0" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_DoctorProfileId_DayOfWeek",
                table: "WorkSchedules",
                columns: new[] { "DoctorProfileId", "DayOfWeek" },
                unique: true);
        }
    }
}
