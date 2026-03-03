using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTypeAndCurrencyTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "CollaboratorContracts");

            migrationBuilder.AddColumn<int>(
                name: "DefaultContractTypeId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultCurrencyTypeId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DefaultContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultCurrencyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultCurrencyTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultContractTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Indefinido", "Indefinite" },
                    { 3, "Obra Labor", "Labor" },
                    { 4, "Prestaciones", "Service" }
                });

            migrationBuilder.InsertData(
                table: "DefaultCurrencyTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "AED (Emiratos Árabes Unidos Dirham)", "AED (United Arab Emirates Dirham)" },
                    { 3, "AFN (Afganistán Afgani)", "AFN (Afghanistan Afghani)" },
                    { 4, "ALL (Albania Lek)", "ALL (Albania Lek)" },
                    { 5, "AMD (Armenia Dram)", "AMD (Armenia Dram)" },
                    { 6, "ANG (Antillas Holandesas Florín)", "ANG (Netherlands Antilles Florin)" },
                    { 7, "AOA (Angola Kwanza)", "AOA (Angola Kwanza)" },
                    { 8, "ARS (Peso argentino)", "ARS (Argentine Peso)" },
                    { 9, "ATS (EURO) (Austria Chelín)", "ATS (EURO) (Austria Shilling)" },
                    { 10, "AUD (Dólar australiano)", "AUD (Australian Dollar)" },
                    { 11, "AWG (Aruba Florín)", "AWG (Aruban Florin)" },
                    { 12, "AZN (Azerbaiyán Nuevo Manat)", "AZN (Azerbaijan New Manat)" },
                    { 13, "BAM (Bosnia Marco)", "BAM (Bosnia Marka)" },
                    { 14, "BBD (Barbados Dólar)", "BBD (Barbados Dollar)" },
                    { 15, "BDT (Bangladesh Taka)", "BDT (Bangladesh Taka)" },
                    { 16, "BEF (EURO) (Bélgica Franco)", "BEF (EURO) (Belgium Franc)" },
                    { 17, "BGN (Bulgaria Lev)", "BGN (Bulgaria Lev)" },
                    { 18, "BHD (Bahréin Dinar)", "BHD (Bahrain Dinar)" },
                    { 19, "BIF (Burundi Franco)", "BIF (Burundi Franc)" },
                    { 20, "BMD (Bermuda Dólar)", "BMD (Bermuda Dollar)" },
                    { 21, "BND (Brunéi Dólar)", "BND (Brunei Dollar)" },
                    { 22, "BOB (Bolivia Boliviano)", "BOB (Bolivian Boliviano)" },
                    { 23, "BRL (Real brasileño)", "BRL (Brazilian Real)" },
                    { 24, "BSD (Bahamas Dólar)", "BSD (Bahamas Dollar)" },
                    { 25, "BTN (Bután Ngultrum)", "BTN (Bhutanese Ngultrum)" },
                    { 26, "BWP (Botsuana Pula)", "BWP (Botswana Pula)" },
                    { 27, "BYR (Bielorrusia Rublo)", "BYR (Belarusian Rouble)" },
                    { 28, "BZD (Belice Dólar)", "BZD (Belize Dollar)" },
                    { 29, "CAD (Canadá Dólar)", "CAD (Canada Dollar)" },
                    { 30, "CDF (Congo Franco)", "CDF (Congo Franc)" },
                    { 31, "CHF (Franco suizo)", "CHF (Swiss Franc)" },
                    { 32, "CLP (Peso chileno)", "CLP (Chilean Peso)" },
                    { 33, "CNY (China Yuan/Renminbi)", "CNY (China Yuan/Renminbi)" },
                    { 34, "COP (Peso colombiano)", "COP (Colombian Peso)" },
                    { 35, "CRC (Costa Rica Colón)", "CRC (Costa Rican Colon)" },
                    { 36, "CUC (Cuba Peso convertible)", "CUC (Cuban Convertible Peso)" },
                    { 37, "CUP (Peso cubano)", "CUP (Cuban Peso)" },
                    { 38, "CVE (Cabo Verde Escudo)", "CVE (Cape Verde Escudo)" },
                    { 39, "CYP (EURO) (Chipre Libra)", "CYP (EURO) (Cyprus Pound)" },
                    { 40, "CZK (República Checa Corona)", "CZK (Czech Republic Koruna)" },
                    { 41, "DJF (Yibuti Franco)", "DJF (Djibouti Franc)" },
                    { 42, "DKK (Dinamarca Corona)", "DKK (Denmark Krone)" },
                    { 43, "DMK (EURO) (Alemania Marco)", "DMK (EURO) (Germany Mark)" },
                    { 44, "DOP (República Dominicana Peso)", "DOP (Dominican Republic Peso)" },
                    { 45, "DZD (Argelia Dinar)", "DZD (Algerian Dinar)" },
                    { 46, "EEK (EURO) (Estonia Corona)", "EEK (EURO) (Estonia Kroon)" },
                    { 47, "EGP (Egipto Libra)", "EGP (Egypt Pound)" },
                    { 48, "ESP (EURO) (España Peseta)", "ESP (EURO) (Spain Peseta)" },
                    { 49, "ETB (Etiopía Birr)", "ETB (Ethiopia Birr)" },
                    { 50, "EUR (Euro)", "EUR (Euro)" },
                    { 51, "FIM (EURO) (Finlandia Marco)", "FIM (EURO) (Finland Mark)" },
                    { 52, "FJD (Fiji Dólar)", "FJD (Fiji Dollar)" },
                    { 53, "FKP (Islas Falkland Libra)", "FKP (Falkland Islands Pound)" },
                    { 54, "GBP (Gran Bretaña Libra esterlina)", "GBP (Great Britain Pound Sterling)" },
                    { 55, "GEL (Georgia Lari)", "GEL (Georgia Lari)" },
                    { 56, "GHS (Ghana Nuevo Cedi)", "GHS (Ghana New Cedi)" },
                    { 57, "GIP (Gibraltar Libra)", "GIP (Gibraltar Pound)" },
                    { 58, "GMD (Gambia Dalasi)", "GMD (Gambia Dalasi)" },
                    { 59, "GNF (Guinea Franco)", "GNF (Guinea Franc)" },
                    { 60, "GRD (EURO) (Grecia Dracma)", "GRD (EURO) (Greece Drachma)" },
                    { 61, "GTQ (Guatemala Quetzal)", "GTQ (Guatemala Quetzal)" },
                    { 62, "GYD (Guyana Dólar)", "GYD (Guyana Dollar)" },
                    { 63, "HKD (Hong Kong Dólar)", "HKD (Hong Kong Dollar)" },
                    { 64, "HNL (Honduras Lempira)", "HNL (Honduras Lempira)" },
                    { 65, "HRK (Croacia Kuna)", "HRK (Croatia Kuna)" },
                    { 66, "HTG (Haití Gourde)", "HTG (Haiti Gourde)" },
                    { 67, "HUF (Hungría Forinto)", "HUF (Hungary Forint)" },
                    { 68, "IDR (Indonesia Rupia)", "IDR (Indonesia Rupiah)" },
                    { 69, "IED (EURO) (Irlanda Libra)", "IED (EURO) (Ireland Pound)" },
                    { 70, "ILS (Israel Nuevo Séquel)", "ILS (Israel New Baku)" },
                    { 71, "INR (India Rupia)", "INR (India Rupee)" },
                    { 72, "IQD (Irak Dinar)", "IQD (Iraqi Dinar)" },
                    { 73, "IRR (Irán Rial)", "IRR (Iranian Rial)" },
                    { 74, "ISK (Islandia Corona)", "ISK (Iceland Krona)" },
                    { 75, "ITL (EURO) (Italia Lira)", "ITL (EURO) (Italy Lira)" },
                    { 76, "JMD (Jamaica Dólar)", "JMD (Jamaica Dollar)" },
                    { 77, "JOD (Jordania Dinar)", "JOD (Jordan Dinar)" },
                    { 78, "JPY (Japón Yen)", "JPY (Japan Yen)" },
                    { 79, "KES (Kenia Chelín)", "KES (Kenya Shilling)" },
                    { 80, "KGS (Kirguistán Som)", "KGS (Kyrgyzstan Som)" },
                    { 81, "KHR (Camboya Riel)", "KHR (Cambodian Riel)" },
                    { 82, "KMF (Comoras Franco)", "KMF (Comoros Franc)" },
                    { 83, "KPW (Corea del Norte Won)", "KPW (North Korea Won)" },
                    { 84, "KRW (Corea del Sur Won)", "KRW (South Korea Won)" },
                    { 85, "KWD (Kuwait Dinar)", "KWD (Kuwait Dinar)" },
                    { 86, "KYD (Islas Caimán Dólar)", "KYD (Cayman Islands Dollar)" },
                    { 87, "KZT (Kazajistán Tenge)", "KZT (Kazakhstan Tenge)" },
                    { 88, "LAK (Laos Kip)", "LAK (Lao Kip)" },
                    { 89, "LBP (Líbano Libra)", "LBP (Lebanon Pound)" },
                    { 90, "LKR (Sri Lanka Rupia)", "LKR (Sri Lanka Rupee)" },
                    { 91, "LRD (Liberia Dólar)", "LRD (Liberia Dollar)" },
                    { 92, "LSL (Lesotho Loti)", "LSL (Lesotho Loti)" },
                    { 93, "LTL (EURO) (Lituania Litas)", "LTL (EURO) (Lithuania Litas)" },
                    { 94, "LUF (EURO) (Luxemburgo Franco)", "LUF (EURO) (Luxembourg Franc)" },
                    { 95, "LVL (EURO) (Letonia Lats)", "LVL (EURO) (Latvia Lats)" },
                    { 96, "LYD (Libia Dinar)", "LYD (Libyan Dinar)" },
                    { 97, "MAD (Marruecos Dirham)", "MAD (Moroccan Dirham)" },
                    { 98, "MDL (Moldavia Leu)", "MDL (Moldova Leu)" },
                    { 99, "MGA (Madagascar Ariary)", "MGA (Madagascar Ariary)" },
                    { 100, "MKD (Macedonia Denar)", "MKD (Macedonia Denar)" },
                    { 101, "MMK (Myanmar Kyat)", "MMK (Myanmar Kyat)" },
                    { 102, "MNT (Mongolia Tugrik)", "MNT (Mongolia Tugrik)" },
                    { 103, "MOP (Macao Pataca)", "MOP (Macao Pataca)" },
                    { 104, "MRO (Mauritania Ouguiya)", "MRO (Mauritania Ouguiya)" },
                    { 105, "MTL (EURO) (Malta Lira)", "MTL (EURO) (Malta Lira)" },
                    { 106, "MUR (Mauricio Rupia)", "MUR (Mauritius Rupee)" },
                    { 107, "MVR (Maldivas Rufiyaa)", "MVR (Maldives Rufiyaa)" },
                    { 108, "MWK (Malawi Kwacha)", "MWK (Malawi Kwacha)" },
                    { 109, "MXN (Peso mexicano)", "MXN (Mexican Peso)" },
                    { 110, "MYR (Malasia Ringgit)", "MYR (Malaysia Ringgit)" },
                    { 111, "MZN (Mozambique Nuevo Metical)", "MZN (Mozambique New Metical)" },
                    { 112, "NAD (Namibia Dólar)", "NAD (Namibia Dollar)" },
                    { 113, "NGN (Nigeria Naira)", "NGN (Nigeria Naira)" },
                    { 114, "NIO (Nicaragua Córdoba)", "NIO (Nicaragua Cordoba)" },
                    { 115, "NLG (EURO) (Países bajos Florín)", "NLG (EURO) (Netherlands Florin)" },
                    { 116, "NOK (Noruega Corona)", "NOK (Norwegian Krone)" },
                    { 117, "NPR (Nepal Rupia)", "NPR (Nepal Rupee)" },
                    { 118, "NZD (Nueva Zelanda Dólar)", "NZD (New Zealand Dollar)" },
                    { 119, "OMR (Omán Rial)", "OMR (Oman Rial)" },
                    { 120, "PAB (Panamá Balboa)", "PAB (Panama Balboa)" },
                    { 121, "PEN (Perú Nuevo Sol)", "PEN (Peru Nuevo Sol)" },
                    { 122, "PGK (Papúa Nueva Guinea Kina)", "PGK (Papua New Guinea Kina)" },
                    { 123, "PHP (Peso filipino)", "PHP (Philippines Peso)" },
                    { 124, "PKR (Pakistán Rupia)", "PKR (Pakistan Rupee)" },
                    { 125, "PLN (Polonia Zloty)", "PLN (Poland Zloty)" },
                    { 126, "PTE (EURO) (Portugal Escudo)", "PTE (EURO) (Portugal Escudo)" },
                    { 127, "PYG (Paraguay Guaraní)", "PYG (Paraguay Guarani)" },
                    { 128, "QAR (Catar Rial)", "QAR (Qatar Rial)" },
                    { 129, "RON (Rumania Nuevo Lei)", "RON (Romania New Lei)" },
                    { 130, "RSD (Serbia Dinar)", "RSD (Serbia Dinar)" },
                    { 131, "RUB (Rusia Rublo)", "RUB (Russian Rouble)" },
                    { 132, "RWF (Ruanda Franco)", "RWF (Rwanda Franc)" },
                    { 133, "SAR (Arabia Saudita Rial)", "SAR (Saudi Arabian Rial)" },
                    { 134, "SBD (Islas Salomón Dólar)", "SBD (Solomon Islands Dollar)" },
                    { 135, "SCR (Seychelles Rupia)", "SCR (Seychelles Rupee)" },
                    { 136, "SDG (Sudán Libra)", "SDG (Sudan Pound)" },
                    { 137, "SEK (Suecia Corona)", "SEK (Swedish Krona)" },
                    { 138, "SGD (Singapur Dólar)", "SGD (Singapore Dollar)" },
                    { 139, "SHP (Santa Helena Libra)", "SHP (Saint Helena Pound)" },
                    { 140, "SIT (EURO) (Eslovenia Tolar)", "SIT (EURO) (Slovenia Tolar)" },
                    { 141, "SKK (EURO) (Eslovaquia Koruna)", "SKK (EURO) (Slovakia Koruna)" },
                    { 142, "SLL (Sierra Leona Leone)", "SLL (Sierra Leone Leone)" },
                    { 143, "SOS (Somalía Chelín)", "SOS (Somali Shilling)" },
                    { 144, "SRD (Suriname Dólar)", "SRD (Suriname Dollar)" },
                    { 145, "STD (Santo Tomé y Príncipe Dobra)", "STD (Sao Tome & Principe Dobra)" },
                    { 146, "SVC (El Salvador Colón)", "SVC (El Salvador Colon)" },
                    { 147, "SYP (Siria Libra)", "SYP (Syria Pound)" },
                    { 148, "SZL (Suazilandia Lilangeni)", "SZL (Swaziland Lilangeni)" },
                    { 149, "THB (Tailandia Baht)", "THB (Thailand Baht)" },
                    { 150, "TMM (Turkmenistán Manat)", "TMM (Turkmenistan Manat)" },
                    { 151, "TND (Túnez Dinar)", "TND (Tunisia Dinar)" },
                    { 152, "TOP (Tonga Pa'anga)", "TOP (Tonga Pa'anga)" },
                    { 153, "TRY (Turquía Nueva Lira)", "TRY (Turkey New Lira)" },
                    { 154, "TTD (Trinidad y Tobago Dólar)", "TTD (Trinidad & Tobago Dollar)" },
                    { 155, "TWD (Taiwán Dólar)", "TWD (Taiwan Dollar)" },
                    { 156, "TZS (Tanzania Chelín)", "TZS (Tanzania Shilling)" },
                    { 157, "UAH (Ucrania Hryvnia)", "UAH (Ukraine Hryvnia)" },
                    { 158, "UGX (Uganda Chelín)", "UGX (Uganda Shilling)" },
                    { 159, "USD (EE.UU. Dólar)", "USD (U.S. Dollar)" },
                    { 160, "UYU (Peso uruguayo)", "UYU (Uruguayan Peso)" },
                    { 161, "VEB (Venezuela Bolívar)", "VEB (Venezuela Bolivar)" },
                    { 162, "VND (Vietnam Dong)", "VND (Vietnam Dong)" },
                    { 163, "VUV (Vanuatu Vatu)", "VUV (Vanuatu Vatu)" },
                    { 164, "WST (Samoa Tala)", "WST (Samoa Tala)" },
                    { 165, "XAF (CFA Franco BEAC)", "XAF (CFA Franc BEAC)" },
                    { 166, "XCD (Caribe oriental Dólar)", "XCD (Eastern Caribbean Dollar)" },
                    { 167, "XOF (CFA Franco BCEAO)", "XOF (CFA Franc BCEAO)" },
                    { 168, "XPF (CFP Franco)", "XPF (CFA Franc Franc CFP)" },
                    { 169, "YER (Yemen Rial)", "YER (Yemen Rial)" },
                    { 170, "ZAR (Sudáfrica Rand)", "ZAR (South African Rand)" },
                    { 171, "ZMK (Zambia Kwacha)", "ZMK (Zambia Kwacha)" },
                    { 172, "ZWD (Zimbabue Dólar)", "ZWD (Zimbabwe Dollar)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultContractTypes");

            migrationBuilder.DropTable(
                name: "DefaultCurrencyTypes");

            migrationBuilder.DropColumn(
                name: "DefaultContractTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "DefaultCurrencyTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "CollaboratorContracts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
