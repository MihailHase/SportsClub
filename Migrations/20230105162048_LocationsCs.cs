using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingStudio02.Migrations
{
    public partial class LocationsCs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "FitnessClass");

            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "FitnessClass",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessClass_LocationID",
                table: "FitnessClass",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_FitnessClass_Location_LocationID",
                table: "FitnessClass",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FitnessClass_Location_LocationID",
                table: "FitnessClass");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_FitnessClass_LocationID",
                table: "FitnessClass");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "FitnessClass");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "FitnessClass",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
