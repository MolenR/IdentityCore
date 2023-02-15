using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class BuildDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62b3adbb-e3a7-4f5f-8a05-61d2074df6c2",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "74119e76-39df-482d-b1f7-d785d5e53e7a", "admin@localhost.com", "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEBHJHbgjBPL8yysvFj/ywMqZWUFW50aZsCxup+IKDrQfEyNPovfeoyOmBNJQavDGmA==", "e9183599-04da-4c47-aa4f-f99c128eeb6a", "admin@localhost.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6c2adbb-4f5f-8a05-e3a7-62b3adbb61d2",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "865af82c-e670-4652-ad1f-84c2fb81a226", "user@localhost.com", "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEDWvg/jiagIZZZp1F0S/EgcqIT9Jb8SpHJbQCmO3zyqe0Kt6RbW5bQNPi7/KFt61YQ==", "27f6cde4-d8fa-4f89-8554-0b5d2b0dae55", "user@localhost.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62b3adbb-e3a7-4f5f-8a05-61d2074df6c2",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5f291dea-1113-476d-95f9-ca4992a79c8b", "admin@host.com", "ADMIN@HOST.COM", "ADMIN@HOST.COM", "AQAAAAIAAYagAAAAEOohjHH52hylO6ViHPdJDCUxAb9eA4aiQeIgAFua3F+6+seFtgRrOmgLC3nUsOGjDg==", "f44e3544-e0fc-4273-8ca9-aa5ef267defb", "admin@host.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6c2adbb-4f5f-8a05-e3a7-62b3adbb61d2",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e7f2a77d-bc16-45d5-814d-e7a59e5c1a9c", "user@login.com", "USER@LOGIN.COM", "USER@LOGIN.COM", "AQAAAAIAAYagAAAAEMJ43GBCHYzEQlG30hTzMVzchu+mRqqHG0rD+VLPVbzWH4Ll4vugBBSTlBJ4dL8/LQ==", "dbb6cf37-c580-4809-8805-4b4fe6cacf98", "user@login.com" });
        }
    }
}
