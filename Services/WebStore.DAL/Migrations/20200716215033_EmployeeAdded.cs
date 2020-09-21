using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class EmployeeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(   // Добавляется таблица Employees
                name: "Employees",
                columns: table => new   //  С колонками Id, Name, SurName ...
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    EmployementDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
