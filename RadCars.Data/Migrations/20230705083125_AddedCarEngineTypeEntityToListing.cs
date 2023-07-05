using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class AddedCarEngineTypeEntityToListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineType",
                table: "Listings");

            migrationBuilder.AddColumn<byte>(
                name: "EngineTypeId",
                table: "Listings",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "CarEngineType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEngineType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarEngineType",
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

            migrationBuilder.CreateIndex(
                name: "IX_Listings_EngineTypeId",
                table: "Listings",
                column: "EngineTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_CarEngineType_EngineTypeId",
                table: "Listings",
                column: "EngineTypeId",
                principalTable: "CarEngineType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_CarEngineType_EngineTypeId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "CarEngineType");

            migrationBuilder.DropIndex(
                name: "IX_Listings_EngineTypeId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "EngineTypeId",
                table: "Listings");

            migrationBuilder.AddColumn<int>(
                name: "EngineType",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
