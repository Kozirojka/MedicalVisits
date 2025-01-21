using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SomeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "ScheduleWorkPlans");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b");

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    MinimumAppointments = table.Column<int>(type: "integer", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: true),
                    StartInterval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndInterval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DoctorScheduleId = table.Column<int>(type: "integer", nullable: false),
                    VisitRequestId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorIntervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorIntervals_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorIntervals_DoctorSchedules_DoctorScheduleId",
                        column: x => x.DoctorScheduleId,
                        principalTable: "DoctorSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorIntervals_VisitRequests_VisitRequestId",
                        column: x => x.VisitRequestId,
                        principalTable: "VisitRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorIntervals_DoctorId",
                table: "DoctorIntervals",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorIntervals_DoctorScheduleId",
                table: "DoctorIntervals",
                column: "DoctorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorIntervals_VisitRequestId",
                table: "DoctorIntervals",
                column: "VisitRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorIntervals");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.CreateTable(
                name: "ScheduleWorkPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    NameOfSchedule = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleWorkPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleWorkPlans_AspNetUsers_UserId",
                        column: x => x.UserId,
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
                    RequestId = table.Column<int>(type: "integer", nullable: true),
                    WorkPlanId = table.Column<int>(type: "integer", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_ScheduleWorkPlans_WorkPlanId",
                        column: x => x.WorkPlanId,
                        principalTable: "ScheduleWorkPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSlots_VisitRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "VisitRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b", 0, "81c1aaff-cc44-4fc8-a3b9-428261f4f202", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEOWOla5e4Q8t2SKTHliFekkGK/c1QjoS20db+ut5B8g1K4cKqNmDZNsrHUFF526Fkg==", null, false, null, null, "f49b4cd7-598f-46dd-b1cf-d1cc61526529", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "45e6a81c-a766-45cf-b8ff-ca0c8a9c477b" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWorkPlans_UserId",
                table: "ScheduleWorkPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_RequestId",
                table: "TimeSlots",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_WorkPlanId",
                table: "TimeSlots",
                column: "WorkPlanId");
        }
    }
}
