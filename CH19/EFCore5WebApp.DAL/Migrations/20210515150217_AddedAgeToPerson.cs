using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore5WebApp.DAL.Migrations {
    public partial class AddedAgeToPerson : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "PersonPerson");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "dbo",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "CreatedOn" },
                values: new object[] { 20, new DateTime(2021, 5, 15, 20, 32, 16, 671, DateTimeKind.Local).AddTicks(2952) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "CreatedOn" },
                values: new object[] { 30, new DateTime(2021, 5, 15, 20, 32, 16, 672, DateTimeKind.Local).AddTicks(7920) });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.CreateTable(
                name: "PersonPerson",
                columns: table => new {
                    ChildrenId = table.Column<int>(type: "int", nullable: false),
                    ParetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PersonPerson", x => new { x.ChildrenId, x.ParetsId });
                    table.ForeignKey(
                        name: "FK_PersonPerson_Persons_ChildrenId",
                        column: x => x.ChildrenId,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPerson_Persons_ParetsId",
                        column: x => x.ParetsId,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 16, 52, 16, 453, DateTimeKind.Local).AddTicks(6080));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 5, 15, 16, 52, 16, 454, DateTimeKind.Local).AddTicks(2231));

            migrationBuilder.CreateIndex(
                name: "IX_PersonPerson_ParetsId",
                table: "PersonPerson",
                column: "ParetsId");
        }
    }
}