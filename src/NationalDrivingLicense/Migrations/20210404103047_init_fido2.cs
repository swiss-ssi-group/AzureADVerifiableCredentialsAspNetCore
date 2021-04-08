using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NationalDrivingLicense.Migrations
{
    public partial class init_fido2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FidoStoredCredential",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PublicKey = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserHandle = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SignatureCounter = table.Column<long>(type: "bigint", nullable: false),
                    CredType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AaGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptorJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FidoStoredCredential", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FidoStoredCredential");
        }
    }
}
