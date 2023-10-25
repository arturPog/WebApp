using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class _25102023CD4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tytul",
                table: "SliderItems",
                newName: "Nazwa");

            migrationBuilder.RenameColumn(
                name: "CzyAktywny",
                table: "SliderItems",
                newName: "IsActive");

            migrationBuilder.AddColumn<string>(
                name: "Nazwa",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nazwa",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "StatusName",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Nazwa",
                table: "SliderItems",
                newName: "Tytul");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "SliderItems",
                newName: "CzyAktywny");
        }
    }
}
