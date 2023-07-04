using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class RemovedCountryFromListingForNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Countries_CountryId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CountryId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Listings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CountryId",
                table: "Listings",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Countries_CountryId",
                table: "Listings",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
