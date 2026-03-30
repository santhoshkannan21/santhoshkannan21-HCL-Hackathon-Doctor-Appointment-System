using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoctorAppointmentSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    ConsultationFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModeArtifact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General Physician" },
                    { 2, "Pediatrics" },
                    { 3, "Dermatology" },
                    { 4, "Gynecology" },
                    { 5, "Orthopedics" },
                    { 6, "Cardiology" },
                    { 7, "Neurology" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "admin@hcl.com", "System Admin", "$2a$12$TbRfUUFzgHdh0lFSWPu0CeWZ.GSj634SJrs8Ww1cnWcfNWwxhlDwe", 1 },
                    { 2, "rahul@test.com", "Patient Rahul", "$2a$12$hGOeop7IbSbMcljWnNESIef.vOQUaOUqDmn0B9NZrd1JQPhna1QIW", 0 },
                    { 3, "priya@test.com", "Patient Priya", "$2a$12$hGOeop7IbSbMcljWnNESIef.vOQUaOUqDmn0B9NZrd1JQPhna1QIW", 0 },
                    { 4, "john@hospital.com", "Dr. John Smith", "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", 2 },
                    { 5, "sarah@hospital.com", "Dr. Sarah Lee", "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", 2 },
                    { 6, "mike@hospital.com", "Dr. Mike Tyson", "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", 2 },
                    { 7, "emily@hospital.com", "Dr. Emily Davis", "$2a$12$ovyrpA80WJR5Kd9s8TpsQOUdZpD3aof9vEI..6pCkg1c7YKHPZAWG", 2 }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "ConsultationFee", "Mode", "Name", "SpecialtyId", "UserId" },
                values: new object[,]
                {
                    { 1, 500m, 0, "Dr. John Smith", 6, 4 },
                    { 2, 800m, 1, "Dr. Sarah Lee", 3, 5 },
                    { 3, 600m, 0, "Dr. Mike Tyson", 7, 6 },
                    { 4, 1000m, 1, "Dr. Emily Davis", 5, 7 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "DoctorId", "ModeArtifact", "PatientId", "Status", "TimeSlot", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "https://meet.hcl.com/room-john", 2, 0, 0, 500m },
                    { 2, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Apollo Clinic, Room 101", 3, 0, 1, 800m },
                    { 3, new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "https://meet.hcl.com/room-mike", 2, 1, 2, 600m },
                    { 4, new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Apollo Clinic, Room 102", 3, 3, 3, 1000m },
                    { 5, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "https://meet.hcl.com/room-john", 2, 2, 4, 500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate_TimeSlot",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDate", "TimeSlot" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
