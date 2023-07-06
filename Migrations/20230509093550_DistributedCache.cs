using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth0UserProfileDisplayStarterKit.Migrations
{
    public partial class DistributedCache : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "tblAccessTokenCache",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(449)", maxLength: 449, nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ExpiresAtTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(type: "bigint", nullable: true),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccessTokenCache", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserContactEmail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAddress = table.Column<string>(type: "nvarchar(37)", maxLength: 37, nullable: true),
                    UserPostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCountry = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserState = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAccessTokenCache_ExpiresAtTime",
                schema: "security",
                table: "tblAccessTokenCache",
                column: "ExpiresAtTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAccessTokenCache",
                schema: "security");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
