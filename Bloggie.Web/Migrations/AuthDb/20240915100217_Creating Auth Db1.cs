using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class CreatingAuthDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ddd909dc-683f-44e1-b481-22e304ecf37f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "316a41f8-1265-47b1-b46c-890b1060b901", "AQAAAAIAAYagAAAAEFkdQ4+fKBjZOs1p2joqKZCPPE/lsL1K5MD8Zai6udCekRgeuw8RCR1P+lr+S0Gq8Q==", "ee768728-d6cc-452b-9567-597c16fdf454" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ddd909dc-683f-44e1-b481-22e304ecf37f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6397b41-1a21-4038-bf7f-5d97f88d53d8", "AQAAAAIAAYagAAAAEBCKD6pKhwbAsirJsb2e4PuU0qOZ24Irwn3FeezQ7+Q4q5v5DE4ML5VOFFLWWG0Qkg==", "6d183520-fba3-414b-a4c1-06b87b634d8e" });
        }
    }
}
