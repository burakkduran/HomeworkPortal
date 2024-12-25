using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkPortal.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Lessons",
                newName: "GradeId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CategoryId",
                table: "Lessons",
                newName: "IX_Lessons_GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Categories_GradeId",
                table: "Lessons",
                column: "GradeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Categories_GradeId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "Lessons",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_GradeId",
                table: "Lessons",
                newName: "IX_Lessons_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Categories_CategoryId",
                table: "Lessons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
