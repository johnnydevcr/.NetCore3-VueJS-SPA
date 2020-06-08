using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Database.Migrations
{
    public partial class DefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "3ac642d6-8ada-49a4-ac48-5bafafb58e86", "770effcf-376f-4adc-9987-6b596d4189de", "ApplicationRole", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "a0daa9c2-c9e3-4565-943a-7d75473de6f9", "097752cf-74bc-4905-8794-a99b4dee541b", "ApplicationRole", "Seller", "Seller" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ac642d6-8ada-49a4-ac48-5bafafb58e86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0daa9c2-c9e3-4565-943a-7d75473de6f9");
        }
    }
}
