using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedicalVisits.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkingScheduleToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e425c663-c590-4edc-8d4c-1b777f967002" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e425c663-c590-4edc-8d4c-1b777f967002");

            migrationBuilder.AddColumn<int>(
                name: "DoctorProfileId",
                table: "VisitRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorProfileId = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsHoliday = table.Column<bool>(type: "boolean", nullable: false),
                    VisitRequestId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSchedules_DoctorProfiles_DoctorProfileId",
                        column: x => x.DoctorProfileId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkSchedules_VisitRequests_VisitRequestId",
                        column: x => x.VisitRequestId,
                        principalTable: "VisitRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9dd7e518-57de-46b9-8d8d-3d7271a3fe74");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "af6ddd74-e00e-4239-a25d-6fd239c6181e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "5369c446-2b33-4396-a114-5a2193e622b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "d0de163a-47aa-46e2-bc7f-f1611e92b4b2");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4c76a8db-983e-43f7-b4b7-0250fa080d9f", 0, "8ad3fa3e-5fbf-451e-a198-249112fdfd19", "admin@medicalvisits.com", true, "Admin", "User", false, null, "ADMIN@MEDICALVISITS.COM", "ADMIN@MEDICALVISITS.COM", "AQAAAAIAAYagAAAAEKxBd47u/0Xhz3fB0nxQlpEz8o5Jr2YEom94irons/fK+XeYyRL5hf2XzDlqkvnjZA==", null, false, null, null, "d4be13c8-5cd8-4e41-ba6c-61c25cc734e3", false, "admin@medicalvisits.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4c76a8db-983e-43f7-b4b7-0250fa080d9f" });

            migrationBuilder.CreateIndex(
                name: "IX_VisitRequests_DoctorProfileId",
                table: "VisitRequests",
                column: "DoctorProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_DoctorProfileId_DayOfWeek",
                table: "WorkSchedules",
                columns: new[] { "DoctorProfileId", "DayOfWeek" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_VisitRequestId",
                table: "WorkSchedules",
                column: "VisitRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitRequests_DoctorProfiles_DoctorProfileId",
                table: "VisitRequests",
                column: "DoctorProfileId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitRequests_DoctorProfiles_DoctorProfileId",
                table: "VisitRequests");

            migrationBuilder.DropTable(
                name: "WorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_VisitRequests_DoctorProfileId",
                table: "VisitRequests");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "4c76a8db-983e-43f7-b4b7-0250fa080d9f" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c76a8db-983e-43f7-b4b7-0250fa080d9f");

            migrationBuilder.DropColumn(
                name: "DoctorProfileId",
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
    }
}
