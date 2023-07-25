using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charisma.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PatientUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PatientEntity_PatientId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientEntity",
                table: "PatientEntity");

            migrationBuilder.RenameTable(
                name: "PatientEntity",
                newName: "Patients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "PatientEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientEntity",
                table: "PatientEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PatientEntity_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "PatientEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
