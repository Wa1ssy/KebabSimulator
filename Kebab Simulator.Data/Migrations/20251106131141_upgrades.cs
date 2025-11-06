using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kebab_Simulator.Data.Migrations
{
    /// <inheritdoc />
    public partial class upgrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarLevel",
                table: "Kebabs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HouseLevel",
                table: "Kebabs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarLevel",
                table: "Kebabs");

            migrationBuilder.DropColumn(
                name: "HouseLevel",
                table: "Kebabs");
        }
    }
}
