using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyworksTest.Migrations
{
    /// <inheritdoc />
    public partial class DropSchoolIdFromStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students"
            );

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId",
                table: "Students"
            );

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Students"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add the SchoolId column
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            // Re-add the foreign key constraint
            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Students_School_SchoolId",
                table: "Student",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Students",
                type: "int",
                nullable: true
            );
        }
    }
}
