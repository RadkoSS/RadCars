using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class FixedAuctionThumbnailRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_CarImages_ThumbnailId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_CarImages_ThumbnailId",
                table: "Listings");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AuctionCarImages_ThumbnailId",
                table: "Auctions",
                column: "ThumbnailId",
                principalTable: "AuctionCarImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_CarImages_ThumbnailId",
                table: "Listings",
                column: "ThumbnailId",
                principalTable: "CarImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AuctionCarImages_ThumbnailId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_CarImages_ThumbnailId",
                table: "Listings");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_CarImages_ThumbnailId",
                table: "Auctions",
                column: "ThumbnailId",
                principalTable: "CarImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_CarImages_ThumbnailId",
                table: "Listings",
                column: "ThumbnailId",
                principalTable: "CarImages",
                principalColumn: "Id");
        }
    }
}
