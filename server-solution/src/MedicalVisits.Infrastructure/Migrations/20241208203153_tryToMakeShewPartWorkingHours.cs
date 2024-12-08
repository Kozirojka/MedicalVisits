using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tryToMakeShewPartWorkingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkSchedules_AspNetUsers_DoctorId",
                table: "DoctorWorkSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitRequests_TimeSlots_TimeSlotId",
                table: "VisitRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_ScheduleId_Date_Status",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_UserId",
                table: "DoctorProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "04ea4b9a-f870-4afb-b885-d470ade3cb8a" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04ea4b9a-f870-4afb-b885-d470ade3cb8a");

            migrationBuilder.AddColumn<int>(
                name: "VisitRequestId",
                table: "TimeSlots",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DoctorProfiles_UserId",
                table: "DoctorProfiles",
                column: "UserId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b9539b02-a013-4734-8895-18e3e1d7d3da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ab1b2f06-2443-452c-aa6e-fe19a977e311");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "854dcf6c-0d43-406a-8643-2b68fe3ddd87");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "cf97cc06-281d-430d-a7fb-449c83c040b5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0a9c6f6e-b5cb-446d-9a0c-eb75e8350cc0", 0, "1e0f25aa-1287-46b0-b520-f85022aa6b0f", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEHA+YKHQQx9bb+IvPL7oY+kLe44/xfjNGakO3+5bK33VtXYoQSBdFHAY36ACdTiqEQ==", null, false, null, null, "4db6dde8-0843-47fc-8970-1016bf4edcda", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "0a9c6f6e-b5cb-446d-9a0c-eb75e8350cc0" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ScheduleId",
                table: "TimeSlots",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkSchedules_DoctorProfiles_DoctorId",
                table: "DoctorWorkSchedules",
                column: "DoctorId",
                principalTable: "DoctorProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitRequests_TimeSlots_TimeSlotId",
                table: "VisitRequests",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkSchedules_DoctorProfiles_DoctorId",
                table: "DoctorWorkSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitRequests_TimeSlots_TimeSlotId",
                table: "VisitRequests");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_ScheduleId",
                table: "TimeSlots");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DoctorProfiles_UserId",
                table: "DoctorProfiles");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "0a9c6f6e-b5cb-446d-9a0c-eb75e8350cc0" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a9c6f6e-b5cb-446d-9a0c-eb75e8350cc0");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "TimeSlots");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b8a3d512-697b-499d-a4e8-82ac017dff45");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9b0130c3-64ab-4d76-8844-018dd09974d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "2d29af71-e5a0-4e8e-bf18-1eacc94a5c55");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "c54a3fa2-71c3-41a2-b561-49026ffbb32a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "04ea4b9a-f870-4afb-b885-d470ade3cb8a", 0, "413fe52a-c8b7-4490-bc77-29c35b147d23", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEO9nniK04lOG/qguFBqz936MdktUzq1Ip2hHQ0p8tBcTgWdNb4YJNXC7M8A9GMH9LQ==", null, false, null, null, "93c830a1-2892-47e9-8a1b-6cd34e3fc37b", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "04ea4b9a-f870-4afb-b885-d470ade3cb8a" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ScheduleId_Date_Status",
                table: "TimeSlots",
                columns: new[] { "ScheduleId", "Date", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_UserId",
                table: "DoctorProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkSchedules_AspNetUsers_DoctorId",
                table: "DoctorWorkSchedules",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitRequests_TimeSlots_TimeSlotId",
                table: "VisitRequests",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
