using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_superhero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false,defaultValue: "NA"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false,defaultValue: "NA"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false,defaultValue: "NA"),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false,defaultValue:"NA"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false,defaultValueSql:"GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_superhero", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_superhero");
        }
    }
}
