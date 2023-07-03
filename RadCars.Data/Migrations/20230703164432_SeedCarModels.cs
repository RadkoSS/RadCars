using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadCars.Data.Migrations
{
    public partial class SeedCarModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 645);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 646);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 647);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 811);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 812);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 813);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 814);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 815);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 816);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 817);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 818);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 819);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 820);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 821);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 822);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 823);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 824);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 825);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 826);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 827);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 828);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 829);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 830);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 831);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 832);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 833);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 834);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 835);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 836);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 837);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 838);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 839);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 840);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 841);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 842);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 843);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 844);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 845);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 846);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 847);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 848);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 849);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 850);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 851);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 852);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 853);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 854);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 855);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 856);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 857);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 858);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 859);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 860);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 861);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 862);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 863);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 864);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 865);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 866);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 867);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 868);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 869);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 870);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 871);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 872);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 873);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 875);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 876);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 877);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 878);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 879);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 880);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 881);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 882);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 883);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 884);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 885);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 886);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 887);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 888);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 889);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 890);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 891);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 892);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 893);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 894);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 895);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 896);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 897);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 898);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 899);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 900);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 910);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 911);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 912);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 913);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 914);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 915);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 916);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 917);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 918);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 919);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 920);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 921);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 922);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 923);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 924);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 925);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 926);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 927);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 928);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 929);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 930);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 931);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 932);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 933);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 934);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 935);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 936);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 937);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 938);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 939);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 940);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 941);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 942);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 943);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 944);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 945);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 946);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 947);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 948);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 949);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 950);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 951);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 952);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 953);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 954);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 955);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 956);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 957);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 958);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 959);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 960);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 961);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 962);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 963);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 964);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 965);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 966);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 967);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 968);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 969);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 970);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 971);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 972);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 973);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 974);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 975);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 976);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 977);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 978);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 979);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 980);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 981);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 982);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 983);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 984);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 985);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 986);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 987);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 988);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 989);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 990);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 991);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 992);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 993);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 994);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 995);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 996);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 997);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 998);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1027);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1028);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1029);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1030);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1031);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1032);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1033);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1034);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1035);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1036);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1037);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1038);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1039);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1040);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1041);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1042);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1043);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1044);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1045);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1046);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1047);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1048);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1049);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1050);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1051);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1052);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1053);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1054);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1055);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1056);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1057);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1058);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1059);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1060);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1061);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1062);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1063);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1064);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1065);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1066);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1067);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1068);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1069);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1070);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1071);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1072);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1073);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1074);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1075);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1076);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1077);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1078);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1079);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1080);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1081);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1082);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1083);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1084);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1085);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1086);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1087);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1088);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1089);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1090);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1091);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1092);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1093);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1094);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1095);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1096);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1097);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1098);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1099);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1100);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1101);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1102);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1103);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1104);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1105);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1106);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1107);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1108);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1109);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1110);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1111);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1112);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1113);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1114);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1115);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1116);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1117);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1118);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1119);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1120);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1121);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1122);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1123);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1124);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1125);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1126);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1127);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1128);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1129);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1130);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1131);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1132);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1133);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1134);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1135);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1136);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1137);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1138);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1139);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1140);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1141);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1142);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1143);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1144);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1145);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1146);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1147);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1148);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1149);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1150);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1151);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1152);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1153);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1154);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1155);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1156);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1157);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1158);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1159);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1160);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1161);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1162);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1163);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1164);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1165);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1166);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1167);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1168);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1169);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1170);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1171);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1172);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1173);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1174);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1175);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1176);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1177);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1178);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1179);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1180);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1181);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1182);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1183);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1184);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1185);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1186);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1187);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1188);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1189);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1190);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1191);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1192);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1193);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1194);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1195);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1196);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1197);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1198);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1199);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1200);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1201);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1202);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1203);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1204);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1205);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1206);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1207);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1208);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1209);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1210);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1211);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1212);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1213);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1214);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1215);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1216);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1217);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1218);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1219);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1220);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1221);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1222);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1223);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1224);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1225);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1226);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1227);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1228);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1229);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1230);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1231);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1232);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1233);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1234);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1235);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1236);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1237);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1238);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1239);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1240);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1241);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1242);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1243);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1244);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1245);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1246);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1247);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1248);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1249);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1250);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1251);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1252);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1253);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1254);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1255);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1256);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1257);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1258);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1259);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1260);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1261);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1262);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1263);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1264);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1265);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1266);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1267);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1268);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1269);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1270);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1271);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1272);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1273);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1274);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1275);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1276);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1277);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1278);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1279);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1280);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1281);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1282);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1283);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1284);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1285);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1286);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1287);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1288);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1289);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1290);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1291);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1292);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1293);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1294);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1295);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1296);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1297);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1298);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1299);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1300);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1301);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1302);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1303);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1304);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1305);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1306);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1307);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1308);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1309);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1310);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1311);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1312);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1313);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1314);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1315);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1316);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1317);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1318);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1319);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1320);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1321);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1322);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1323);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1324);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1325);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1326);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1327);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1328);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1329);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1330);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1331);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1332);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1333);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1334);

            migrationBuilder.DeleteData(
                table: "CarModels",
                keyColumn: "Id",
                keyValue: 1335);
        }
    }
}
