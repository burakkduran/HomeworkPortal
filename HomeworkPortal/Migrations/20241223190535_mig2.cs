using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeworkPortal.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Lessons");
        }
    }
}
