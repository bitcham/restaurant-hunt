using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clinicians_ClinicianId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TimeSlots_TimeSlotId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinicians_Users_UserId",
                table: "Clinicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_UserId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_Users_user_id",
                table: "refresh_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Clinicians_ClinicianId",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clinicians",
                table: "Clinicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "patients");

            migrationBuilder.RenameTable(
                name: "Clinicians",
                newName: "clinicians");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "appointments");

            migrationBuilder.RenameTable(
                name: "TimeSlots",
                newName: "time_slots");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_UserId",
                table: "patients",
                newName: "IX_patients_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Clinicians_UserId",
                table: "clinicians",
                newName: "IX_clinicians_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_TimeSlotId",
                table: "appointments",
                newName: "IX_appointments_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "appointments",
                newName: "IX_appointments_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ClinicianId",
                table: "appointments",
                newName: "IX_appointments_ClinicianId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlots_ClinicianId_StartTime",
                table: "time_slots",
                newName: "IX_time_slots_ClinicianId_StartTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_patients",
                table: "patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clinicians",
                table: "clinicians",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appointments",
                table: "appointments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_time_slots",
                table: "time_slots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_clinicians_ClinicianId",
                table: "appointments",
                column: "ClinicianId",
                principalTable: "clinicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patients_PatientId",
                table: "appointments",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_time_slots_TimeSlotId",
                table: "appointments",
                column: "TimeSlotId",
                principalTable: "time_slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clinicians_users_UserId",
                table: "clinicians",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_users_UserId",
                table: "patients",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_user_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_time_slots_clinicians_ClinicianId",
                table: "time_slots",
                column: "ClinicianId",
                principalTable: "clinicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_clinicians_ClinicianId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patients_PatientId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_time_slots_TimeSlotId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_clinicians_users_UserId",
                table: "clinicians");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_users_UserId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_user_id",
                table: "refresh_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_time_slots_clinicians_ClinicianId",
                table: "time_slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patients",
                table: "patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clinicians",
                table: "clinicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appointments",
                table: "appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_time_slots",
                table: "time_slots");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "patients",
                newName: "Patients");

            migrationBuilder.RenameTable(
                name: "clinicians",
                newName: "Clinicians");

            migrationBuilder.RenameTable(
                name: "appointments",
                newName: "Appointments");

            migrationBuilder.RenameTable(
                name: "time_slots",
                newName: "TimeSlots");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_patients_UserId",
                table: "Patients",
                newName: "IX_Patients_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_clinicians_UserId",
                table: "Clinicians",
                newName: "IX_Clinicians_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_TimeSlotId",
                table: "Appointments",
                newName: "IX_Appointments_TimeSlotId");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_ClinicianId",
                table: "Appointments",
                newName: "IX_Appointments_ClinicianId");

            migrationBuilder.RenameIndex(
                name: "IX_time_slots_ClinicianId_StartTime",
                table: "TimeSlots",
                newName: "IX_TimeSlots_ClinicianId_StartTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clinicians",
                table: "Clinicians",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clinicians_ClinicianId",
                table: "Appointments",
                column: "ClinicianId",
                principalTable: "Clinicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TimeSlots_TimeSlotId",
                table: "Appointments",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinicians_Users_UserId",
                table: "Clinicians",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_Users_user_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Clinicians_ClinicianId",
                table: "TimeSlots",
                column: "ClinicianId",
                principalTable: "Clinicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
