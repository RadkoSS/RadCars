using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class AddedAuctionsAndAllRelatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    BlitzPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    MinimumBid = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    EngineModel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EngineTypeId = table.Column<int>(type: "int", nullable: false),
                    VinNumber = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarMakeId = table.Column<int>(type: "int", nullable: false),
                    CarModelId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auction_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auction_CarImages_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "CarImages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auction_CarMakes_CarMakeId",
                        column: x => x.CarMakeId,
                        principalTable: "CarMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auction_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auction_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auction_EngineTypes_EngineTypeId",
                        column: x => x.EngineTypeId,
                        principalTable: "EngineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuctionCarImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionCarImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionCarImage_Auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuctionFeature",
                columns: table => new
                {
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionFeature", x => new { x.AuctionId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_AuctionFeature_Auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionFeature_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteAuction",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteAuction", x => new { x.UserId, x.AuctionId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteAuction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFavoriteAuction_Auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CarMakeId",
                table: "Auction",
                column: "CarMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CarModelId",
                table: "Auction",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CityId",
                table: "Auction",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_CreatorId",
                table: "Auction",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_EngineTypeId",
                table: "Auction",
                column: "EngineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_IsDeleted",
                table: "Auction",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_ThumbnailId",
                table: "Auction",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionCarImage_AuctionId",
                table: "AuctionCarImage",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionCarImage_IsDeleted",
                table: "AuctionCarImage",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionFeature_FeatureId",
                table: "AuctionFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionFeature_IsDeleted",
                table: "AuctionFeature",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteAuction_AuctionId",
                table: "UserFavoriteAuction",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteAuction_IsDeleted",
                table: "UserFavoriteAuction",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionCarImage");

            migrationBuilder.DropTable(
                name: "AuctionFeature");

            migrationBuilder.DropTable(
                name: "UserFavoriteAuction");

            migrationBuilder.DropTable(
                name: "Auction");
        }
    }
}
