using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore5WebApp.DAL.Migrations {
    public partial class PersonParentChildren : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "PersonPerson",
                schema: "dbo",
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
                schema: "dbo",
                table: "PersonPerson",
                column: "ParetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "PersonPerson",
                schema: "dbo");

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
    }
}