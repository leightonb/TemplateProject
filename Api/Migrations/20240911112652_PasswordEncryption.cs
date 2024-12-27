using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateProject.Migrations
{
    /// <inheritdoc />
    public partial class PasswordEncryption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Users",
                type: "BLOB",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordIV",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordKey",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "LocaleId",
                table: "UserAccess",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "UserAccess",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_LocaleId",
                table: "UserAccess",
                column: "LocaleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_RegionId",
                table: "UserAccess",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_Locales_LocaleId",
                table: "UserAccess",
                column: "LocaleId",
                principalTable: "Locales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccess_Regions_RegionId",
                table: "UserAccess",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_Locales_LocaleId",
                table: "UserAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccess_Regions_RegionId",
                table: "UserAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserAccess_LocaleId",
                table: "UserAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserAccess_RegionId",
                table: "UserAccess");

            migrationBuilder.DropColumn(
                name: "PasswordIV",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocaleId",
                table: "UserAccess");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "UserAccess");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");
        }
    }
}
