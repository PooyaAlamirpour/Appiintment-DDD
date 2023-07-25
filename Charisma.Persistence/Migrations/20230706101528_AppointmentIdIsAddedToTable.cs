using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charisma.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentIdIsAddedToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Appointments");
        }
    }
}
