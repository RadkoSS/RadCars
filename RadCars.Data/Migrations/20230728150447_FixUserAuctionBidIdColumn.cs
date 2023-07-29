using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class FixUserAuctionBidIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAuctionBids",
                table: "UserAuctionBids");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserAuctionBids",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAuctionBids",
                table: "UserAuctionBids",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuctionBids_BidderId",
                table: "UserAuctionBids",
                column: "BidderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAuctionBids",
                table: "UserAuctionBids");

            migrationBuilder.DropIndex(
                name: "IX_UserAuctionBids_BidderId",
                table: "UserAuctionBids");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAuctionBids");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAuctionBids",
                table: "UserAuctionBids",
                columns: new[] { "BidderId", "AuctionId" });
        }
    }
}
