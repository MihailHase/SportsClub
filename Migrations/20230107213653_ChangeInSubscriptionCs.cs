using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingStudio02.Migrations
{
    public partial class ChangeInSubscriptionCs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Subscription",
                newName: "StartingDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartingDate",
                table: "Subscription",
                newName: "ReturnDate");
        }
    }
}
