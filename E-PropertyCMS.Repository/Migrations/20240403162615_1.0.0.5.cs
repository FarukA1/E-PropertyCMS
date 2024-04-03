using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace E_PropertyCMS.Repository.Migrations
{
    public partial class _1005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_CaseStatus_CaseStatusId",
                table: "Case");

            migrationBuilder.DropTable(
                name: "CaseStatus");

            migrationBuilder.DropIndex(
                name: "IX_Case_CaseStatusId",
                table: "Case");

            migrationBuilder.RenameColumn(
                name: "CaseStatusId",
                table: "Case",
                newName: "CaseStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseStatus",
                table: "Case",
                newName: "CaseStatusId");

            migrationBuilder.CreateTable(
                name: "CaseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Case_CaseStatusId",
                table: "Case",
                column: "CaseStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_CaseStatus_CaseStatusId",
                table: "Case",
                column: "CaseStatusId",
                principalTable: "CaseStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
