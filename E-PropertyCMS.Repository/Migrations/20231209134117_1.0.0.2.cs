using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_PropertyCMS.Repository.Migrations
{
    public partial class _1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAllowed",
                table: "User",
                newName: "IsBlocked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "User",
                newName: "IsAllowed");
        }
    }
}
