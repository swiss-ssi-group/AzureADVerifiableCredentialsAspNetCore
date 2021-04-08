using Microsoft.EntityFrameworkCore.Migrations;

namespace NationalDrivingLicense.Migrations
{
    public partial class LicenseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenseType",
                table: "DriverLicences",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseType",
                table: "DriverLicences");
        }
    }
}
