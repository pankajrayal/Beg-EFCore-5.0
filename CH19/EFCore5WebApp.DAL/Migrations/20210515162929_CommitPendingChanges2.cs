using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore5WebApp.DAL.Migrations
{
    public partial class CommitPendingChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 21, 59, 29, 267, DateTimeKind.Local).AddTicks(8629));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 21, 59, 29, 268, DateTimeKind.Local).AddTicks(5563));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 21, 56, 33, 624, DateTimeKind.Local).AddTicks(8966));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 21, 56, 33, 625, DateTimeKind.Local).AddTicks(6714));
        }
    }
}
