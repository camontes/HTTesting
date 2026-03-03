using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLifePreferencesTables2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DefaultLifePreferences",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 2, "Actuar", "Act" },
                    { 3, "Acuarios", "Aquariums" },
                    { 4, "Aerobic", "Aerobic" },
                    { 5, "Aeromodelismo", "Aeromodelling" },
                    { 6, "Aerostación", "Aerostation" },
                    { 7, "Aikido", "Aikido" },
                    { 8, "Airsoft", "Airsoft" },
                    { 9, "Ajedrez", "Chess" },
                    { 10, "Animación", "Animation" },
                    { 11, "Aquagym", "Aquagym" },
                    { 12, "Aromaterapia", "Aromatherapy" },
                    { 13, "Arte digital", "Digital art" },
                    { 14, "Arte", "Art" },
                    { 15, "Asociaciones", "Associations" },
                    { 16, "Astrología", "Astrology" },
                    { 17, "Astronomía", "Astronomy" },
                    { 18, "Atletismo", "Athletics" },
                    { 19, "Audiolibros", "Audiobooks" },
                    { 20, "Autocaravanas", "Motorhomes" },
                    { 21, "Automovilismo", "Motoring" },
                    { 22, "Aviación deportiva", "Sport Aviation" },
                    { 23, "Avicultura", "Aviculture" },
                    { 24, "Avistamiento de aves", "Bird watching" },
                    { 25, "Backgammon", "Backgammon" },
                    { 26, "Badminton", "Badminton" },
                    { 27, "Bailar", "Dancing" },
                    { 28, "Baloncesto", "Basketball" },
                    { 29, "Balonmano", "Handball" },
                    { 30, "Banda o grupo musicales", "Band or musical group" },
                    { 31, "Barcos de motor", "Motor boats" },
                    { 32, "Barranquismo", "Canyoning" },
                    { 33, "Batik y estampación de tejidos", "Batik and fabric printing" },
                    { 34, "Beisbol", "Baseball" },
                    { 35, "Belleza y estética", "Beauty and esthetics" },
                    { 36, "Biatlon", "Biathlon" },
                    { 37, "Bibliofilia", "Bibliophilia" },
                    { 38, "Bicicleta(ciclismo)", "Bicycle(cycling)" },
                    { 39, "Bicicleta de montaña", "Mountain biking" },
                    { 40, "Bikejoring", "Bikejoring" },
                    { 41, "Billar", "Billiards" },
                    { 42, "Bingo", "Bingo" },
                    { 43, "Blog(y videoblog)", "Blog(and videoblog)" },
                    { 44, "BMX", "BMX" },
                    { 45, "Bodyboard", "Bodyboard" },
                    { 46, "Bolos(bowling, boliche)", "Bowling" },
                    { 47, "Bonsai(arboles enanos)", "Bonsai(dwarf trees)" },
                    { 48, "Bordado sobre papel", "Embroidery on paper" },
                    { 49, "Boxeo", "Boxing" },
                    { 50, "Bricolaje o diy", "DIY" },
                    { 51, "Buceo", "Diving" },
                    { 52, "Caballo(equitación)", "Horse(horseback riding)" },
                    { 53, "Caligrafía y lettering", "Calligraphy and lettering" },
                    { 54, "Campismo", "Camping" },
                    { 55, "Canaricultura", "Dog breeding" },
                    { 56, "Canicross", "Canicross" },
                    { 57, "Cantar", "Singing" },
                    { 58, "Capoeira", "Capoeira" },
                    { 59, "Carpintería", "Carpentry" },
                    { 60, "Carreras por montaña", "Mountain racing" },
                    { 61, "Carrovela y blokart", "Carrovela and blokart" },
                    { 62, "Casas de muñecas", "Doll houses" },
                    { 63, "Cata de cerveza", "Beer tasting" },
                    { 64, "Cata de vinos", "Wine tasting" },
                    { 65, "Caza(deportiva)", "Hunting(sport)" },
                    { 66, "Cerámica", "Ceramics" },
                    { 67, "Cerveza(fabricación)", "Beer(brewing)" },
                    { 68, "Cestería", "Basketry" },
                    { 69, "Cetrería", "Falconry" },
                    { 70, "Chalkpaint(pintura de tiza)", "Chalkpaint(chalk painting)" },
                    { 71, "Chi kung o qui gong", "Chi kung or qui gong" },
                    { 72, "Cianotipia", "Cyanotype" },
                    { 73, "Cicloturismo", "Cycling" },
                    { 74, "Cine", "Cinema" },
                    { 75, "Cocina", "Cooking" },
                    { 76, "Coleccionismo(antigüedades)", "Collecting(antiques)" },
                    { 77, "Colorear libros", "Coloring books" },
                    { 78, "Cometas(volar cometas)", "Kites(kite flying)" },
                    { 79, "Comics(creación de)", "Comics(creating)" },
                    { 80, "Comics(lectura de)", "Comics(reading)" },
                    { 81, "Comidista(foodie)", "Foodie" },
                    { 82, "Composición musical", "Musical composition" },
                    { 83, "Compostaje", "Composting" },
                    { 84, "Comprar", "Shopping" },
                    { 85, "Conducción de automóviles", "Automobile driving" },
                    { 86, "Conducción de motocicletas", "Motorcycle driving" },
                    { 87, "Conferencias(asistir a)", "Lectures(attend)" },
                    { 88, "Coro", "Choir" },
                    { 89, "Correr(running)", "Running" },
                    { 90, "Cosmética", "Cosmetics" },
                    { 91, "Costura, corte y confección", "Sewing, cutting and tailoring" },
                    { 92, "Cristal teñido", "Stained glass" },
                    { 93, "Croquet", "Croquet" },
                    { 94, "Cruceros(viajes de)", "Cruising(travel)" },
                    { 95, "Cubo de rubik(rompecabezas)", "Rubik's cube (puzzles)" },
                    { 96, "Cuero(creaciones con)", "Leather(creations with)" },
                    { 97, "Culturismo(body building)", "Bodybuilding(body building)" },
                    { 98, "Curling", "Curling" },
                    { 99, "Customización de ropa", "Clothing customization" },
                    { 100, "Customización y restauración de bicicletas", "Customization and restoration of bicycles" },
                    { 101, "Damas(juego de)", "Checkers(checkers game)" },
                    { 102, "Danza aérea", "Aerial dance" },
                    { 103, "Dardos(lanzar dardos)", "Darts(throwing darts)" },
                    { 104, "Decoración de interiores", "Interior decoration" },
                    { 105, "Decoupage(decoración de superficies)", "Decoupage(surface decoration)" },
                    { 106, "Deporte(asistir a espectáculos deportivos)", "Sport(attending sporting events)" },
                    { 107, "Deporte(ver deporte)", "Sports(watching sports)" },
                    { 108, "Deportes de fantasía(liga de fantasía)", "Fantasy sports(fantasy league)" },
                    { 109, "Diario(escribir un)", "Journaling(writing a diary)" },
                    { 110, "Dibujo artístico", "Artistic drawing" },
                    { 111, "Dioramas(y belenes)", "Dioramas(and nativity scenes)" },
                    { 112, "Dirigir, entrenar, gestionar.", "Directing, coaching, managing" },
                    { 113, "Disc golf", "Disc golf" },
                    { 114, "Diseño de ropa(moda)", "Clothing design(fashion)" },
                    { 115, "Diseño y creación de páginas web", "Design and creation of web pages" },
                    { 116, "Dj(disk jockey)", "Dj(disc jockey)" },
                    { 117, "Documentales(afición a los)", "Documentaries(hobby)" },
                    { 118, "Domino", "Domino" },
                    { 119, "Electrónica", "Electronics" },
                    { 120, "Encuadernación de libros", "Book binding" },
                    { 121, "Enmarcar cuadros", "Picture framing" },
                    { 122, "Escalada", "Climbing" },
                    { 123, "Escribir literatura", "Writing literature" },
                    { 124, "Escultura", "Sculpting" },
                    { 125, "Esgrima", "Fencing" },
                    { 126, "Esmaltes sobre metal(al fuego)", "Enamels on metal(on fire)" },
                    { 127, "Espeleología", "Speleology" },
                    { 128, "Esports", "Esports" },
                    { 129, "Esquí alpino", "Alpine skiing" },
                    { 130, "Esquí de fondo o nórdico", "Cross-country or Nordic skiing" },
                    { 131, "Esquí náutico o acuático", "Water skiing or water skiing" },
                    { 132, "Estudiar", "Study" },
                    { 133, "Exploración urbana(urbex)", "Urban exploration(urbex)" },
                    { 134, "Flamenco", "Flamenco" },
                    { 135, "Floorball(o unihockey)", "Floorball(or unihockey)" },
                    { 136, "Flores kanzashi", "Kanzashi flowers" },
                    { 137, "Flores secas(trabajar con)", "Dried flowers(work with)" },
                    { 138, "Flyboard", "Flyboard" },
                    { 139, "Fotografía", "Photography" },
                    { 140, "Frisbee(disco volador)", "Frisbee(flying disc)" },
                    { 141, "Frontón", "Fronton" },
                    { 142, "Futbol", "Soccer" },
                    { 143, "Futbol americano", "Football" },
                    { 144, "Futbol sala", "Indoor soccer" },
                    { 145, "Futbolín", "Foosball" },
                    { 146, "Genealogía e historia familiar", "Genealogy and family history" },
                    { 147, "Geocaching y búsqueda de tesoros", "Geocaching and treasure hunting" },
                    { 148, "Gimnasia de mantenimiento y fitness", "Gymnastics and fitness" },
                    { 149, "Globos(trabajo con globos)", "Balloons(balloon work)" },
                    { 150, "Go(juego)", "Go(game)" },
                    { 151, "Golf", "Golf" },
                    { 152, "Grabados artísticos", "Artistic engravings" },
                    { 153, "Graffiti y arte urbano", "Graffiti and street art" },
                    { 154, "Groundhopping(«ir de estadio en estadio»)", "Groundhopping('going from stadium to stadium')" },
                    { 155, "Hacer vino", "Making wine" },
                    { 156, "Halterofilia(levantamiento de pesas)", "Weightlifting" },
                    { 157, "Hapkido", "Hapkido" },
                    { 158, "Hidroponía(cultivo en liquido)", "Hydroponics(liquid farming)" },
                    { 159, "Hockey de mesa(air hockey)", "Table field hockey(air field hockey)" },
                    { 160, "Hockey sobre hielo", "Ice hockey" },
                    { 161, "Hockey sobre hierba", "Field Hockey" },
                    { 162, "Hockey sobre patines", "Roller Hockey" },
                    { 163, "Hockey subacuático", "Underwater field hockey" },
                    { 164, "Horticultura(agricultura)", "Horticulture(agriculture)" },
                    { 165, "Huerto casero", "Home gardening" },
                    { 166, "Hydrospeed", "Hydrospeed" },
                    { 167, "Ikebana(arte floral)", "Ikebana(floral art)" },
                    { 168, "Internet", "Internet" },
                    { 169, "Invertir en bolsa", "Investing in the stock market" },
                    { 170, "Jabón(hacer)", "Soap(making)" },
                    { 171, "Jardinería", "Gardening" },
                    { 172, "Jiu jitsu", "Jiu jitsu" },
                    { 173, "Joyas y bisutería(creación de)", "Jewelry and costume jewelry(making)" },
                    { 174, "Judo", "Judo" },
                    { 175, "Juegos con dispositivos electrónicos", "Games with electronic devices" },
                    { 176, "Juegos de cartas o naipes", "Card games" },
                    { 177, "Juegos de mesa", "Board games" },
                    { 178, "Juegos de mesa temáticos", "Themed board games" },
                    { 179, "Juegos de rol", "Role - playing games" },
                    { 180, "Jugger", "Jugger" },
                    { 181, "Karaoke", "Karaoke" },
                    { 182, "Karate", "Karate" },
                    { 183, "Kayak polo", "Kayak polo" },
                    { 184, "Kendo", "Kendo" },
                    { 185, "Kenpo", "Kenpo" },
                    { 186, "Kick boxing", "Kick boxing" },
                    { 187, "Kintsugi", "Kintsugi" },
                    { 188, "Kitesurf", "Kitesurfing" },
                    { 189, "Kokedamas(cultivo de plantas)", "Kokedamas(plant cultivation)" },
                    { 190, "Korfball", "Korfball" },
                    { 191, "Kung fu", "Kung fu" },
                    { 192, "Labores(punto, bordado, encaje, etc)", "Needlework(knitting, embroidery, lace, etc.)" },
                    { 193, "Lectura", "Reading" },
                    { 194, "Lego", "Lego" },
                    { 195, "Lotería", "Lottery" },
                    { 196, "Lucha(olímpica, grecorromana)", "Wrestling(Olympic, Greco - Roman)" },
                    { 197, "Madera(tallas en madera)", "Wood(wood carving)" },
                    { 198, "Magia(ilusionismo, prestidigitación)", "Magic(illusionism, conjuring)" },
                    { 199, "Mahjong(juego de fichas chino)", "Mahjong(Chinese tile game)" },
                    { 200, "Malabares", "Juggling" },
                    { 201, "Manualidades", "Handicrafts" },
                    { 202, "Marcha o paseo nórdico(nordic walking)", "Nordic walking(Nordic walking)" },
                    { 203, "Marionetas(teatro de)", "Puppetry(puppet theater)" },
                    { 204, "Meditación", "Meditation" },
                    { 205, "Mercadillos / flea markets", "Flea markets" },
                    { 206, "Mermeladas(preparar)", "Jams(preparing)" },
                    { 207, "Metales(búsqueda de)", "Metals(search for)" },
                    { 208, "Mindfulness o atención plena", "Mindfulness" },
                    { 209, "Minerales(búsqueda de)", "Minerals(search for)" },
                    { 210, "Moda(afición a la moda)", "Fashion(fashion hobby)" },
                    { 211, "Modelismo(aviones, coches, barcos, drones)", "Modeling(airplanes, cars, boats, drones)" },
                    { 212, "Modelismo con cerillas", "Match modeling" },
                    { 213, "Montañismo / alpinismo", "Mountaineering / alpinism" },
                    { 214, "Moto acuática", "Jet skiing" },
                    { 215, "Muñecas(hacer)", "Dolls(doll making)" },
                    { 216, "Mushing", "Mushing" },
                    { 217, "Música(afición a la)", "Music(hobby)" },
                    { 218, "Nadar(natación)", "Swimming" },
                    { 219, "Observación de trenes y aviones", "Train and airplane observation" },
                    { 220, "Orientación", "Orientation" },
                    { 221, "Origami(o papiroflexia)", "Origami" },
                    { 222, "Padbol", "Paddleball" },
                    { 223, "Paddle surf(surf a remo)", "Paddle surfing" },
                    { 224, "Padel", "Paddle boarding" },
                    { 225, "Paintball", "Paintball" },
                    { 226, "Palomas(cría de palomas)", "Pigeons(pigeon breeding)" },
                    { 227, "Papel mache", "Paper mache" },
                    { 228, "Paracaidismo", "Parachuting" },
                    { 229, "Paramotor", "Paramotoring" },
                    { 230, "Parapente", "Paragliding" },
                    { 231, "Parkour(freerunning)", "Parkour(freerunning)" },
                    { 232, "Pasatiempos(crucigramas, sudokus)", "Hobbies(crossword puzzles, sudoku)" },
                    { 233, "Pasear", "Walking" },
                    { 234, "Patchwork y colchas", "Patchwork and quilting" },
                    { 235, "Patinaje sobre hielo", "Ice skating" },
                    { 236, "Patinaje sobre ruedas(roller)", "Roller skating(roller skating)" },
                    { 237, "Perros(concursos / exhibiciones)", "Dogs(contests / exhibitions)" },
                    { 238, "Pesca", "Fishing" },
                    { 239, "Pesca submarina", "Underwater fishing" },
                    { 240, "Petanca", "Petanque" },
                    { 241, "Pilates", "Pilates" },
                    { 242, "Ping pong o tenis de mesa", "Ping pong or table tennis" },
                    { 243, "Pintura artística", "Artistic painting" },
                    { 244, "Pintura de figuras", "Figure painting" },
                    { 245, "Pintura sobre seda", "Silk painting" },
                    { 246, "Piragüismo(canoismo / canotaje)", "Canoeing(boating)" },
                    { 247, "Plantas de interior", "Indoor plants" },
                    { 248, "Plantas silvestres", "Wild plants" },
                    { 249, "Podcasts(afición / creación)", "Podcasts(hobby / creation)" },
                    { 250, "Polo", "Polo" },
                    { 251, "Porcelana fría", "Cold porcelain" },
                    { 252, "Powerlifting", "Powerlifting" },
                    { 253, "Producción musical", "Music production" },
                    { 254, "Programación informática", "Computer programming" },
                    { 255, "Puzzles", "Puzzles" },
                    { 256, "Quad y buggy", "Quad and buggy" },
                    { 257, "Radio(afición a la)", "Radio(hobby)" },
                    { 258, "Radioafición", "Amateur radio" },
                    { 259, "Rafting", "Rafting" },
                    { 260, "Raquetas de nieve", "Snowshoeing" },
                    { 261, "Reciclaje creativo", "Creative recycling" },
                    { 262, "Redes sociales", "Social networking" },
                    { 263, "Reiki(técnica espiritual sanadora)", "Reiki(spiritual healing technique)" },
                    { 264, "Remo deportivo", "Sports rowing" },
                    { 265, "Repostería / pastelería y cupcakes", "Baking / pastry and cupcakes" },
                    { 266, "Repujado de metal", "Metal working" },
                    { 267, "Restauración de coches clásicos", "Classic car restoration" },
                    { 268, "Restauración de muebles", "Furniture restoration" },
                    { 269, "Leer", "Read" },
                    { 270, "Robótica", "Robotics" },
                    { 271, "Roller derby", "Roller derby" },
                    { 272, "Rugby", "Rugby" },
                    { 273, "Scalextric", "Scalextric" },
                    { 274, "Scrapbook(album de recortes)", "Scrapbooking" },
                    { 275, "Senderismo", "Hiking" },
                    { 276, "Series de tv", "TV series" },
                    { 277, "Serigrafia", "Serigraphy" },
                    { 278, "Setas(búsqueda de)", "Mushrooms(mushroom hunting)" },
                    { 279, "Skateboarding", "Skateboarding" },
                    { 280, "Slime(jugar con slime)", "Slime(playing with slime)" },
                    { 281, "Snowbike", "Snowbike" },
                    { 282, "Snowboard", "Snowboard" },
                    { 283, "Softball", "Softball" },
                    { 284, "Speedriding", "Speedriding" },
                    { 285, "Spinning", "Spinning" },
                    { 286, "Squash", "Squash" },
                    { 287, "Surf", "Surf" },
                    { 288, "Taekwondo", "Taekwondo" },
                    { 289, "Taichi", "Taichi" },
                    { 290, "Tampones o sellos de caucho", "Rubber stamps or stamps" },
                    { 291, "Tarot", "Tarot" },
                    { 292, "Tatuajes", "Tattoos" },
                    { 293, "Taxidermia", "Taxidermy" },
                    { 294, "Tejer", "Knit" },
                    { 295, "Tejo", "Shuffleboard" },
                    { 296, "Tenis", "Tennis" },
                    { 297, "Tintado / teñido de tejidos", "Dyeing / dyeing of fabrics" },
                    { 298, "Tiro con arco", "Archery" },
                    { 299, "Tiro con tirachinas", "Slingshot shooting" },
                    { 300, "Tiro deportivo con arma de fuego", "Shooting sports with a firearm" },
                    { 301, "Tocar un instrumento musical", "Play a musical instrument" },
                    { 302, "Toros(afición a los)", "Bullfighting(bullfighting hobby)" },
                    { 303, "Tostar café", "Roast coffee" },
                    { 304, "Transferencia de imágenes", "Image transfer" },
                    { 305, "Trenes a escala", "Scale trains" },
                    { 306, "Triatlon", "Triathlon" },
                    { 307, "Uñas(decoración)", "Nails(decoration)" },
                    { 308, "Vehículos de control remoto(rc)", "Remote control vehicles(rc)" },
                    { 309, "Vela deportiva y recreativa", "Sailing sports and recreation" },
                    { 310, "Velas(creación de velas)", "Candles(making candles)" },
                    { 311, "Viajar", "Travel" },
                    { 312, "Viajar lentamente", "Travel slowly" },
                    { 313, "Viajes de aventura", "Adventure travel" },
                    { 314, "Viajes temáticos", "Themed trips" },
                    { 315, "Video(realización de videos)", "Video(making videos)" },
                    { 316, "Videojuegos", "Video games" },
                    { 317, "Visitar monumentos", "Visit monuments" },
                    { 318, "Visitar museos y exposiciones", "Visit museums and exhibitions" },
                    { 319, "Visitas urbanas", "Urban visits" },
                    { 320, "Voleibol", "Volleyball" },
                    { 321, "Voluntariado ambiental", "Environmental volunteering" },
                    { 322, "Voluntariado animal", "Animal volunteering" },
                    { 323, "Voluntariado cultural", "Cultural volunteering" },
                    { 324, "Voluntariado en deporte", "Sports volunteering" },
                    { 325, "Voluntariado social", "Social volunteering" },
                    { 326, "Vuelo a vela", "Gliding" },
                    { 327, "Wakeboard", "Wakeboarding" },
                    { 328, "Waterpolo", "Water polo" },
                    { 329, "Windsurf", "Windsurfing" },
                    { 330, "Yoga", "Yoga" },
                    { 331, "Youtuber", "Youtuber" },
                    { 332, "Zumba", "Zumba" },
                    { 333, "Otra", "Other" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "DefaultLifePreferences",
                keyColumn: "Id",
                keyValue: 333);
        }
    }
}
