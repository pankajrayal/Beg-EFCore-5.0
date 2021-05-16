using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore5WebApp.DAL.Migrations {
    public partial class AddStateAL : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Code", "Description", "LookUpType" },
                values: new object[] { "AL", "Alabama", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Code",
                keyValue: "AL");
        }
    }
}