using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class AddedStartAuctionJobIdPropertyToAuctionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartAuctionJobId",
                table: "Auctions",
                type: "nvarchar(38)",
                maxLength: 38,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartAuctionJobId",
                table: "Auctions");
        }
    }
}
