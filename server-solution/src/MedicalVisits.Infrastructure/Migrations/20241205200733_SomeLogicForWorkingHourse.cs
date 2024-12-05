using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SomeLogicForWorkingHourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_VisitRequests_VisitRequestId",
                table: "WorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_VisitRequestId",
                table: "WorkSchedules");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "652234d8-4bd8-4e40-a3bb-162421c16339" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "652234d8-4bd8-4e40-a3bb-162421c16339");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "WorkSchedules");

            migrationBuilder.AddColumn<int>(
                name: "TimeSlotId",
                table: "VisitRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DefaultStartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DefaultEndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorWorkSchedules_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScheduleId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_DoctorWorkSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "DoctorWorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_VisitRequests_TimeSlotId",
                table: "VisitRequests",
                column: "TimeSlotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorWorkSchedules_DoctorId",
                table: "DoctorWorkSchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ScheduleId_Date_Status",
                table: "TimeSlots",
                columns: new[] { "ScheduleId", "Date", "Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_VisitRequests_TimeSlots_TimeSlotId",
                table: "VisitRequests",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitRequests_TimeSlots_TimeSlotId",
                table: "VisitRequests");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "DoctorWorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_VisitRequests_TimeSlotId",
                table: "VisitRequests");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "04ea4b9a-f870-4afb-b885-d470ade3cb8a" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04ea4b9a-f870-4afb-b885-d470ade3cb8a");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "VisitRequests");

            migrationBuilder.AddColumn<int>(
                name: "VisitRequestId",
                table: "WorkSchedules",
                type: "integer",
                nullable: true);

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
                name: "IX_WorkSchedules_VisitRequestId",
                table: "WorkSchedules",
                column: "VisitRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_VisitRequests_VisitRequestId",
                table: "WorkSchedules",
                column: "VisitRequestId",
                principalTable: "VisitRequests",
                principalColumn: "Id");
        }
    }
}
