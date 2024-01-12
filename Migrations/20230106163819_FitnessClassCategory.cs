using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingStudio02.Migrations
{
    public partial class FitnessClassCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FitnessClassCategory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FitnessClassID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessClassCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FitnessClassCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FitnessClassCategory_FitnessClass_FitnessClassID",
                        column: x => x.FitnessClassID,
                        principalTable: "FitnessClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessClassCategory_CategoryID",
                table: "FitnessClassCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessClassCategory_FitnessClassID",
                table: "FitnessClassCategory",
                column: "FitnessClassID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessClassCategory");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
