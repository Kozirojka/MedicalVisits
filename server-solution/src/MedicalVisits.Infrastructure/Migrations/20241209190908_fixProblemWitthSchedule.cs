using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixProblemWitthSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWorkPlans_DoctorProfiles_DoctorId",
                table: "ScheduleWorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_VisitRequests_RequestId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleWorkPlans_DoctorId",
                table: "ScheduleWorkPlans");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "6cc4db82-8435-46d4-a942-e302d58267a6" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cc4db82-8435-46d4-a942-e302d58267a6");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "ScheduleWorkPlans");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "TimeSlots",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ScheduleWorkPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f281dfd5-b984-4b08-bf3e-43407d104fe4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f05ecd92-bbf3-457b-90a6-f186bbbf6fde");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "637e1010-83fa-4efd-b833-6f09885a48f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9e68772f-8223-4817-bbf9-5a70f82194a7");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3cf769eb-9c8d-4704-a0f3-8faf6a616ec2", 0, "3fc66f19-ed3f-4c6d-a844-2145f9ebc594", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAELJcpweAZzTxUjMAJF+a/gJ5VVxGsyxPM+wdE8ndk3YDDae2eii8/apOwNUOBwxjrg==", null, false, null, null, "6183b659-e607-4f87-a50a-06f39c07ec8c", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "3cf769eb-9c8d-4704-a0f3-8faf6a616ec2" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWorkPlans_UserId",
                table: "ScheduleWorkPlans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWorkPlans_AspNetUsers_UserId",
                table: "ScheduleWorkPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_VisitRequests_RequestId",
                table: "TimeSlots",
                column: "RequestId",
                principalTable: "VisitRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWorkPlans_AspNetUsers_UserId",
                table: "ScheduleWorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_VisitRequests_RequestId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleWorkPlans_UserId",
                table: "ScheduleWorkPlans");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "3cf769eb-9c8d-4704-a0f3-8faf6a616ec2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cf769eb-9c8d-4704-a0f3-8faf6a616ec2");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ScheduleWorkPlans");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "TimeSlots",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "ScheduleWorkPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a8492ef9-580c-4eb2-9fa8-1d6a191a69ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "968e20ac-392f-4d02-9be7-aba24b7318fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "40c19536-b2b6-4db8-ae9b-fd9bd9bb1634");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "4146f98a-6bb4-4959-b5b1-770a3a600ef2");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6cc4db82-8435-46d4-a942-e302d58267a6", 0, "1b05869d-657c-47d1-ba80-71708381c400", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAELxgRYZqUkTN+lPukXn2t1hYavUGhVnZaMTqfXdvLA69ERn9pPF/O9DB73YpDypbfA==", null, false, null, null, "57039649-e792-4793-852a-f79dd8b1d026", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "6cc4db82-8435-46d4-a942-e302d58267a6" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWorkPlans_DoctorId",
                table: "ScheduleWorkPlans",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWorkPlans_DoctorProfiles_DoctorId",
                table: "ScheduleWorkPlans",
                column: "DoctorId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_VisitRequests_RequestId",
                table: "TimeSlots",
                column: "RequestId",
                principalTable: "VisitRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
