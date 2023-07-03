using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 10, 1, "Distronic - система за контрол на дистанцията" },
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
                    { 42, 4, "Гражданска отговорност" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 43, 4, "Платен и преминат ГТП" },
                    { 44, 4, "Обслужени базови консумативи" },
                    { 45, 4, "Винетка" },
                    { 46, 5, "Кожен салон" },
                    { 47, 5, "Алкантара салон" },
                    { 48, 5, "Тапициран таван" },
                    { 49, 5, "Кожени стелки" },
                    { 50, 5, "Гумени стелки" },
                    { 51, 5, "DVD" },
                    { 52, 5, "Поставки за чаши" },
                    { 53, 5, "Подлакатник с поставки" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 53);
        }
    }
}
