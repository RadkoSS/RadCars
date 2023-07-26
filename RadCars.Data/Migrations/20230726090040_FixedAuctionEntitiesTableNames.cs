using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class FixedAuctionEntitiesTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auction_AspNetUsers_CreatorId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_CarImages_ThumbnailId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_CarMakes_CarMakeId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_CarModels_CarModelId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_Cities_CityId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_EngineTypes_EngineTypeId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionCarImage_Auction_AuctionId",
                table: "AuctionCarImage");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionFeature_Auction_AuctionId",
                table: "AuctionFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionFeature_Features_FeatureId",
                table: "AuctionFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAuction_AspNetUsers_UserId",
                table: "UserFavoriteAuction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAuction_Auction_AuctionId",
                table: "UserFavoriteAuction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteAuction",
                table: "UserFavoriteAuction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionFeature",
                table: "AuctionFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionCarImage",
                table: "AuctionCarImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auction",
                table: "Auction");

            migrationBuilder.RenameTable(
                name: "UserFavoriteAuction",
                newName: "UserFavoriteAuctions");

            migrationBuilder.RenameTable(
                name: "AuctionFeature",
                newName: "AuctionFeatures");

            migrationBuilder.RenameTable(
                name: "AuctionCarImage",
                newName: "AuctionCarImages");

            migrationBuilder.RenameTable(
                name: "Auction",
                newName: "Auctions");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteAuction_IsDeleted",
                table: "UserFavoriteAuctions",
                newName: "IX_UserFavoriteAuctions_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteAuction_AuctionId",
                table: "UserFavoriteAuctions",
                newName: "IX_UserFavoriteAuctions_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionFeature_IsDeleted",
                table: "AuctionFeatures",
                newName: "IX_AuctionFeatures_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionFeature_FeatureId",
                table: "AuctionFeatures",
                newName: "IX_AuctionFeatures_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionCarImage_IsDeleted",
                table: "AuctionCarImages",
                newName: "IX_AuctionCarImages_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionCarImage_AuctionId",
                table: "AuctionCarImages",
                newName: "IX_AuctionCarImages_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_ThumbnailId",
                table: "Auctions",
                newName: "IX_Auctions_ThumbnailId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_IsDeleted",
                table: "Auctions",
                newName: "IX_Auctions_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_EngineTypeId",
                table: "Auctions",
                newName: "IX_Auctions_EngineTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_CreatorId",
                table: "Auctions",
                newName: "IX_Auctions_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_CityId",
                table: "Auctions",
                newName: "IX_Auctions_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_CarModelId",
                table: "Auctions",
                newName: "IX_Auctions_CarModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_CarMakeId",
                table: "Auctions",
                newName: "IX_Auctions_CarMakeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteAuctions",
                table: "UserFavoriteAuctions",
                columns: new[] { "UserId", "AuctionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionFeatures",
                table: "AuctionFeatures",
                columns: new[] { "AuctionId", "FeatureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionCarImages",
                table: "AuctionCarImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserAuctionBids",
                columns: table => new
                {
                    BidderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAuctionBids", x => new { x.BidderId, x.AuctionId });
                    table.ForeignKey(
                        name: "FK_UserAuctionBids_AspNetUsers_BidderId",
                        column: x => x.BidderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAuctionBids_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAuctionBids_AuctionId",
                table: "UserAuctionBids",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAuctionBids_IsDeleted",
                table: "UserAuctionBids",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionCarImages_Auctions_AuctionId",
                table: "AuctionCarImages",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionFeatures_Auctions_AuctionId",
                table: "AuctionFeatures",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionFeatures_Features_FeatureId",
                table: "AuctionFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_CreatorId",
                table: "Auctions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_CarImages_ThumbnailId",
                table: "Auctions",
                column: "ThumbnailId",
                principalTable: "CarImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_CarMakes_CarMakeId",
                table: "Auctions",
                column: "CarMakeId",
                principalTable: "CarMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_CarModels_CarModelId",
                table: "Auctions",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Cities_CityId",
                table: "Auctions",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_EngineTypes_EngineTypeId",
                table: "Auctions",
                column: "EngineTypeId",
                principalTable: "EngineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAuctions_AspNetUsers_UserId",
                table: "UserFavoriteAuctions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAuctions_Auctions_AuctionId",
                table: "UserFavoriteAuctions",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionCarImages_Auctions_AuctionId",
                table: "AuctionCarImages");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionFeatures_Auctions_AuctionId",
                table: "AuctionFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionFeatures_Features_FeatureId",
                table: "AuctionFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_CreatorId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_CarImages_ThumbnailId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_CarMakes_CarMakeId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_CarModels_CarModelId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Cities_CityId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_EngineTypes_EngineTypeId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAuctions_AspNetUsers_UserId",
                table: "UserFavoriteAuctions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteAuctions_Auctions_AuctionId",
                table: "UserFavoriteAuctions");

            migrationBuilder.DropTable(
                name: "UserAuctionBids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteAuctions",
                table: "UserFavoriteAuctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionFeatures",
                table: "AuctionFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionCarImages",
                table: "AuctionCarImages");

            migrationBuilder.RenameTable(
                name: "UserFavoriteAuctions",
                newName: "UserFavoriteAuction");

            migrationBuilder.RenameTable(
                name: "Auctions",
                newName: "Auction");

            migrationBuilder.RenameTable(
                name: "AuctionFeatures",
                newName: "AuctionFeature");

            migrationBuilder.RenameTable(
                name: "AuctionCarImages",
                newName: "AuctionCarImage");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteAuctions_IsDeleted",
                table: "UserFavoriteAuction",
                newName: "IX_UserFavoriteAuction_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteAuctions_AuctionId",
                table: "UserFavoriteAuction",
                newName: "IX_UserFavoriteAuction_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_ThumbnailId",
                table: "Auction",
                newName: "IX_Auction_ThumbnailId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_IsDeleted",
                table: "Auction",
                newName: "IX_Auction_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_EngineTypeId",
                table: "Auction",
                newName: "IX_Auction_EngineTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CreatorId",
                table: "Auction",
                newName: "IX_Auction_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CityId",
                table: "Auction",
                newName: "IX_Auction_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CarModelId",
                table: "Auction",
                newName: "IX_Auction_CarModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CarMakeId",
                table: "Auction",
                newName: "IX_Auction_CarMakeId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionFeatures_IsDeleted",
                table: "AuctionFeature",
                newName: "IX_AuctionFeature_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionFeatures_FeatureId",
                table: "AuctionFeature",
                newName: "IX_AuctionFeature_FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionCarImages_IsDeleted",
                table: "AuctionCarImage",
                newName: "IX_AuctionCarImage_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionCarImages_AuctionId",
                table: "AuctionCarImage",
                newName: "IX_AuctionCarImage_AuctionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteAuction",
                table: "UserFavoriteAuction",
                columns: new[] { "UserId", "AuctionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auction",
                table: "Auction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionFeature",
                table: "AuctionFeature",
                columns: new[] { "AuctionId", "FeatureId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionCarImage",
                table: "AuctionCarImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_AspNetUsers_CreatorId",
                table: "Auction",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_CarImages_ThumbnailId",
                table: "Auction",
                column: "ThumbnailId",
                principalTable: "CarImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_CarMakes_CarMakeId",
                table: "Auction",
                column: "CarMakeId",
                principalTable: "CarMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_CarModels_CarModelId",
                table: "Auction",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_Cities_CityId",
                table: "Auction",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_EngineTypes_EngineTypeId",
                table: "Auction",
                column: "EngineTypeId",
                principalTable: "EngineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionCarImage_Auction_AuctionId",
                table: "AuctionCarImage",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionFeature_Auction_AuctionId",
                table: "AuctionFeature",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionFeature_Features_FeatureId",
                table: "AuctionFeature",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAuction_AspNetUsers_UserId",
                table: "UserFavoriteAuction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteAuction_Auction_AuctionId",
                table: "UserFavoriteAuction",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
