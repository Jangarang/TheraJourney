using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheraJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TherapistUpdateAndTherapistApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Therapists");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Therapists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Therapists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "Therapists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Therapists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
