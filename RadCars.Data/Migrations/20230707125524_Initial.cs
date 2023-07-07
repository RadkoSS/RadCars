using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarMakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMakes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CarMakeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarMakes_CarMakeId",
                        column: x => x.CarMakeId,
                        principalTable: "CarMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<long>(type: "bigint", nullable: false),
                    EngineModel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EngineTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    VinNumber = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarMakeId = table.Column<int>(type: "int", nullable: false),
                    CarModelId = table.Column<int>(type: "int", nullable: false),
                    ThumbnailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listings_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_CarImages_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "CarImages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listings_CarMakes_CarMakeId",
                        column: x => x.CarMakeId,
                        principalTable: "CarMakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_EngineTypes_EngineTypeId",
                        column: x => x.EngineTypeId,
                        principalTable: "EngineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListingFeatures",
                columns: table => new
                {
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingFeatures", x => new { x.ListingId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_ListingFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingFeatures_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteListings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteListings", x => new { x.UserId, x.ListingId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteListings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteListings_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarMakes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Acura" },
                    { 2, "Alfa Romeo" },
                    { 3, "Aston Martin" },
                    { 4, "Audi" },
                    { 5, "Bentley" },
                    { 6, "BMW" },
                    { 7, "Brilliance" },
                    { 8, "Bugatti" },
                    { 9, "Buick" },
                    { 10, "BYD" },
                    { 11, "Cadillac" },
                    { 12, "Changan" },
                    { 13, "Chery" },
                    { 14, "Chevrolet" },
                    { 15, "Chrysler" },
                    { 16, "Citroen" },
                    { 17, "Dacia" },
                    { 18, "Daewoo" },
                    { 19, "Daihatsu" },
                    { 20, "Datsun" },
                    { 21, "Dodge" },
                    { 22, "Dongfeng" },
                    { 23, "Exeed" },
                    { 24, "FAW" },
                    { 25, "Ferrari" },
                    { 26, "Fiat" },
                    { 27, "Fisker" },
                    { 28, "Ford" },
                    { 29, "Foton" },
                    { 30, "GAC" },
                    { 31, "GAZ" },
                    { 32, "Geely" },
                    { 33, "Genesis" },
                    { 34, "GMC" },
                    { 35, "Great Wall" },
                    { 36, "Haval" },
                    { 37, "Holden" },
                    { 38, "Honda" },
                    { 39, "Hummer" },
                    { 40, "Hyundai" },
                    { 41, "Infiniti" },
                    { 42, "Isuzu" }
                });

            migrationBuilder.InsertData(
                table: "CarMakes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 43, "Iveco" },
                    { 44, "Jac" },
                    { 45, "Jaguar" },
                    { 46, "Jeep" },
                    { 47, "Kia" },
                    { 48, "Lamborghini" },
                    { 49, "Lancia" },
                    { 50, "Land Rover" },
                    { 51, "Lexus" },
                    { 52, "Lifan" },
                    { 53, "Lincoln" },
                    { 54, "Lotus" },
                    { 55, "Marussia" },
                    { 56, "Maserati" },
                    { 57, "Maybach" },
                    { 58, "Mazda" },
                    { 59, "McLaren" },
                    { 60, "Mercedes" },
                    { 61, "Mercury" },
                    { 62, "MG" },
                    { 63, "Mini" },
                    { 64, "Mitsubishi" },
                    { 65, "Nissan" },
                    { 66, "Opel" },
                    { 67, "Peugeot" },
                    { 68, "Plymouth" },
                    { 69, "Pontiac" },
                    { 70, "Porsche" },
                    { 71, "Ravon" },
                    { 72, "Renault" },
                    { 73, "Rolls-Royce" },
                    { 74, "Rover" },
                    { 75, "Saab" },
                    { 76, "Saturn" },
                    { 77, "Scion" },
                    { 78, "Seat" },
                    { 79, "Skoda" },
                    { 80, "Smart" },
                    { 81, "Ssang Yong" },
                    { 82, "Subaru" },
                    { 83, "Suzuki" },
                    { 84, "Tesla" }
                });

            migrationBuilder.InsertData(
                table: "CarMakes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 85, "Toyota" },
                    { 86, "UAZ" },
                    { 87, "VAZ" },
                    { 88, "Volkswagen" },
                    { 89, "Volvo" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Системи за безопасност" },
                    { 2, "Системи за комфорт" },
                    { 3, "Системи за защита" },
                    { 4, "Платени разходи" },
                    { 5, "Вътрешни екстри" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Bulgaria" });

            migrationBuilder.InsertData(
                table: "EngineTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Бензин" },
                    { (byte)2, "Дизел" },
                    { (byte)3, "Газ / Бензин" },
                    { (byte)4, "Метан / Бензин" },
                    { (byte)5, "Електрически" },
                    { (byte)6, "Хибрид" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "CDX" },
                    { 2, 1, "CL" },
                    { 3, 1, "EL" },
                    { 4, 1, "ILX" },
                    { 5, 1, "Integra" },
                    { 6, 1, "MDX" },
                    { 7, 1, "NSX" },
                    { 8, 1, "RDX" },
                    { 9, 1, "RL" },
                    { 10, 1, "RLX" },
                    { 11, 1, "RSX" },
                    { 12, 1, "TL" },
                    { 13, 1, "TLX" },
                    { 14, 1, "TLX-L" },
                    { 15, 1, "TSX" },
                    { 16, 1, "ZDX" },
                    { 17, 2, "146" },
                    { 18, 2, "147" },
                    { 19, 2, "147 GTA" },
                    { 20, 2, "156" },
                    { 21, 2, "156 GTA" },
                    { 22, 2, "159" },
                    { 23, 2, "166" },
                    { 24, 2, "4C" },
                    { 25, 2, "8C Competizione" },
                    { 26, 2, "Brera" },
                    { 27, 2, "Giulia" },
                    { 28, 2, "Giulietta" },
                    { 29, 2, "GT" },
                    { 30, 2, "GTV" },
                    { 31, 2, "MiTo" },
                    { 32, 2, "Spider" },
                    { 33, 2, "Stelvio" },
                    { 34, 2, "Tonale" },
                    { 35, 3, "Cygnet" },
                    { 36, 3, "DB11" },
                    { 37, 3, "DB9" },
                    { 38, 3, "DBS" },
                    { 39, 3, "DBS Superleggera" },
                    { 40, 3, "DBS Violante" },
                    { 41, 3, "DBX" },
                    { 42, 3, "Rapide" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 43, 3, "V12 Vanquish" },
                    { 44, 3, "V12 Vantage" },
                    { 45, 3, "V8 Vantage" },
                    { 46, 3, "Valkyrie" },
                    { 47, 3, "Vanquish" },
                    { 48, 3, "Vantage" },
                    { 49, 3, "Virage" },
                    { 50, 3, "Zagato Coupe" },
                    { 51, 4, "A1" },
                    { 52, 4, "A2" },
                    { 53, 4, "A3" },
                    { 54, 4, "A4" },
                    { 55, 4, "A4 Allroad Quattro" },
                    { 56, 4, "A5" },
                    { 57, 4, "A6" },
                    { 58, 4, "A7" },
                    { 59, 4, "A8" },
                    { 60, 4, "Allroad" },
                    { 61, 4, "e-tron" },
                    { 62, 4, "e-tron GT" },
                    { 63, 4, "e-tron Sportback" },
                    { 64, 4, "Q2" },
                    { 65, 4, "Q3" },
                    { 66, 4, "Q3 Sportback" },
                    { 67, 4, "Q4" },
                    { 68, 4, "Q4 Sportback" },
                    { 69, 4, "Q5" },
                    { 70, 4, "Q5 Sportback" },
                    { 71, 4, "Q7" },
                    { 72, 4, "Q8" },
                    { 73, 4, "R8" },
                    { 74, 4, "RS e-tron GT" },
                    { 75, 4, "RS Q3" },
                    { 76, 4, "RS Q3 Sportback" },
                    { 77, 4, "RS Q7" },
                    { 78, 4, "RS Q8" },
                    { 79, 4, "RS3" },
                    { 80, 4, "RS4" },
                    { 81, 4, "RS5" },
                    { 82, 4, "RS6" },
                    { 83, 4, "RS7" },
                    { 84, 4, "S1" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 85, 4, "S3" },
                    { 86, 4, "S4" },
                    { 87, 4, "S5" },
                    { 88, 4, "S6" },
                    { 89, 4, "S7" },
                    { 90, 4, "S8" },
                    { 91, 4, "SQ2" },
                    { 92, 4, "SQ5" },
                    { 93, 4, "SQ5 Sportback" },
                    { 94, 4, "SQ7" },
                    { 95, 4, "SQ8" },
                    { 96, 4, "TT" },
                    { 97, 4, "TT RS" },
                    { 98, 4, "TTS" },
                    { 99, 5, "Arnage" },
                    { 100, 5, "Azure" },
                    { 101, 5, "Bentayga" },
                    { 102, 5, "Brooklands" },
                    { 103, 5, "Continental" },
                    { 104, 5, "Continental Flying Spur" },
                    { 105, 5, "Continental GT" },
                    { 106, 5, "Flying Spur" },
                    { 107, 5, "Mulsanne" },
                    { 108, 6, "1 series" },
                    { 109, 6, "2 series" },
                    { 110, 6, "3 series" },
                    { 111, 6, "4 series" },
                    { 112, 6, "5 series" },
                    { 113, 6, "6 series" },
                    { 114, 6, "7 series" },
                    { 115, 6, "8 series" },
                    { 116, 6, "i3" },
                    { 117, 6, "i4" },
                    { 118, 6, "i8" },
                    { 119, 6, "iX" },
                    { 120, 6, "iX3" },
                    { 121, 6, "M2" },
                    { 122, 6, "M3" },
                    { 123, 6, "M4" },
                    { 124, 6, "M5" },
                    { 125, 6, "M6" },
                    { 126, 6, "M8" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 127, 6, "X1" },
                    { 128, 6, "X2" },
                    { 129, 6, "X3" },
                    { 130, 6, "X3 M" },
                    { 131, 6, "X4" },
                    { 132, 6, "X4 M" },
                    { 133, 6, "X5" },
                    { 134, 6, "X5 M" },
                    { 135, 6, "X6" },
                    { 136, 6, "X6 M" },
                    { 137, 6, "X7" },
                    { 138, 6, "Z3" },
                    { 139, 6, "Z4" },
                    { 140, 6, "Z8" },
                    { 141, 7, "H230" },
                    { 142, 7, "V3" },
                    { 143, 7, "V5" },
                    { 144, 8, "Chiron" },
                    { 145, 8, "Divo" },
                    { 146, 8, "Veyron" },
                    { 147, 9, "Century" },
                    { 148, 9, "Enclave" },
                    { 149, 9, "Encore" },
                    { 150, 9, "Envision" },
                    { 151, 9, "GL8 ES" },
                    { 152, 9, "La Crosse" },
                    { 153, 9, "LaCrosse" },
                    { 154, 9, "Le Sabre" },
                    { 155, 9, "Lucerne" },
                    { 156, 9, "Park Avenue" },
                    { 157, 9, "Rainier" },
                    { 158, 9, "Regal" },
                    { 159, 9, "Rendezvouz" },
                    { 160, 9, "Terraza" },
                    { 161, 9, "Verano" },
                    { 162, 10, "Qin" },
                    { 163, 11, "ATS" },
                    { 164, 11, "ATS-V" },
                    { 165, 11, "BLS" },
                    { 166, 11, "CT4" },
                    { 167, 11, "CT4-V" },
                    { 168, 11, "CT5" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 169, 11, "CT5-V" },
                    { 170, 11, "CT6" },
                    { 171, 11, "CTS" },
                    { 172, 11, "De Ville" },
                    { 173, 11, "DTS" },
                    { 174, 11, "Eldorado" },
                    { 175, 11, "ELR" },
                    { 176, 11, "Escalade" },
                    { 177, 11, "Seville" },
                    { 178, 11, "SRX" },
                    { 179, 11, "STS" },
                    { 180, 11, "XLR" },
                    { 181, 11, "XT4" },
                    { 182, 11, "XT5" },
                    { 183, 11, "XT6" },
                    { 184, 11, "XTS" },
                    { 185, 12, "CS35" },
                    { 186, 12, "CS35 Plus" },
                    { 187, 12, "CS55" },
                    { 188, 12, "CS55 Plus" },
                    { 189, 12, "CS75" },
                    { 190, 12, "CS75 Plus" },
                    { 191, 12, "CS95" },
                    { 192, 12, "Eado" },
                    { 193, 12, "Raeton" },
                    { 194, 12, "Raeton CC" },
                    { 195, 12, "Uni-K" },
                    { 196, 12, "Uni-T" },
                    { 197, 12, "Uni-V" },
                    { 198, 13, "Amulet" },
                    { 199, 13, "Arrizo 5 Plus" },
                    { 200, 13, "Arrizo 6" },
                    { 201, 13, "Arrizo 6 Pro" },
                    { 202, 13, "Arrizo 7" },
                    { 203, 13, "Arrizo 8" },
                    { 204, 13, "Bonus" },
                    { 205, 13, "Bonus 3" },
                    { 206, 13, "CrossEastar" },
                    { 207, 13, "Eastar" },
                    { 208, 13, "eQ" },
                    { 209, 13, "eQ1" },
                    { 210, 13, "eQ5" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 211, 13, "Fora" },
                    { 212, 13, "IndiS" },
                    { 213, 13, "Kimo" },
                    { 214, 13, "M11" },
                    { 215, 13, "Omoda 5" },
                    { 216, 13, "QQ" },
                    { 217, 13, "QQ3" },
                    { 218, 13, "QQ6" },
                    { 219, 13, "Tiggo" },
                    { 220, 13, "Tiggo 3" },
                    { 221, 13, "Tiggo 4" },
                    { 222, 13, "Tiggo 4 Pro" },
                    { 223, 13, "Tiggo 5" },
                    { 224, 13, "Tiggo 7" },
                    { 225, 13, "Tiggo 7 Pro" },
                    { 226, 13, "Tiggo 8" },
                    { 227, 13, "Tiggo 8 Plus" },
                    { 228, 13, "Tiggo 8 Pro" },
                    { 229, 13, "Tiggo 8 Pro Max" },
                    { 230, 13, "Tiggo e" },
                    { 231, 13, "Very" },
                    { 232, 14, "Astro" },
                    { 233, 14, "Avalanche" },
                    { 234, 14, "Aveo" },
                    { 235, 14, "Beat" },
                    { 236, 14, "Blazer" },
                    { 237, 14, "Bolt" },
                    { 238, 14, "Bolt EUV" },
                    { 239, 14, "Camaro" },
                    { 240, 14, "Captiva" },
                    { 241, 14, "Cavalier" },
                    { 242, 14, "Cobalt" },
                    { 243, 14, "Colorado" },
                    { 244, 14, "Corvette" },
                    { 245, 14, "Cruze" },
                    { 246, 14, "Epica" },
                    { 247, 14, "Equinox" },
                    { 248, 14, "Express" },
                    { 249, 14, "HHR" },
                    { 250, 14, "Impala" },
                    { 251, 14, "Lacetti" },
                    { 252, 14, "Lanos" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 253, 14, "Malibu" },
                    { 254, 14, "Monte Carlo" },
                    { 255, 14, "Niva" },
                    { 256, 14, "Orlando" },
                    { 257, 14, "Rezzo" },
                    { 258, 14, "Silverado" },
                    { 259, 14, "Silverado 1500" },
                    { 260, 14, "Silverado 2500 HD" },
                    { 261, 14, "Spark" },
                    { 262, 14, "SSR" },
                    { 263, 14, "Suburban" },
                    { 264, 14, "Tahoe" },
                    { 265, 14, "TrailBlazer" },
                    { 266, 14, "Traverse" },
                    { 267, 14, "Trax" },
                    { 268, 14, "Uplander" },
                    { 269, 14, "Venture" },
                    { 270, 15, "200" },
                    { 271, 15, "300" },
                    { 272, 15, "300M" },
                    { 273, 15, "Aspen" },
                    { 274, 15, "Concorde" },
                    { 275, 15, "Crossfire" },
                    { 276, 15, "Grand Caravan" },
                    { 277, 15, "Grand Voyager" },
                    { 278, 15, "Pacifica" },
                    { 279, 15, "PT Cruiser" },
                    { 280, 15, "Sebring" },
                    { 281, 15, "Town & Country" },
                    { 282, 15, "Voyager" },
                    { 283, 16, "Berlingo" },
                    { 284, 16, "C-Crosser" },
                    { 285, 16, "C-Elysee" },
                    { 286, 16, "C1" },
                    { 287, 16, "C2" },
                    { 288, 16, "C3" },
                    { 289, 16, "C3 Aircross" },
                    { 290, 16, "C3 Picasso" },
                    { 291, 16, "C3 Pluriel" },
                    { 292, 16, "C4" },
                    { 293, 16, "C4 Aircross" },
                    { 294, 16, "C4 Cactus" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 295, 16, "C4 Picasso" },
                    { 296, 16, "C4 SpaceTourer" },
                    { 297, 16, "C5" },
                    { 298, 16, "C5 Aircross" },
                    { 299, 16, "C6" },
                    { 300, 16, "C8" },
                    { 301, 16, "DS 7 Crossback" },
                    { 302, 16, "DS3" },
                    { 303, 16, "DS4" },
                    { 304, 16, "DS5" },
                    { 305, 16, "Grand C4 Picasso" },
                    { 306, 16, "Grand C4 SpaceTourer" },
                    { 307, 16, "Jumper" },
                    { 308, 16, "Jumpy" },
                    { 309, 16, "Nemo" },
                    { 310, 16, "Saxo" },
                    { 311, 16, "Spacetourer" },
                    { 312, 16, "Xsara" },
                    { 313, 16, "Xsara Picasso" },
                    { 314, 17, "Dokker" },
                    { 315, 17, "Lodgy" },
                    { 316, 17, "Solenza" },
                    { 317, 17, "Spring" },
                    { 318, 17, "SupeRNova" },
                    { 319, 18, "Evanda" },
                    { 320, 18, "Kalos" },
                    { 321, 18, "Leganza" },
                    { 322, 18, "Magnus" },
                    { 323, 18, "Matiz" },
                    { 324, 18, "Nexia" },
                    { 325, 18, "Nubira" },
                    { 326, 19, "Applause" },
                    { 327, 19, "Cast" },
                    { 328, 19, "Copen" },
                    { 329, 19, "Cuore" },
                    { 330, 19, "Gran Move" },
                    { 331, 19, "Luxio" },
                    { 332, 19, "Materia" },
                    { 333, 19, "Mebius" },
                    { 334, 19, "Move" },
                    { 335, 19, "Rocky" },
                    { 336, 19, "Sirion" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 337, 19, "Terios" },
                    { 338, 19, "Trevis" },
                    { 339, 19, "YRV" },
                    { 340, 20, "mi-DO" },
                    { 341, 20, "on-DO" },
                    { 342, 21, "Avenger" },
                    { 343, 21, "Caliber" },
                    { 344, 21, "Caliber SRT4" },
                    { 345, 21, "Caravan" },
                    { 346, 21, "Challenger" },
                    { 347, 21, "Charger" },
                    { 348, 21, "Dakota" },
                    { 349, 21, "Dart" },
                    { 350, 21, "Durango" },
                    { 351, 21, "Intrepid" },
                    { 352, 21, "Journey" },
                    { 353, 21, "Magnum" },
                    { 354, 21, "Neon" },
                    { 355, 21, "Nitro" },
                    { 356, 21, "Ram 1500" },
                    { 357, 21, "Ram 2500" },
                    { 358, 21, "Ram 3500" },
                    { 359, 21, "Ram SRT10" },
                    { 360, 21, "Stratus" },
                    { 361, 21, "Viper" },
                    { 362, 22, "580" },
                    { 363, 22, "A30" },
                    { 364, 22, "AX7" },
                    { 365, 22, "H30 Cross" },
                    { 366, 23, "TXL" },
                    { 367, 23, "VX" },
                    { 368, 24, "Bestune T77" },
                    { 369, 24, "Besturn B30" },
                    { 370, 24, "Besturn B50" },
                    { 371, 24, "Besturn B70" },
                    { 372, 24, "Besturn X40" },
                    { 373, 24, "Besturn X80" },
                    { 374, 24, "Oley" },
                    { 375, 24, "Vita" },
                    { 376, 25, "296" },
                    { 377, 25, "348" },
                    { 378, 25, "360" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 379, 25, "456" },
                    { 380, 25, "458" },
                    { 381, 25, "488" },
                    { 382, 25, "512" },
                    { 383, 25, "550" },
                    { 384, 25, "575 M" },
                    { 385, 25, "599 GTB Fiorano" },
                    { 386, 25, "599 GTO" },
                    { 387, 25, "612" },
                    { 388, 25, "812" },
                    { 389, 25, "California" },
                    { 390, 25, "California T" },
                    { 391, 25, "Challenge Stradale" },
                    { 392, 25, "Enzo" },
                    { 393, 25, "F12" },
                    { 394, 25, "F355" },
                    { 395, 25, "F430" },
                    { 396, 25, "F50" },
                    { 397, 25, "F512 M" },
                    { 398, 25, "F8 Spider" },
                    { 399, 25, "F8 Tributo" },
                    { 400, 25, "FF" },
                    { 401, 25, "GTC4 Lusso" },
                    { 402, 25, "LaFerrari" },
                    { 403, 25, "Portofino" },
                    { 404, 25, "Portofino M" },
                    { 405, 25, "Roma" },
                    { 406, 25, "SF90 Spider" },
                    { 407, 25, "SF90 Stradale" },
                    { 408, 26, "124 Spider" },
                    { 409, 26, "500" },
                    { 410, 26, "500L" },
                    { 411, 26, "500X" },
                    { 412, 26, "Albea" },
                    { 413, 26, "Brava" },
                    { 414, 26, "Bravo" },
                    { 415, 26, "Coupe" },
                    { 416, 26, "Croma" },
                    { 417, 26, "Doblo" },
                    { 418, 26, "Ducato" },
                    { 419, 26, "Freemont" },
                    { 420, 26, "Grande Punto" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 421, 26, "Idea" },
                    { 422, 26, "Linea" },
                    { 423, 26, "Marea" },
                    { 424, 26, "Multipla" },
                    { 425, 26, "Palio" },
                    { 426, 26, "Panda" },
                    { 427, 26, "Panda 4x4" },
                    { 428, 26, "Punto" },
                    { 429, 26, "Qubo" },
                    { 430, 26, "Sedici" },
                    { 431, 26, "Siena" },
                    { 432, 26, "Stilo" },
                    { 433, 26, "Strada" },
                    { 434, 26, "Tipo" },
                    { 435, 26, "Ulysse" },
                    { 436, 27, "Karma" },
                    { 437, 28, "B-Max" },
                    { 438, 28, "Bronco" },
                    { 439, 28, "Bronco Sport" },
                    { 440, 28, "C-Max" },
                    { 441, 28, "Cougar" },
                    { 442, 28, "Crown Victoria" },
                    { 443, 28, "EcoSport" },
                    { 444, 28, "Edge" },
                    { 445, 28, "Endura" },
                    { 446, 28, "Equator" },
                    { 447, 28, "Escape" },
                    { 448, 28, "Excursion" },
                    { 449, 28, "Expedition" },
                    { 450, 28, "Explorer" },
                    { 451, 28, "Explorer Sport Trac" },
                    { 452, 28, "F-150" },
                    { 453, 28, "F-250" },
                    { 454, 28, "F-350" },
                    { 455, 28, "Falcon" },
                    { 456, 28, "Fiesta" },
                    { 457, 28, "Five Hundred" },
                    { 458, 28, "Flex" },
                    { 459, 28, "Focus" },
                    { 460, 28, "Focus Active" },
                    { 461, 28, "Focus Electric" },
                    { 462, 28, "Freestar" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 463, 28, "Freestyle" },
                    { 464, 28, "Fusion" },
                    { 465, 28, "Galaxy" },
                    { 466, 28, "Ka" },
                    { 467, 28, "Kuga" },
                    { 468, 28, "Maverick" },
                    { 469, 28, "Mondeo" },
                    { 470, 28, "Mustang" },
                    { 471, 28, "Mustang Mach-E" },
                    { 472, 28, "Mustang Shelby GT350" },
                    { 473, 28, "Mustang Shelby GT500" },
                    { 474, 28, "Puma" },
                    { 475, 28, "Ranger" },
                    { 476, 28, "S-Max" },
                    { 477, 28, "Taurus" },
                    { 478, 28, "Taurus X" },
                    { 479, 28, "Thunderbird" },
                    { 480, 28, "Tourneo Connect" },
                    { 481, 28, "Transit" },
                    { 482, 28, "Transit Connect" },
                    { 483, 29, "Sauvana" },
                    { 484, 30, "GS5" },
                    { 485, 30, "Trumpchi GM8" },
                    { 486, 30, "Trumpchi GS8" },
                    { 487, 31, "3102" },
                    { 488, 31, "31105" },
                    { 489, 31, "Gazelle" },
                    { 490, 31, "Gazelle Business" },
                    { 491, 31, "Gazelle Next" },
                    { 492, 31, "Gazelle NN" },
                    { 493, 31, "Gazelle Sity" },
                    { 494, 31, "Siber" },
                    { 495, 31, "Sobol" },
                    { 496, 32, "Atlas" },
                    { 497, 32, "Atlas Pro" },
                    { 498, 32, "Azkarra" },
                    { 499, 32, "Coolray" },
                    { 500, 32, "Emgrand 7" },
                    { 501, 32, "Emgrand EC7" },
                    { 502, 32, "Emgrand GS" },
                    { 503, 32, "Emgrand X7" },
                    { 504, 32, "GC9" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 505, 32, "GС6" },
                    { 506, 32, "MK" },
                    { 507, 32, "Monjaro" },
                    { 508, 32, "Otaka" },
                    { 509, 32, "Preface" },
                    { 510, 32, "Tugella" },
                    { 511, 32, "Vision" },
                    { 512, 33, "G70" },
                    { 513, 33, "G80" },
                    { 514, 33, "G90" },
                    { 515, 33, "GV60" },
                    { 516, 33, "GV70" },
                    { 517, 33, "GV80" },
                    { 518, 34, "Acadia" },
                    { 519, 34, "Canyon" },
                    { 520, 34, "Envoy" },
                    { 521, 34, "Sierra 1500" },
                    { 522, 34, "Sierra 2500" },
                    { 523, 34, "Sierra 3500" },
                    { 524, 34, "Terrain" },
                    { 525, 34, "Yukon" },
                    { 526, 35, "Cowry" },
                    { 527, 35, "Deer" },
                    { 528, 35, "Hover" },
                    { 529, 35, "Hover M2" },
                    { 530, 35, "Pegasus" },
                    { 531, 35, "Peri" },
                    { 532, 35, "Poer" },
                    { 533, 35, "Safe" },
                    { 534, 35, "Sailor" },
                    { 535, 35, "Sing" },
                    { 536, 35, "Socool" },
                    { 537, 35, "Wingle" },
                    { 538, 35, "Wingle 7" },
                    { 539, 36, "Dargo" },
                    { 540, 36, "F7" },
                    { 541, 36, "F7x" },
                    { 542, 36, "H4" },
                    { 543, 36, "H6" },
                    { 544, 36, "H9" },
                    { 545, 36, "Jolion" },
                    { 546, 37, "Commodore" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 547, 37, "Corvette C8" },
                    { 548, 38, "Accord" },
                    { 549, 38, "Amaze" },
                    { 550, 38, "City" },
                    { 551, 38, "Civic" },
                    { 552, 38, "Civic Type R" },
                    { 553, 38, "CR-V" },
                    { 554, 38, "CR-Z" },
                    { 555, 38, "Crosstour" },
                    { 556, 38, "e" },
                    { 557, 38, "Element" },
                    { 558, 38, "Fit" },
                    { 559, 38, "FR-V" },
                    { 560, 38, "HR-V" },
                    { 561, 38, "Insight" },
                    { 562, 38, "Jade" },
                    { 563, 38, "Jazz" },
                    { 564, 38, "Legend" },
                    { 565, 38, "Odyssey" },
                    { 566, 38, "Passport" },
                    { 567, 38, "Pilot" },
                    { 568, 38, "Prelude" },
                    { 569, 38, "Ridgeline" },
                    { 570, 38, "S2000" },
                    { 571, 38, "Shuttle" },
                    { 572, 38, "Stepwgn" },
                    { 573, 38, "Stream" },
                    { 574, 38, "Vezel" },
                    { 575, 39, "H1" },
                    { 576, 39, "H2" },
                    { 577, 39, "H3" },
                    { 578, 40, "Accent" },
                    { 579, 40, "Atos Prime" },
                    { 580, 40, "Azera" },
                    { 581, 40, "Bayon" },
                    { 582, 40, "Centennial" },
                    { 583, 40, "Creta" },
                    { 584, 40, "Creta Grand" },
                    { 585, 40, "Elantra" },
                    { 586, 40, "Entourage" },
                    { 587, 40, "Eon" },
                    { 588, 40, "Equus" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 589, 40, "Galloper" },
                    { 590, 40, "Genesis" },
                    { 591, 40, "Genesis Coupe" },
                    { 592, 40, "Getz" },
                    { 593, 40, "Grandeur" },
                    { 594, 40, "H-1" },
                    { 595, 40, "i10" },
                    { 596, 40, "i20" },
                    { 597, 40, "i20 N" },
                    { 598, 40, "i30" },
                    { 599, 40, "i30 N" },
                    { 600, 40, "i40" },
                    { 601, 40, "Ioniq" },
                    { 602, 40, "Ioniq 5" },
                    { 603, 40, "ix20" },
                    { 604, 40, "ix35" },
                    { 605, 40, "Kona" },
                    { 606, 40, "Kona N" },
                    { 607, 40, "Kusto" },
                    { 608, 40, "Matrix" },
                    { 609, 40, "Mistra" },
                    { 610, 40, "Nexo" },
                    { 611, 40, "Palisade" },
                    { 612, 40, "Porter" },
                    { 613, 40, "Santa Cruz" },
                    { 614, 40, "Santa Fe" },
                    { 615, 40, "Solaris" },
                    { 616, 40, "Sonata" },
                    { 617, 40, "Staria" },
                    { 618, 40, "Terracan" },
                    { 619, 40, "Trajet" },
                    { 620, 40, "Tucson" },
                    { 621, 40, "Veloster" },
                    { 622, 40, "Venue" },
                    { 623, 40, "Veracruz" },
                    { 624, 40, "Verna" },
                    { 625, 40, "Xcent" },
                    { 626, 40, "XG" },
                    { 627, 41, "EX" },
                    { 628, 41, "FX" },
                    { 629, 41, "G" },
                    { 630, 41, "I35" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 631, 41, "JX" },
                    { 632, 41, "M" },
                    { 633, 41, "Q30" },
                    { 634, 41, "Q40" },
                    { 635, 41, "Q45" },
                    { 636, 41, "Q50" },
                    { 637, 41, "Q60" },
                    { 638, 41, "Q70" },
                    { 639, 41, "QX30" },
                    { 640, 41, "QX4" },
                    { 641, 41, "QX50" },
                    { 642, 41, "QX55" },
                    { 643, 41, "QX56" },
                    { 644, 41, "QX60" },
                    { 645, 41, "QX70" },
                    { 646, 41, "QX80" },
                    { 647, 42, "Ascender" },
                    { 648, 42, "Axiom" },
                    { 649, 42, "D-Max" },
                    { 650, 42, "D-Max Rodeo" },
                    { 651, 42, "I280" },
                    { 652, 42, "I290" },
                    { 653, 42, "I350" },
                    { 654, 42, "I370" },
                    { 655, 42, "mu-X" },
                    { 656, 42, "Rodeo" },
                    { 657, 42, "Trooper" },
                    { 658, 42, "VehiCross" },
                    { 659, 43, "Daily" },
                    { 660, 44, "iEV7S" },
                    { 661, 44, "T6" },
                    { 662, 45, "E-Pace" },
                    { 663, 45, "F-Pace" },
                    { 664, 45, "F-Type" },
                    { 665, 45, "I-Pace" },
                    { 666, 45, "S-Type" },
                    { 667, 45, "X-Type" },
                    { 668, 45, "XE" },
                    { 669, 45, "XF" },
                    { 670, 45, "XJ" },
                    { 671, 45, "XK/XKR" },
                    { 672, 46, "Cherokee" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 673, 46, "Commander" },
                    { 674, 46, "Compass" },
                    { 675, 46, "Gladiator" },
                    { 676, 46, "Grand Cherokee" },
                    { 677, 46, "Grand Wagoneer" },
                    { 678, 46, "Liberty" },
                    { 679, 46, "Meridian" },
                    { 680, 46, "Patriot" },
                    { 681, 46, "Renegade" },
                    { 682, 46, "Wagoneer" },
                    { 683, 46, "Wrangler" },
                    { 684, 47, "Carens" },
                    { 685, 47, "Carnival" },
                    { 686, 47, "Ceed" },
                    { 687, 47, "Cerato" },
                    { 688, 47, "Clarus" },
                    { 689, 47, "EV6" },
                    { 690, 47, "Forte" },
                    { 691, 47, "K5" },
                    { 692, 47, "K8" },
                    { 693, 47, "K900" },
                    { 694, 47, "Magentis" },
                    { 695, 47, "Mohave" },
                    { 696, 47, "Niro" },
                    { 697, 47, "Opirus" },
                    { 698, 47, "Optima" },
                    { 699, 47, "Picanto" },
                    { 700, 47, "ProCeed" },
                    { 701, 47, "Quoris" },
                    { 702, 47, "Ray" },
                    { 703, 47, "Rio" },
                    { 704, 47, "Rio X" },
                    { 705, 47, "Rio X-Line" },
                    { 706, 47, "Seltos" },
                    { 707, 47, "Shuma" },
                    { 708, 47, "Sonet" },
                    { 709, 47, "Sorento" },
                    { 710, 47, "Sorento Prime" },
                    { 711, 47, "Soul" },
                    { 712, 47, "Spectra" },
                    { 713, 47, "Sportage" },
                    { 714, 47, "Stinger" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 715, 47, "Stonic" },
                    { 716, 47, "Telluride" },
                    { 717, 47, "Venga" },
                    { 718, 48, "Aventador" },
                    { 719, 48, "Centenario" },
                    { 720, 48, "Diablo" },
                    { 721, 48, "Gallardo" },
                    { 722, 48, "Huracan" },
                    { 723, 48, "Murcielago" },
                    { 724, 48, "Reventon" },
                    { 725, 48, "Sian" },
                    { 726, 48, "Urus" },
                    { 727, 49, "Delta" },
                    { 728, 49, "Lybra" },
                    { 729, 49, "Musa" },
                    { 730, 49, "Phedra" },
                    { 731, 49, "Thema" },
                    { 732, 49, "Thesis" },
                    { 733, 49, "Ypsilon" },
                    { 734, 50, "Defender" },
                    { 735, 50, "Discovery" },
                    { 736, 50, "Discovery Sport" },
                    { 737, 50, "Evoque" },
                    { 738, 50, "Freelander" },
                    { 739, 50, "Range Rover" },
                    { 740, 50, "Range Rover Sport" },
                    { 741, 50, "Range Rover Velar" },
                    { 742, 51, "CT" },
                    { 743, 51, "ES" },
                    { 744, 51, "GS" },
                    { 745, 51, "GX" },
                    { 746, 51, "HS" },
                    { 747, 51, "IS" },
                    { 748, 51, "LC" },
                    { 749, 51, "LFA" },
                    { 750, 51, "LM" },
                    { 751, 51, "LS" },
                    { 752, 51, "LX" },
                    { 753, 51, "NX" },
                    { 754, 51, "RC" },
                    { 755, 51, "RC F" },
                    { 756, 51, "RX" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 757, 51, "SC" },
                    { 758, 51, "UX" },
                    { 759, 52, "Breez" },
                    { 760, 52, "Cebrium" },
                    { 761, 52, "Celliya" },
                    { 762, 52, "Murman" },
                    { 763, 52, "Myway" },
                    { 764, 52, "Smily" },
                    { 765, 52, "Solano" },
                    { 766, 52, "X50" },
                    { 767, 52, "X60" },
                    { 768, 52, "X70" },
                    { 769, 52, "X80" },
                    { 770, 53, "Aviator" },
                    { 771, 53, "Corsair" },
                    { 772, 53, "Mark LT" },
                    { 773, 53, "MKC" },
                    { 774, 53, "MKS" },
                    { 775, 53, "MKT" },
                    { 776, 53, "MKX" },
                    { 777, 53, "MKZ" },
                    { 778, 53, "Nautilus" },
                    { 779, 53, "Navigator" },
                    { 780, 53, "Town Car" },
                    { 781, 53, "Zephyr" },
                    { 782, 54, "Elise" },
                    { 783, 54, "Europa S" },
                    { 784, 54, "Evora" },
                    { 785, 54, "Exige" },
                    { 786, 55, "B1" },
                    { 787, 55, "B2" },
                    { 788, 56, "3200 GT" },
                    { 789, 56, "Ghibli" },
                    { 790, 56, "Gran Cabrio" },
                    { 791, 56, "Gran Turismo " },
                    { 792, 56, "Gran Turismo S" },
                    { 793, 56, "Grecale" },
                    { 794, 56, "Levante" },
                    { 795, 56, "MC20" },
                    { 796, 56, "Quattroporte" },
                    { 797, 56, "Quattroporte S" },
                    { 798, 57, "57" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 799, 57, "57 S" },
                    { 800, 57, "62" },
                    { 801, 57, "62 S" },
                    { 802, 57, "Landaulet" },
                    { 803, 58, "2" },
                    { 804, 58, "3" },
                    { 805, 58, "323" },
                    { 806, 58, "5" },
                    { 807, 58, "6" },
                    { 808, 58, "626" },
                    { 809, 58, "B-Series" },
                    { 810, 58, "BT-50" },
                    { 811, 58, "CX-3" },
                    { 812, 58, "CX-30" },
                    { 813, 58, "CX-30 EV" },
                    { 814, 58, "CX-4" },
                    { 815, 58, "CX-5" },
                    { 816, 58, "CX-60" },
                    { 817, 58, "CX-7" },
                    { 818, 58, "CX-8" },
                    { 819, 58, "CX-9" },
                    { 820, 58, "MPV" },
                    { 821, 58, "MX-30" },
                    { 822, 58, "MX-5" },
                    { 823, 58, "Premacy" },
                    { 824, 58, "RX-7" },
                    { 825, 58, "RX-8" },
                    { 826, 58, "Tribute" },
                    { 827, 59, "540C" },
                    { 828, 59, "570S" },
                    { 829, 59, "600LT" },
                    { 830, 59, "650S" },
                    { 831, 59, "675LT" },
                    { 832, 59, "720S" },
                    { 833, 59, "720S Spider" },
                    { 834, 59, "765LT" },
                    { 835, 59, "Artura" },
                    { 836, 59, "MP4-12C" },
                    { 837, 59, "P1" },
                    { 838, 60, "A-class" },
                    { 839, 60, "AMG GT" },
                    { 840, 60, "AMG GT 4-Door" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 841, 60, "B-class" },
                    { 842, 60, "C-class" },
                    { 843, 60, "C-class Sport Coupe" },
                    { 844, 60, "Citan" },
                    { 845, 60, "CL-class" },
                    { 846, 60, "CLA-class" },
                    { 847, 60, "CLC-class " },
                    { 848, 60, "CLK-class" },
                    { 849, 60, "CLS-class" },
                    { 850, 60, "E-class" },
                    { 851, 60, "E-class Coupe" },
                    { 852, 60, "EQA" },
                    { 853, 60, "EQB" },
                    { 854, 60, "EQC" },
                    { 855, 60, "EQE" },
                    { 856, 60, "EQE AMG" },
                    { 857, 60, "EQS" },
                    { 858, 60, "EQS AMG" },
                    { 859, 60, "EQV" },
                    { 860, 60, "G-class" },
                    { 861, 60, "GL-class" },
                    { 862, 60, "GLA-class" },
                    { 863, 60, "GLA-class AMG" },
                    { 864, 60, "GLB-class" },
                    { 865, 60, "GLC-class" },
                    { 866, 60, "GLC-class AMG" },
                    { 867, 60, "GLC-class Coupe" },
                    { 868, 60, "GLE-class" },
                    { 869, 60, "GLE-class AMG" },
                    { 870, 60, "GLE-class Coupe" },
                    { 871, 60, "GLK-class" },
                    { 872, 60, "GLS-class" },
                    { 873, 60, "GLS-class AMG" },
                    { 874, 60, "M-class" },
                    { 875, 60, "R-class" },
                    { 876, 60, "S-class" },
                    { 877, 60, "S-class Cabrio" },
                    { 878, 60, "S-class Coupe" },
                    { 879, 60, "SL-class" },
                    { 880, 60, "SL-Class AMG" },
                    { 881, 60, "SLC-class" },
                    { 882, 60, "SLK-class" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 883, 60, "SLR-class" },
                    { 884, 60, "SLS AMG" },
                    { 885, 60, "Sprinter" },
                    { 886, 60, "Vaneo" },
                    { 887, 60, "Viano" },
                    { 888, 60, "Vito" },
                    { 889, 60, "X-class" },
                    { 890, 61, "Grand Marquis" },
                    { 891, 61, "Mariner" },
                    { 892, 61, "Milan" },
                    { 893, 61, "Montego" },
                    { 894, 61, "Monterey" },
                    { 895, 61, "Mountaineer" },
                    { 896, 61, "Sable" },
                    { 897, 62, "Hector" },
                    { 898, 62, "TF" },
                    { 899, 62, "XPower SV" },
                    { 900, 62, "ZR" },
                    { 901, 62, "ZS" },
                    { 902, 62, "ZS EV" },
                    { 903, 62, "ZT" },
                    { 904, 62, "ZT-T" },
                    { 905, 63, "Clubman" },
                    { 906, 63, "Clubman S" },
                    { 907, 63, "Clubvan" },
                    { 908, 63, "Cooper" },
                    { 909, 63, "Cooper Cabrio" },
                    { 910, 63, "Cooper S" },
                    { 911, 63, "Cooper S Cabrio" },
                    { 912, 63, "Cooper S Countryman All4" },
                    { 913, 63, "Countryman" },
                    { 914, 63, "One" },
                    { 915, 64, "3000 GT" },
                    { 916, 64, "ASX" },
                    { 917, 64, "Carisma" },
                    { 918, 64, "Colt" },
                    { 919, 64, "Dignity" },
                    { 920, 64, "Eclipse" },
                    { 921, 64, "Eclipse Cross" },
                    { 922, 64, "Endeavor" },
                    { 923, 64, "Galant" },
                    { 924, 64, "Grandis" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 925, 64, "i-MiEV" },
                    { 926, 64, "L200" },
                    { 927, 64, "Lancer" },
                    { 928, 64, "Lancer Evo" },
                    { 929, 64, "Mirage" },
                    { 930, 64, "Outlander" },
                    { 931, 64, "Outlander Sport" },
                    { 932, 64, "Outlander XL" },
                    { 933, 64, "Pajero" },
                    { 934, 64, "Pajero Pinin" },
                    { 935, 64, "Pajero Sport" },
                    { 936, 64, "Raider" },
                    { 937, 64, "Space Gear" },
                    { 938, 64, "Space Runner" },
                    { 939, 64, "Space Star" },
                    { 940, 64, "Xpander" },
                    { 941, 65, "350Z" },
                    { 942, 65, "370Z" },
                    { 943, 65, "Almera" },
                    { 944, 65, "Almera Classic" },
                    { 945, 65, "Almera Tino" },
                    { 946, 65, "Altima" },
                    { 947, 65, "Ariya" },
                    { 948, 65, "Armada" },
                    { 949, 65, "Bluebird Sylphy" },
                    { 950, 65, "Frontier" },
                    { 951, 65, "GT-R" },
                    { 952, 65, "Juke" },
                    { 953, 65, "Leaf" },
                    { 954, 65, "Maxima" },
                    { 955, 65, "Micra" },
                    { 956, 65, "Murano" },
                    { 957, 65, "Navara" },
                    { 958, 65, "Note" },
                    { 959, 65, "NP300" },
                    { 960, 65, "Pathfinder" },
                    { 961, 65, "Patrol" },
                    { 962, 65, "Primera" },
                    { 963, 65, "Qashqai" },
                    { 964, 65, "Qashqai+2" },
                    { 965, 65, "Quest" },
                    { 966, 65, "Rogue" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 967, 65, "Sentra" },
                    { 968, 65, "Skyline" },
                    { 969, 65, "Sylphy" },
                    { 970, 65, "Teana" },
                    { 971, 65, "Terrano" },
                    { 972, 65, "Tiida" },
                    { 973, 65, "Titan" },
                    { 974, 65, "Titan XD" },
                    { 975, 65, "X-Trail" },
                    { 976, 65, "XTerra" },
                    { 977, 65, "Z" },
                    { 978, 66, "Adam" },
                    { 979, 66, "Agila" },
                    { 980, 66, "Ampera-e" },
                    { 981, 66, "Antara" },
                    { 982, 66, "Astra" },
                    { 983, 66, "Astra GTC" },
                    { 984, 66, "Astra OPC" },
                    { 985, 66, "Cascada" },
                    { 986, 66, "Combo" },
                    { 987, 66, "Corsa" },
                    { 988, 66, "Corsa OPC" },
                    { 989, 66, "Crossland" },
                    { 990, 66, "Crossland X" },
                    { 991, 66, "Frontera" },
                    { 992, 66, "Grandland" },
                    { 993, 66, "Grandland X" },
                    { 994, 66, "Insignia" },
                    { 995, 66, "Insignia OPC" },
                    { 996, 66, "Karl" },
                    { 997, 66, "Meriva" },
                    { 998, 66, "Mokka" },
                    { 999, 66, "Omega" },
                    { 1000, 66, "Signum" },
                    { 1001, 66, "Speedster" },
                    { 1002, 66, "Tigra" },
                    { 1003, 66, "Vectra" },
                    { 1004, 66, "Vivaro" },
                    { 1005, 66, "Zafira" },
                    { 1006, 66, "Zafira Life" },
                    { 1007, 66, "Zafira Tourer" },
                    { 1008, 67, "1007" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1009, 67, "107" },
                    { 1010, 67, "108" },
                    { 1011, 67, "2008" },
                    { 1012, 67, "206" },
                    { 1013, 67, "207" },
                    { 1014, 67, "208" },
                    { 1015, 67, "3008" },
                    { 1016, 67, "301" },
                    { 1017, 67, "307" },
                    { 1018, 67, "308" },
                    { 1019, 67, "4007" },
                    { 1020, 67, "4008" },
                    { 1021, 67, "406" },
                    { 1022, 67, "407" },
                    { 1023, 67, "408" },
                    { 1024, 67, "5008" },
                    { 1025, 67, "508" },
                    { 1026, 67, "607" },
                    { 1027, 67, "807" },
                    { 1028, 67, "Boxer" },
                    { 1029, 67, "Expert" },
                    { 1030, 67, "Landtrek" },
                    { 1031, 67, "Manager" },
                    { 1032, 67, "Partner" },
                    { 1033, 67, "RCZ Sport" },
                    { 1034, 67, "Rifter" },
                    { 1035, 67, "Traveller" },
                    { 1036, 68, "Road Runner" },
                    { 1037, 69, "Aztec" },
                    { 1038, 69, "Bonneville" },
                    { 1039, 69, "Firebird" },
                    { 1040, 69, "G5 Pursuit" },
                    { 1041, 69, "G6" },
                    { 1042, 69, "G8" },
                    { 1043, 69, "Grand AM" },
                    { 1044, 69, "Grand Prix" },
                    { 1045, 69, "GTO" },
                    { 1046, 69, "Montana" },
                    { 1047, 69, "Solstice" },
                    { 1048, 69, "Sunfire" },
                    { 1049, 69, "Torrent" },
                    { 1050, 69, "Vibe" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1051, 70, "718 Boxster" },
                    { 1052, 70, "718 Cayman" },
                    { 1053, 70, "911" },
                    { 1054, 70, "Boxster" },
                    { 1055, 70, "Cayenne" },
                    { 1056, 70, "Cayman" },
                    { 1057, 70, "Macan" },
                    { 1058, 70, "Panamera" },
                    { 1059, 70, "Taycan" },
                    { 1060, 71, "Gentra" },
                    { 1061, 72, "Alaskan" },
                    { 1062, 72, "Arkana" },
                    { 1063, 72, "Avantime" },
                    { 1064, 72, "Captur" },
                    { 1065, 72, "Clio" },
                    { 1066, 72, "Duster" },
                    { 1067, 72, "Duster Oroch" },
                    { 1068, 72, "Espace" },
                    { 1069, 72, "Fluence" },
                    { 1070, 72, "Grand Scenic" },
                    { 1071, 72, "Kadjar" },
                    { 1072, 72, "Kangoo" },
                    { 1073, 72, "Kaptur" },
                    { 1074, 72, "Kiger" },
                    { 1075, 72, "Koleos" },
                    { 1076, 72, "Laguna" },
                    { 1077, 72, "Latitude" },
                    { 1078, 72, "Logan" },
                    { 1079, 72, "Logan Stepway" },
                    { 1080, 72, "Master" },
                    { 1081, 72, "Megane" },
                    { 1082, 72, "Modus" },
                    { 1083, 72, "Sandero" },
                    { 1084, 72, "Sandero Stepway" },
                    { 1085, 72, "Scenic" },
                    { 1086, 72, "Symbol" },
                    { 1087, 72, "Taliant" },
                    { 1088, 72, "Talisman" },
                    { 1089, 72, "Trafic" },
                    { 1090, 72, "Triber" },
                    { 1091, 72, "Twingo" },
                    { 1092, 72, "Twizy" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1093, 72, "Vel Satis" },
                    { 1094, 72, "Wind" },
                    { 1095, 72, "Zoe" },
                    { 1096, 73, "Cullinan" },
                    { 1097, 73, "Dawn" },
                    { 1098, 73, "Ghost" },
                    { 1099, 73, "Phantom" },
                    { 1100, 73, "Wraith" },
                    { 1101, 74, "25" },
                    { 1102, 74, "400" },
                    { 1103, 74, "45" },
                    { 1104, 74, "600" },
                    { 1105, 74, "75" },
                    { 1106, 74, "Streetwise" },
                    { 1107, 75, "9-2x" },
                    { 1108, 75, "9-3" },
                    { 1109, 75, "9-4x" },
                    { 1110, 75, "9-5" },
                    { 1111, 75, "9-7x" },
                    { 1112, 76, "Aura" },
                    { 1113, 76, "Ion" },
                    { 1114, 76, "LW" },
                    { 1115, 76, "Outlook" },
                    { 1116, 76, "Sky" },
                    { 1117, 76, "Vue" },
                    { 1118, 77, "FR-S" },
                    { 1119, 77, "tC" },
                    { 1120, 77, "xA" },
                    { 1121, 77, "xB" },
                    { 1122, 77, "xD" },
                    { 1123, 78, "Alhambra" },
                    { 1124, 78, "Altea" },
                    { 1125, 78, "Altea Freetrack" },
                    { 1126, 78, "Altea XL" },
                    { 1127, 78, "Arona" },
                    { 1128, 78, "Arosa" },
                    { 1129, 78, "Ateca" },
                    { 1130, 78, "Cordoba" },
                    { 1131, 78, "Exeo" },
                    { 1132, 78, "Ibiza" },
                    { 1133, 78, "Leon" },
                    { 1134, 78, "Mii" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1135, 78, "Tarraco" },
                    { 1136, 78, "Toledo" },
                    { 1137, 79, "Citigo" },
                    { 1138, 79, "Enyaq iV" },
                    { 1139, 79, "Fabia" },
                    { 1140, 79, "Felicia" },
                    { 1141, 79, "Kamiq" },
                    { 1142, 79, "Karoq" },
                    { 1143, 79, "Kodiaq" },
                    { 1144, 79, "Octavia" },
                    { 1145, 79, "Octavia Scout" },
                    { 1146, 79, "Octavia Tour" },
                    { 1147, 79, "Praktik" },
                    { 1148, 79, "Rapid" },
                    { 1149, 79, "Rapid Spaceback (NH1)" },
                    { 1150, 79, "Roomster" },
                    { 1151, 79, "Scala" },
                    { 1152, 79, "Superb" },
                    { 1153, 79, "Yeti" },
                    { 1154, 80, "Forfour" },
                    { 1155, 80, "Fortwo" },
                    { 1156, 80, "Roadster" },
                    { 1157, 81, "Actyon" },
                    { 1158, 81, "Actyon Sports" },
                    { 1159, 81, "Chairman" },
                    { 1160, 81, "Korando" },
                    { 1161, 81, "Kyron" },
                    { 1162, 81, "Musso" },
                    { 1163, 81, "Musso Grand" },
                    { 1164, 81, "Musso Sport" },
                    { 1165, 81, "Rexton" },
                    { 1166, 81, "Rexton Sports" },
                    { 1167, 81, "Rodius" },
                    { 1168, 81, "Stavic" },
                    { 1169, 81, "Tivoli" },
                    { 1170, 81, "Tivoli Grand" },
                    { 1171, 81, "XLV" },
                    { 1172, 82, "Ascent" },
                    { 1173, 82, "Baja" },
                    { 1174, 82, "BRZ" },
                    { 1175, 82, "Crosstrack" },
                    { 1176, 82, "Exiga" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1177, 82, "Forester" },
                    { 1178, 82, "Impreza" },
                    { 1179, 82, "Justy" },
                    { 1180, 82, "Legacy" },
                    { 1181, 82, "Levorg" },
                    { 1182, 82, "Outback" },
                    { 1183, 82, "Traviq" },
                    { 1184, 82, "Tribeca" },
                    { 1185, 82, "WRX" },
                    { 1186, 82, "XV" },
                    { 1187, 83, "Alto" },
                    { 1188, 83, "Baleno" },
                    { 1189, 83, "Celerio" },
                    { 1190, 83, "Ciaz" },
                    { 1191, 83, "Ertiga" },
                    { 1192, 83, "Grand Vitara" },
                    { 1193, 83, "Grand Vitara XL7" },
                    { 1194, 83, "Ignis" },
                    { 1195, 83, "Jimny" },
                    { 1196, 83, "Kizashi" },
                    { 1197, 83, "Liana" },
                    { 1198, 83, "S-Presso" },
                    { 1199, 83, "Splash" },
                    { 1200, 83, "Swift" },
                    { 1201, 83, "SX4" },
                    { 1202, 83, "Vitara" },
                    { 1203, 83, "Wagon R" },
                    { 1204, 83, "Wagon R+" },
                    { 1205, 83, "XL6" },
                    { 1206, 83, "XL7" },
                    { 1207, 84, "Model 3" },
                    { 1208, 84, "Model S" },
                    { 1209, 84, "Model X" },
                    { 1210, 84, "Model Y" },
                    { 1211, 85, "4Runner" },
                    { 1212, 85, "Alphard" },
                    { 1213, 85, "Auris" },
                    { 1214, 85, "Avalon" },
                    { 1215, 85, "Avensis" },
                    { 1216, 85, "Avensis Verso" },
                    { 1217, 85, "Aygo" },
                    { 1218, 85, "Aygo X" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1219, 85, "C+pod" },
                    { 1220, 85, "C-HR" },
                    { 1221, 85, "Caldina" },
                    { 1222, 85, "Camry" },
                    { 1223, 85, "Celica" },
                    { 1224, 85, "Corolla" },
                    { 1225, 85, "Corolla Cross" },
                    { 1226, 85, "Corolla Verso" },
                    { 1227, 85, "FJ Cruiser" },
                    { 1228, 85, "Fortuner" },
                    { 1229, 85, "GT 86" },
                    { 1230, 85, "Hiace" },
                    { 1231, 85, "Highlander" },
                    { 1232, 85, "Hilux" },
                    { 1233, 85, "iQ" },
                    { 1234, 85, "ist" },
                    { 1235, 85, "Land Cruiser" },
                    { 1236, 85, "Land Cruiser Prado" },
                    { 1237, 85, "Mark II" },
                    { 1238, 85, "Mirai" },
                    { 1239, 85, "MR2" },
                    { 1240, 85, "Picnic" },
                    { 1241, 85, "Previa" },
                    { 1242, 85, "Prius" },
                    { 1243, 85, "Prius Prime" },
                    { 1244, 85, "RAV4" },
                    { 1245, 85, "Sequoia" },
                    { 1246, 85, "Sienna" },
                    { 1247, 85, "Supra" },
                    { 1248, 85, "Tacoma" },
                    { 1249, 85, "Tundra" },
                    { 1250, 85, "Venza" },
                    { 1251, 85, "Verso" },
                    { 1252, 85, "Vitz" },
                    { 1253, 85, "Yaris" },
                    { 1254, 85, "Yaris Verso" },
                    { 1255, 86, "Pickup" },
                    { 1256, 86, "Патриот" },
                    { 1257, 86, "Хантер" },
                    { 1258, 87, "2101-2107" },
                    { 1259, 87, "2108" },
                    { 1260, 87, "2110" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1261, 87, "2113" },
                    { 1262, 87, "4x4 Urban" },
                    { 1263, 87, "Granta" },
                    { 1264, 87, "Granta Cross" },
                    { 1265, 87, "Largus" },
                    { 1266, 87, "Largus Cross" },
                    { 1267, 87, "Niva Legend" },
                    { 1268, 87, "Niva Travel" },
                    { 1269, 87, "Vesta Cross" },
                    { 1270, 87, "Vesta Sport" },
                    { 1271, 87, "Vesta SW" },
                    { 1272, 87, "XRay" },
                    { 1273, 87, "XRay Cross" },
                    { 1274, 87, "Веста" },
                    { 1275, 87, "Калина" },
                    { 1276, 87, "Нива 4X4" },
                    { 1277, 87, "Ока" },
                    { 1278, 87, "Приора" },
                    { 1279, 88, "Amarok" },
                    { 1280, 88, "Arteon" },
                    { 1281, 88, "Beetle" },
                    { 1282, 88, "Bora" },
                    { 1283, 88, "Caddy" },
                    { 1284, 88, "CC" },
                    { 1285, 88, "Crafter" },
                    { 1286, 88, "CrossGolf" },
                    { 1287, 88, "CrossPolo" },
                    { 1288, 88, "CrossTouran" },
                    { 1289, 88, "Eos" },
                    { 1290, 88, "Fox" },
                    { 1291, 88, "Golf" },
                    { 1292, 88, "ID.3" },
                    { 1293, 88, "ID.4" },
                    { 1294, 88, "ID.4 X" },
                    { 1295, 88, "Jetta" },
                    { 1296, 88, "Lupo" },
                    { 1297, 88, "Multivan" },
                    { 1298, 88, "New Beetle" },
                    { 1299, 88, "Passat" },
                    { 1300, 88, "Passat CC" },
                    { 1301, 88, "Phaeton" },
                    { 1302, 88, "Pointer" }
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "CarMakeId", "Name" },
                values: new object[,]
                {
                    { 1303, 88, "Polo" },
                    { 1304, 88, "Routan" },
                    { 1305, 88, "Scirocco" },
                    { 1306, 88, "Sharan" },
                    { 1307, 88, "T-Roc" },
                    { 1308, 88, "Taos" },
                    { 1309, 88, "Teramont" },
                    { 1310, 88, "Teramont X" },
                    { 1311, 88, "Tiguan" },
                    { 1312, 88, "Tiguan X" },
                    { 1313, 88, "Touareg" },
                    { 1314, 88, "Touran" },
                    { 1315, 88, "Transporter" },
                    { 1316, 88, "Up" },
                    { 1317, 89, "C30" },
                    { 1318, 89, "C40" },
                    { 1319, 89, "C70" },
                    { 1320, 89, "C70 Convertible" },
                    { 1321, 89, "C70 Coupe" },
                    { 1322, 89, "S40" },
                    { 1323, 89, "S60" },
                    { 1324, 89, "S70" },
                    { 1325, 89, "S80" },
                    { 1326, 89, "S90" },
                    { 1327, 89, "V40" },
                    { 1328, 89, "V50" },
                    { 1329, 89, "V60" },
                    { 1330, 89, "V70" },
                    { 1331, 89, "V90" },
                    { 1332, 89, "XC40" },
                    { 1333, 89, "XC60" },
                    { 1334, 89, "XC70" },
                    { 1335, 89, "XC90" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, 1, 41.5364m, 23.9358m, "Ablanitsa" },
                    { 2, 1, 42.6489m, 27.6481m, "Aheloy" },
                    { 3, 1, 43.2500m, 27.8167m, "Aksakovo" },
                    { 4, 1, 42.8484m, 22.9837m, "Aldomirovtsi" },
                    { 5, 1, 43.2631m, 24.9403m, "Aleksandrovo" },
                    { 6, 1, 42.0167m, 24.8667m, "Asenovgrad" },
                    { 7, 1, 44.0978m, 27.1691m, "Aydemir" },
                    { 8, 1, 42.7000m, 27.2500m, "Aytos" },
                    { 9, 1, 42.6167m, 27.3000m, "Balgarovo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 10, 1, 43.3439m, 23.6902m, "Banitsa" },
                    { 11, 1, 42.7000m, 23.1333m, "Bankya" },
                    { 12, 1, 42.5500m, 24.8333m, "Banya" },
                    { 13, 1, 43.7763m, 25.9508m, "Basarbovo" },
                    { 14, 1, 42.7363m, 27.4917m, "Bata" },
                    { 15, 1, 42.6000m, 22.9500m, "Batanovtsi" },
                    { 16, 1, 43.3521m, 27.3312m, "Belogradets" },
                    { 17, 1, 43.1833m, 27.7000m, "Beloslav" },
                    { 18, 1, 42.2082m, 24.6499m, "Benkovski" },
                    { 19, 1, 42.2393m, 23.1575m, "Bistritsa" },
                    { 20, 1, 42.0119m, 23.0897m, "Blagoevgrad" },
                    { 21, 1, 42.1500m, 23.0167m, "Boboshevo" },
                    { 22, 1, 41.5932m, 23.7371m, "Borovo" },
                    { 23, 1, 42.9073m, 23.7937m, "Botevgrad" },
                    { 24, 1, 42.7625m, 23.1997m, "Bozhurishte" },
                    { 25, 1, 44.1500m, 22.6500m, "Bregovo" },
                    { 26, 1, 42.0839m, 24.5920m, "Brestovitsa" },
                    { 27, 1, 42.7667m, 23.5667m, "Buhovo" },
                    { 28, 1, 43.4439m, 24.6156m, "Bukovlak" },
                    { 29, 1, 42.5030m, 27.4702m, "Burgas" },
                    { 30, 1, 42.6833m, 23.4333m, "Busmantsi" },
                    { 31, 1, 41.5461m, 25.0738m, "Byal Izvor" },
                    { 32, 1, 43.4500m, 25.7333m, "Byala" },
                    { 33, 1, 42.7578m, 23.4256m, "Chepintsi" },
                    { 34, 1, 42.2720m, 24.4011m, "Chernogorovo" },
                    { 35, 1, 42.3103m, 23.1742m, "Cherven Breg" },
                    { 36, 1, 43.8056m, 26.0989m, "Chervena Voda" },
                    { 37, 1, 43.0436m, 25.6208m, "Debelets" },
                    { 38, 1, 41.7333m, 24.4000m, "Devin" },
                    { 39, 1, 43.2167m, 27.5667m, "Devnya" },
                    { 40, 1, 42.0500m, 25.6000m, "Dimitrovgrad" },
                    { 41, 1, 42.6555m, 23.0496m, "Divotino" },
                    { 42, 1, 43.1112m, 25.8330m, "Dobri Dyal" },
                    { 43, 1, 43.5667m, 27.8333m, "Dobrich" },
                    { 44, 1, 42.9522m, 27.2872m, "Dobromir" },
                    { 45, 1, 42.8167m, 23.2833m, "Dobroslavtsi" },
                    { 46, 1, 41.4238m, 25.1320m, "Dolen" },
                    { 47, 1, 42.2833m, 23.7667m, "Dolna Banya" },
                    { 48, 1, 43.1564m, 25.7392m, "Dolna Oryahovitsa" },
                    { 49, 1, 42.7000m, 23.5000m, "Dolni Bogrov" },
                    { 50, 1, 43.2228m, 25.7468m, "Draganovo" },
                    { 51, 1, 42.6043m, 23.1391m, "Dragichevo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 52, 1, 42.2650m, 23.1185m, "Dupnitsa" },
                    { 53, 1, 42.2263m, 23.0811m, "Dzherman" },
                    { 54, 1, 43.1374m, 25.9017m, "Dzhulyunitsa" },
                    { 55, 1, 42.6667m, 23.6000m, "Elin Pelin" },
                    { 56, 1, 42.8333m, 24.0000m, "Etropole" },
                    { 57, 1, 43.2018m, 27.7671m, "Ezerovo" },
                    { 58, 1, 43.5345m, 23.2646m, "Gabrovnitsa" },
                    { 59, 1, 42.8667m, 25.3333m, "Gabrovo" },
                    { 60, 1, 42.7689m, 27.5287m, "Galabets" },
                    { 61, 1, 42.6167m, 23.4167m, "German" },
                    { 62, 1, 42.1574m, 24.3088m, "Glavinitsa" },
                    { 63, 1, 43.6892m, 23.8081m, "Glozhene" },
                    { 64, 1, 41.5953m, 25.3935m, "Gluhar" },
                    { 65, 1, 43.1333m, 25.7000m, "Gorna Oryahovitsa" },
                    { 66, 1, 42.7167m, 23.5333m, "Gorni Bogrov" },
                    { 67, 1, 42.0214m, 25.4250m, "Gorski Izvor" },
                    { 68, 1, 41.5667m, 23.7333m, "Gotse Delchev" },
                    { 69, 1, 42.1472m, 25.2080m, "Gradina" },
                    { 70, 1, 42.2818m, 24.7329m, "Graf Ignatievo" },
                    { 71, 1, 43.4132m, 24.7054m, "Grivitsa" },
                    { 72, 1, 43.6333m, 24.7000m, "Gulyantsi" },
                    { 73, 1, 42.6572m, 25.7961m, "Gurkovo" },
                    { 74, 1, 42.7659m, 27.5998m, "Gyulyovtsa" },
                    { 75, 1, 42.1424m, 24.4555m, "Hadzhievo" },
                    { 76, 1, 41.9333m, 25.5667m, "Haskovo" },
                    { 77, 1, 43.2483m, 27.7783m, "Ignatievo" },
                    { 78, 1, 43.7167m, 26.8333m, "Isperih" },
                    { 79, 1, 42.2249m, 24.3304m, "Ivaylo" },
                    { 80, 1, 43.2952m, 27.7694m, "Izvorsko" },
                    { 81, 1, 42.6500m, 27.5667m, "Kableshkovo" },
                    { 82, 1, 42.1314m, 24.6072m, "Kadievo" },
                    { 83, 1, 42.2364m, 24.8238m, "Kalekovets" },
                    { 84, 1, 44.0861m, 27.2505m, "Kalipetrovo" },
                    { 85, 1, 42.6167m, 24.9833m, "Kalofer" },
                    { 86, 1, 43.2493m, 27.8999m, "Kamenar" },
                    { 87, 1, 42.2622m, 24.1715m, "Karabunar" },
                    { 88, 1, 42.7357m, 27.1845m, "Karageorgievo" },
                    { 89, 1, 43.6500m, 27.5667m, "Karapelit" },
                    { 90, 1, 41.6500m, 25.3667m, "Kardzhali" },
                    { 91, 1, 42.6436m, 24.8072m, "Karlovo" },
                    { 92, 1, 42.6167m, 25.4000m, "Kazanlak" },
                    { 93, 1, 42.6667m, 23.4667m, "Kazichene" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 94, 1, 42.5040m, 26.2560m, "Kermen" },
                    { 95, 1, 42.9833m, 25.6333m, "Kilifarevo" },
                    { 96, 1, 42.5680m, 23.1796m, "Kladnitsa" },
                    { 97, 1, 42.5833m, 23.4167m, "Kokalyane" },
                    { 98, 1, 43.1627m, 27.7806m, "Konstantinovo" },
                    { 99, 1, 42.7516m, 27.6907m, "Kosharitsa" },
                    { 100, 1, 42.0000m, 24.0167m, "Kostandovo" },
                    { 101, 1, 42.1790m, 24.6235m, "Kostievo" },
                    { 102, 1, 42.8167m, 23.2167m, "Kostinbrod" },
                    { 103, 1, 43.3572m, 24.1444m, "Koynare" },
                    { 104, 1, 43.7833m, 23.7333m, "Kozloduy" },
                    { 105, 1, 42.6669m, 25.3831m, "Kran" },
                    { 106, 1, 41.9599m, 23.8208m, "Krastava" },
                    { 107, 1, 42.3199m, 23.2012m, "Kraynitsi" },
                    { 108, 1, 42.0081m, 25.5900m, "Krepost" },
                    { 109, 1, 42.0500m, 24.4667m, "Krichim" },
                    { 110, 1, 42.6833m, 23.4667m, "Krivina" },
                    { 111, 1, 42.5629m, 26.3822m, "Krushare" },
                    { 112, 1, 42.4477m, 26.5331m, "Kukorevo" },
                    { 113, 1, 42.0930m, 24.4998m, "Kurtovo Konare" },
                    { 114, 1, 42.2833m, 22.6833m, "Kyustendil" },
                    { 115, 1, 42.6431m, 23.6402m, "Lesnovo" },
                    { 116, 1, 43.8256m, 23.2375m, "Lom" },
                    { 117, 1, 43.1347m, 24.7172m, "Lovech" },
                    { 118, 1, 42.6000m, 23.4833m, "Lozen" },
                    { 119, 1, 43.1000m, 25.7167m, "Lyaskovets" },
                    { 120, 1, 43.3428m, 27.7756m, "Lyuben Karavelovo" },
                    { 121, 1, 41.5000m, 24.9500m, "Madan" },
                    { 122, 1, 42.7273m, 27.3702m, "Maglen" },
                    { 123, 1, 41.8580m, 25.6301m, "Malevo" },
                    { 124, 1, 42.1990m, 24.4312m, "Malo Konare" },
                    { 125, 1, 42.5995m, 23.1703m, "Marchaevo" },
                    { 126, 1, 42.4015m, 27.4821m, "Marinka" },
                    { 127, 1, 43.9167m, 26.0833m, "Marten" },
                    { 128, 1, 42.1333m, 25.5000m, "Merichleri" },
                    { 129, 1, 42.7833m, 23.3000m, "Mirovyane" },
                    { 130, 1, 42.1765m, 24.2936m, "Mokrishte" },
                    { 131, 1, 43.4075m, 23.2217m, "Montana" },
                    { 132, 1, 43.5238m, 26.6439m, "Mortagonovo" },
                    { 133, 1, 42.7695m, 23.4041m, "Negovan" },
                    { 134, 1, 42.6333m, 25.8000m, "Nikolaevo" },
                    { 135, 1, 43.8573m, 26.0929m, "Nikolovo" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 136, 1, 43.3500m, 27.2000m, "Novi Pazar" },
                    { 137, 1, 43.8022m, 26.1932m, "Novo Selo" },
                    { 138, 1, 42.8167m, 27.8833m, "Obzor" },
                    { 139, 1, 42.1475m, 24.4139m, "Ognyanovo" },
                    { 140, 1, 43.1000m, 26.4170m, "Omurtag" },
                    { 141, 1, 41.6022m, 25.3595m, "Opalchensko" },
                    { 142, 1, 42.7498m, 27.6204m, "Orizare" },
                    { 143, 1, 43.6853m, 26.6070m, "Ostrovo" },
                    { 144, 1, 42.5000m, 24.1833m, "Panagyurishte" },
                    { 145, 1, 42.5708m, 23.4397m, "Pancharevo" },
                    { 146, 1, 42.0745m, 24.6630m, "Parvenets" },
                    { 147, 1, 43.1530m, 25.6521m, "Parvomaytsi" },
                    { 148, 1, 42.1220m, 24.2022m, "Patalenitsa" },
                    { 149, 1, 42.2000m, 24.3333m, "Pazardzhik" },
                    { 150, 1, 42.6000m, 23.0333m, "Pernik" },
                    { 151, 1, 42.0500m, 24.5500m, "Perushtitsa" },
                    { 152, 1, 42.0333m, 24.3000m, "Peshtera" },
                    { 153, 1, 42.8437m, 23.1480m, "Petarch" },
                    { 154, 1, 41.3953m, 23.2069m, "Petrich" },
                    { 155, 1, 43.1558m, 25.7902m, "Pisarevo" },
                    { 156, 1, 42.8916m, 27.1551m, "Planinitsa" },
                    { 157, 1, 43.4078m, 24.6203m, "Pleven" },
                    { 158, 1, 42.1500m, 24.7500m, "Plovdiv" },
                    { 159, 1, 43.1807m, 25.6199m, "Polikrayshte" },
                    { 160, 1, 42.5683m, 27.6167m, "Pomorie" },
                    { 161, 1, 42.7982m, 27.4513m, "Prosenik" },
                    { 162, 1, 43.1833m, 27.4333m, "Provadia" },
                    { 163, 1, 41.9833m, 24.0833m, "Rakitovo" },
                    { 164, 1, 43.6011m, 26.4641m, "Rakovski" },
                    { 165, 1, 42.6434m, 27.6783m, "Ravda" },
                    { 166, 1, 42.5145m, 27.2405m, "Ravnets" },
                    { 167, 1, 43.5333m, 26.5167m, "Razgrad" },
                    { 168, 1, 42.7826m, 27.3955m, "Razhitsa" },
                    { 169, 1, 41.8833m, 23.4667m, "Razlog" },
                    { 170, 1, 43.1958m, 25.5591m, "Resen" },
                    { 171, 1, 42.1818m, 24.8646m, "Rogosh" },
                    { 172, 1, 42.5809m, 23.1677m, "Rudartsi" },
                    { 173, 1, 41.4833m, 24.8500m, "Rudozem" },
                    { 174, 1, 42.7998m, 27.2822m, "Ruen" },
                    { 175, 1, 43.8231m, 25.9539m, "Ruse" },
                    { 176, 1, 42.1330m, 24.9330m, "Sadovo" },
                    { 177, 1, 42.2667m, 24.5500m, "Saedinenie" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 178, 1, 42.2655m, 23.1583m, "Samoranovo" },
                    { 179, 1, 43.9327m, 26.1149m, "Sandrovo" },
                    { 180, 1, 42.2833m, 23.2667m, "Sapareva Banya" },
                    { 181, 1, 42.2511m, 24.3203m, "Saraya" },
                    { 182, 1, 41.7333m, 24.0333m, "Sarnitsa" },
                    { 183, 1, 42.6434m, 26.1526m, "Seliminovo" },
                    { 184, 1, 43.7701m, 26.1703m, "Semerdzhievo" },
                    { 185, 1, 42.2167m, 24.1000m, "Septemvri" },
                    { 186, 1, 42.7000m, 25.3833m, "Shipka" },
                    { 187, 1, 43.2833m, 26.9333m, "Shumen" },
                    { 188, 1, 44.1172m, 27.2606m, "Silistra" },
                    { 189, 1, 42.1480m, 24.3765m, "Sinitovo" },
                    { 190, 1, 42.1782m, 24.8417m, "Skutare" },
                    { 191, 1, 43.4667m, 24.8667m, "Slavyanovo" },
                    { 192, 1, 42.6833m, 26.3333m, "Sliven" },
                    { 193, 1, 42.8500m, 23.0333m, "Slivnitsa" },
                    { 194, 1, 42.2633m, 22.7019m, "Slokoshtitsa" },
                    { 195, 1, 41.5833m, 24.7000m, "Smolyan" },
                    { 196, 1, 42.8470m, 27.2775m, "Snyagovo" },
                    { 197, 1, 42.7000m, 23.3300m, "Sofia" },
                    { 198, 1, 42.6500m, 24.7500m, "Sopot" },
                    { 199, 1, 42.6909m, 26.4035m, "Sotirya" },
                    { 200, 1, 42.1333m, 24.5333m, "Stamboliyski" },
                    { 201, 1, 42.4333m, 25.6500m, "Stara Zagora" },
                    { 202, 1, 41.4269m, 25.0568m, "Startsevo" },
                    { 203, 1, 43.4833m, 27.8667m, "Stefanovo" },
                    { 204, 1, 43.4500m, 27.8167m, "Stozher" },
                    { 205, 1, 42.7726m, 27.4710m, "Stratsin" },
                    { 206, 1, 42.5439m, 23.1209m, "Studena" },
                    { 207, 1, 43.3333m, 27.6000m, "Suvorovo" },
                    { 208, 1, 42.7167m, 27.7667m, "Sveti Vlas" },
                    { 209, 1, 42.7819m, 23.3838m, "Svetovrachane" },
                    { 210, 1, 43.6167m, 25.3500m, "Svishtov" },
                    { 211, 1, 42.6946m, 27.6496m, "Tankovo" },
                    { 212, 1, 43.2500m, 26.5833m, "Targovishte" },
                    { 213, 1, 42.6513m, 26.4278m, "Topolchane" },
                    { 214, 1, 43.2183m, 27.8214m, "Topoli" },
                    { 215, 1, 42.9442m, 27.2117m, "Tranak" },
                    { 216, 1, 42.1358m, 24.4661m, "Trivoditsi" },
                    { 217, 1, 42.2321m, 24.7246m, "Trud" },
                    { 218, 1, 42.2027m, 24.6875m, "Tsaratsovo" },
                    { 219, 1, 43.3307m, 27.0114m, "Tsarev Brod" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 220, 1, 41.9631m, 25.6558m, "Uzundzhovo" },
                    { 221, 1, 43.2944m, 27.8488m, "Vaglen" },
                    { 222, 1, 41.5959m, 24.0636m, "Vaklinovo" },
                    { 223, 1, 41.5290m, 23.9894m, "Valkosel" },
                    { 224, 1, 43.1809m, 25.8290m, "Varbitsa" },
                    { 225, 1, 43.1340m, 26.5309m, "Vardun" },
                    { 226, 1, 43.2167m, 27.9167m, "Varna" },
                    { 227, 1, 43.1830m, 23.2830m, "Varshets" },
                    { 228, 1, 43.1667m, 26.8170m, "Veliki Preslav" },
                    { 229, 1, 43.0778m, 25.6167m, "Veliko Tarnovo" },
                    { 230, 1, 42.0167m, 24.0000m, "Velingrad" },
                    { 231, 1, 42.5329m, 26.5448m, "Veselinovo" },
                    { 232, 1, 43.7000m, 26.2667m, "Vetovo" },
                    { 233, 1, 42.2667m, 24.0500m, "Vetren" },
                    { 234, 1, 44.0000m, 22.8667m, "Vidin" },
                    { 235, 1, 42.1353m, 25.1363m, "Vinitsa" },
                    { 236, 1, 42.2861m, 24.1302m, "Vinogradets" },
                    { 237, 1, 42.6333m, 23.2000m, "Vladaya" },
                    { 238, 1, 42.7727m, 23.2429m, "Voluyak" },
                    { 239, 1, 42.2167m, 24.6333m, "Voysil" },
                    { 240, 1, 42.1999m, 24.7942m, "Voyvodinovo" },
                    { 241, 1, 41.8588m, 25.5472m, "Voyvodovo" },
                    { 242, 1, 43.2000m, 23.5500m, "Vratsa" },
                    { 243, 1, 42.8124m, 27.1985m, "Vresovo" },
                    { 244, 1, 42.7946m, 27.2540m, "Yabalchevo" },
                    { 245, 1, 42.0699m, 25.4411m, "Yabalkovo" },
                    { 246, 1, 42.2888m, 23.1423m, "Yahinovo" },
                    { 247, 1, 42.4833m, 26.5000m, "Yambol" },
                    { 248, 1, 42.7780m, 27.1697m, "Zaychar" },
                    { 249, 1, 42.5333m, 23.3667m, "Zheleznitsa" },
                    { 250, 1, 41.3833m, 25.1000m, "Zlatograd" },
                    { 251, 1, 43.1538m, 27.8372m, "Zvezditsa" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Автоматичен контрол на стабилността" },
                    { 2, 1, "ABS - система против блокиране на колелата" },
                    { 3, 1, "Предни въздушни възглавници" },
                    { 4, 1, "Задни въздушни възглавници" },
                    { 5, 1, "Странични въздушни възглавници" },
                    { 6, 1, "EBD - разпределяне на спирачното усилие" },
                    { 7, 1, "Контрол на налягането на гумите" },
                    { 8, 1, "Парктроник" },
                    { 9, 1, "Система за защита от пробуксуване" },
                    { 10, 1, "Distronic - система за контрол на дистанцията" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 11, 1, "BAS - система за подпомагане на спирането" },
                    { 12, 1, "Електронна програма за стабилизиране" },
                    { 13, 1, "Блокаж на диференциала" },
                    { 14, 2, "Старт/Стоп система" },
                    { 15, 2, "USB, audio/video, IN/AUX изводи" },
                    { 16, 2, "Ел. огледала" },
                    { 17, 2, "Ел. стъкла" },
                    { 18, 2, "Автоматични чистачки" },
                    { 19, 2, "Автоматични фарове" },
                    { 20, 2, "Ел. регулиране на окачването" },
                    { 21, 2, "Ел. регулиране на седалките" },
                    { 22, 2, "Ел. усилвател на волана" },
                    { 23, 2, "Климатик" },
                    { 24, 2, "Мултифункционален волан" },
                    { 25, 2, "Навигация" },
                    { 26, 2, "Подгев на предните седалките" },
                    { 27, 2, "Подгев на задните седалките" },
                    { 28, 2, "Регулиране на волана" },
                    { 29, 2, "Серво усилвател на волана" },
                    { 30, 2, "Система за измиване на фаровете" },
                    { 31, 2, "Круиз контрол" },
                    { 32, 2, "Хладилна жабка" },
                    { 33, 2, "Безключово палене" },
                    { 34, 2, "Bluetooth мултимедия" },
                    { 35, 2, "Stereo уредба" },
                    { 36, 3, "Аларма" },
                    { 37, 3, "Автокаско" },
                    { 38, 3, "Централно заключване" },
                    { 39, 3, "GPS проследяване" },
                    { 40, 2, "Ел. багажник" },
                    { 41, 4, "Годишен данък" },
                    { 42, 4, "Гражданска отговорност" },
                    { 43, 4, "Платен и преминат ГТП" },
                    { 44, 4, "Обслужени базови консумативи" },
                    { 45, 4, "Винетка" },
                    { 46, 5, "Кожен салон" },
                    { 47, 5, "Алкантара салон" },
                    { 48, 5, "Тапициран таван" },
                    { 49, 5, "Кожени стелки" },
                    { 50, 5, "Гумени стелки" },
                    { 51, 5, "DVD" },
                    { 52, 5, "Поставки за чаши" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[] { 53, 5, "Подлакатник с поставки" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_ListingId",
                table: "CarImages",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarMakeId",
                table: "CarModels",
                column: "CarMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_CategoryId",
                table: "Features",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingFeatures_FeatureId",
                table: "ListingFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarMakeId",
                table: "Listings",
                column: "CarMakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CarModelId",
                table: "Listings",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CityId",
                table: "Listings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CreatorId",
                table: "Listings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_EngineTypeId",
                table: "Listings",
                column: "EngineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ThumbnailId",
                table: "Listings",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteListings_ListingId",
                table: "UserFavoriteListings",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Listings_ListingId",
                table: "CarImages",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_CreatorId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Listings_ListingId",
                table: "CarImages");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ListingFeatures");

            migrationBuilder.DropTable(
                name: "UserFavoriteListings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "CarImages");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "EngineTypes");

            migrationBuilder.DropTable(
                name: "CarMakes");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
