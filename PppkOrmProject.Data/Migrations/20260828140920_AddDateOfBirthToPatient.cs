using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PppkOrmProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirthToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Patients",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Patients");
        }
    }
}
