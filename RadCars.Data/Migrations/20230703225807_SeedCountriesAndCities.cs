using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class SeedCountriesAndCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Listings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
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
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Bulgaria" });

            migrationBuilder.InsertData(
                table: "City",
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
                    { 9, 1, 42.6167m, 27.3000m, "Balgarovo" },
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
                    { 42, 1, 43.1112m, 25.8330m, "Dobri Dyal" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 43, 1, 43.5667m, 27.8333m, "Dobrich" },
                    { 44, 1, 42.9522m, 27.2872m, "Dobromir" },
                    { 45, 1, 42.8167m, 23.2833m, "Dobroslavtsi" },
                    { 46, 1, 41.4238m, 25.1320m, "Dolen" },
                    { 47, 1, 42.2833m, 23.7667m, "Dolna Banya" },
                    { 48, 1, 43.1564m, 25.7392m, "Dolna Oryahovitsa" },
                    { 49, 1, 42.7000m, 23.5000m, "Dolni Bogrov" },
                    { 50, 1, 43.2228m, 25.7468m, "Draganovo" },
                    { 51, 1, 42.6043m, 23.1391m, "Dragichevo" },
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
                    { 84, 1, 44.0861m, 27.2505m, "Kalipetrovo" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 85, 1, 42.6167m, 24.9833m, "Kalofer" },
                    { 86, 1, 43.2493m, 27.8999m, "Kamenar" },
                    { 87, 1, 42.2622m, 24.1715m, "Karabunar" },
                    { 88, 1, 42.7357m, 27.1845m, "Karageorgievo" },
                    { 89, 1, 43.6500m, 27.5667m, "Karapelit" },
                    { 90, 1, 41.6500m, 25.3667m, "Kardzhali" },
                    { 91, 1, 42.6436m, 24.8072m, "Karlovo" },
                    { 92, 1, 42.6167m, 25.4000m, "Kazanlak" },
                    { 93, 1, 42.6667m, 23.4667m, "Kazichene" },
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
                    { 126, 1, 42.4015m, 27.4821m, "Marinka" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 127, 1, 43.9167m, 26.0833m, "Marten" },
                    { 128, 1, 42.1333m, 25.5000m, "Merichleri" },
                    { 129, 1, 42.7833m, 23.3000m, "Mirovyane" },
                    { 130, 1, 42.1765m, 24.2936m, "Mokrishte" },
                    { 131, 1, 43.4075m, 23.2217m, "Montana" },
                    { 132, 1, 43.5238m, 26.6439m, "Mortagonovo" },
                    { 133, 1, 42.7695m, 23.4041m, "Negovan" },
                    { 134, 1, 42.6333m, 25.8000m, "Nikolaevo" },
                    { 135, 1, 43.8573m, 26.0929m, "Nikolovo" },
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
                    { 168, 1, 42.7826m, 27.3955m, "Razhitsa" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 169, 1, 41.8833m, 23.4667m, "Razlog" },
                    { 170, 1, 43.1958m, 25.5591m, "Resen" },
                    { 171, 1, 42.1818m, 24.8646m, "Rogosh" },
                    { 172, 1, 42.5809m, 23.1677m, "Rudartsi" },
                    { 173, 1, 41.4833m, 24.8500m, "Rudozem" },
                    { 174, 1, 42.7998m, 27.2822m, "Ruen" },
                    { 175, 1, 43.8231m, 25.9539m, "Ruse" },
                    { 176, 1, 42.1330m, 24.9330m, "Sadovo" },
                    { 177, 1, 42.2667m, 24.5500m, "Saedinenie" },
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
                    { 210, 1, 43.6167m, 25.3500m, "Svishtov" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 211, 1, 42.6946m, 27.6496m, "Tankovo" },
                    { 212, 1, 43.2500m, 26.5833m, "Targovishte" },
                    { 213, 1, 42.6513m, 26.4278m, "Topolchane" },
                    { 214, 1, 43.2183m, 27.8214m, "Topoli" },
                    { 215, 1, 42.9442m, 27.2117m, "Tranak" },
                    { 216, 1, 42.1358m, 24.4661m, "Trivoditsi" },
                    { 217, 1, 42.2321m, 24.7246m, "Trud" },
                    { 218, 1, 42.2027m, 24.6875m, "Tsaratsovo" },
                    { 219, 1, 43.3307m, 27.0114m, "Tsarev Brod" },
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

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CityId",
                table: "Listings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CountryId",
                table: "Listings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_City_CityId",
                table: "Listings",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Country_CountryId",
                table: "Listings",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_City_CityId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Country_CountryId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CityId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_CountryId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Listings");
        }
    }
}
