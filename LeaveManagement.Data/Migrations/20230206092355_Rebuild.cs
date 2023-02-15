using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62b3adbb-e3a7-4f5f-8a05-61d2074df6c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26694cdc-8de5-4d51-9d89-434ca9bd8437", "AQAAAAIAAYagAAAAEGaSv8c3MJsN6NSWLMVNvhiMYYOgBbJnuVmQ9RiWcWNJDMOHf/6xiM5/qhsACkAZUA==", "65b132fd-3e74-422e-ab41-12b2efc5dd09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6c2adbb-4f5f-8a05-e3a7-62b3adbb61d2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d865106c-a901-4ad4-b8b0-0989c5a461f2", "AQAAAAIAAYagAAAAECh2wzuOwufF0Ftdl///sIzU5wSpoyjmzmWMGTIYvjO5nLzh7CyHap917mJODSGykQ==", "85df9a5a-06fb-44d3-bcf8-c2739a9075e4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62b3adbb-e3a7-4f5f-8a05-61d2074df6c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74119e76-39df-482d-b1f7-d785d5e53e7a", "AQAAAAIAAYagAAAAEBHJHbgjBPL8yysvFj/ywMqZWUFW50aZsCxup+IKDrQfEyNPovfeoyOmBNJQavDGmA==", "e9183599-04da-4c47-aa4f-f99c128eeb6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f6c2adbb-4f5f-8a05-e3a7-62b3adbb61d2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "865af82c-e670-4652-ad1f-84c2fb81a226", "AQAAAAIAAYagAAAAEDWvg/jiagIZZZp1F0S/EgcqIT9Jb8SpHJbQCmO3zyqe0Kt6RbW5bQNPi7/KFt61YQ==", "27f6cde4-d8fa-4f89-8554-0b5d2b0dae55" });
        }
    }
}
