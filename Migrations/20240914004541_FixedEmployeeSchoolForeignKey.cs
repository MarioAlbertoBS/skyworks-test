using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyworksTest.Migrations
{
    /// <inheritdoc />
    public partial class FixedEmployeeSchoolForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Schools_GroupId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SchoolId",
                table: "Employees",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Schools_SchoolId",
                table: "Employees",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Schools_SchoolId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SchoolId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Schools_GroupId",
                table: "Employees",
                column: "GroupId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
