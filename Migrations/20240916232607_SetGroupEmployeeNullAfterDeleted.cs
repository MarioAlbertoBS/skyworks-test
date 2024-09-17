using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyworksTest.Migrations
{
    /// <inheritdoc />
    public partial class SetGroupEmployeeNullAfterDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Employees_EmployeeId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Employees_EmployeeId",
                table: "Groups",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Employees_EmployeeId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Employees_EmployeeId",
                table: "Groups",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
