using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Physiocure.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorFeePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Doctors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RadiusKm",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkingType",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "RadiusKm",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WorkingType",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "Fee",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
