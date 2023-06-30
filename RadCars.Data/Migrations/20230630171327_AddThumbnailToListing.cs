using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class AddThumbnailToListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailId",
                table: "Listings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ThumbnailId",
                table: "Listings",
                column: "ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_CarPictures_ThumbnailId",
                table: "Listings",
                column: "ThumbnailId",
                principalTable: "CarPictures",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_CarPictures_ThumbnailId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_ThumbnailId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Listings");
        }
    }
}
