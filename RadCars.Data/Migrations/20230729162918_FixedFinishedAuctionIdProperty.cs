using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class FixedFinishedAuctionIdProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndAuctionJobId",
                table: "Auctions",
                type: "nvarchar(38)",
                maxLength: 38,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndAuctionJobId",
                table: "Auctions",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(38)",
                oldMaxLength: 38,
                oldNullable: true);
        }
    }
}
