using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore5WebApp.DAL.Migrations
{
    public partial class AddedCreatedOnONPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 16, 3, 19, 523, DateTimeKind.Local).AddTicks(4505));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 16, 3, 19, 524, DateTimeKind.Local).AddTicks(6316));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Persons");
        }
    }
}
