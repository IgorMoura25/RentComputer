using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RC.Identity.API.Migrations
{
    public partial class PrivateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "SecurityKeys");

            migrationBuilder.CreateTable(
                name: "PrivateKey",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivateKey = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateKey", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivateKey");

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "SecurityKeys",
                type: "varchar(MAX)",
                nullable: true);
        }
    }
}
