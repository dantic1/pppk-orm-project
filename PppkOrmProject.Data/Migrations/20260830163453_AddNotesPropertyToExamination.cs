using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PppkOrmProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesPropertyToExamination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Examinations",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Examinations");
        }
    }
}
