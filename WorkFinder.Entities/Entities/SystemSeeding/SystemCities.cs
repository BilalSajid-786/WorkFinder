using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public static class SystemCities
    {
        public static readonly List<(int CityId, string CityName, int CountryId)> Cities = new()
        {
            (1, "New York", 1),
            (2, "Los Angeles", 1),
            (3, "Chicago", 1),

            (4, "London", 2),
            (5, "Manchester", 2),
            (6, "Birmingham", 2),

            (7, "Toronto", 3),
            (8, "Vancouver", 3),
            (9, "Montreal", 3),

            (10, "Sydney", 4),
            (11, "Melbourne", 4),
            (12, "Brisbane", 4),

            (13, "Berlin", 5),
            (14, "Munich", 5),
            (15, "Hamburg", 5),

            (16, "Tirana", 6),
            (17, "Durres", 6),
            (18, "Shkoder", 6),

            (19, "Andorra la Vella", 7),
            (20, "Escaldes-Engordany", 7),

            (21, "Vienna", 8),
            (22, "Graz", 8),
            (23, "Linz", 8),

            (24, "Minsk", 9),
            (25, "Gomel", 9),
            (26, "Brest", 9),

            (27, "Brussels", 10),
            (28, "Antwerp", 10),
            (29, "Ghent", 10),

            (30, "Sarajevo", 11),
            (31, "Banja Luka", 11),
            (32, "Mostar", 11),

            (33, "Sofia", 12),
            (34, "Plovdiv", 12),
            (35, "Varna", 12),

            (36, "Zagreb", 13),
            (37, "Split", 13),
            (38, "Rijeka", 13),

            (39, "Nicosia", 14),
            (40, "Limassol", 14),
            (41, "Larnaca", 14),

            (42, "Prague", 15),
            (43, "Brno", 15),
            (44, "Ostrava", 15),

            (45, "Copenhagen", 16),
            (46, "Aarhus", 16),
            (47, "Odense", 16),

            (48, "Tallinn", 17),
            (49, "Tartu", 17),
            (50, "Narva", 17),

            (51, "Helsinki", 18),
            (52, "Espoo", 18),
            (53, "Tampere", 18),

            (54, "Paris", 19),
            (55, "Lyon", 19),
            (56, "Marseille", 19),

            (57, "Athens", 20),
            (58, "Thessaloniki", 20),
            (59, "Patras", 20),

            (60, "Budapest", 21),
            (61, "Debrecen", 21),
            (62, "Szeged", 21),

            (63, "Reykjavik", 22),
            (64, "Kopavogur", 22),
            (65, "Hafnarfjordur", 22),

            (66, "Dublin", 23),
            (67, "Cork", 23),
            (68, "Limerick", 23),

            (69, "Rome", 24),
            (70, "Milan", 24),
            (71, "Naples", 24),

            (72, "Riga", 25),
            (73, "Daugavpils", 25),
            (74, "Liepaja", 25),

            (75, "Vaduz", 26),
            (76, "Schaan", 26),

            (77, "Vilnius", 27),
            (78, "Kaunas", 27),
            (79, "Klaipeda", 27),

            (80, "Luxembourg City", 28),
            (81, "Esch-sur-Alzette", 28),
            (82, "Differdange", 28),

            (83, "Valletta", 29),
            (84, "Birkirkara", 29),
            (85, "Sliema", 29),

            (86, "Chisinau", 30),
            (87, "Balti", 30),
            (88, "Cahul", 30),

            (89, "Monaco", 31),
            (90, "Monte Carlo", 31),

            (91, "Podgorica", 32),
            (92, "Niksic", 32),
            (93, "Budva", 32),

            (94, "Amsterdam", 33),
            (95, "Rotterdam", 33),
            (96, "The Hague", 33),

            (97, "Skopje", 34),
            (98, "Bitola", 34),
            (99, "Kumanovo", 34),

            (100, "Oslo", 35),
            (101, "Bergen", 35),
            (102, "Trondheim", 35),

            (103, "Warsaw", 36),
            (104, "Krakow", 36),
            (105, "Wroclaw", 36),

            (106, "Lisbon", 37),
            (107, "Porto", 37),
            (108, "Coimbra", 37),

            (109, "Bucharest", 38),
            (110, "Cluj-Napoca", 38),
            (111, "Timisoara", 38),

            (112, "Moscow", 39),
            (113, "Saint Petersburg", 39),
            (114, "Novosibirsk", 39),

            (115, "San Marino", 40),
            (116, "Serravalle", 40),

            (117, "Belgrade", 41),
            (118, "Novi Sad", 41),
            (119, "Nis", 41),

            (120, "Bratislava", 42),
            (121, "Kosice", 42),
            (122, "Presov", 42),

            (123, "Ljubljana", 43),
            (124, "Maribor", 43),
            (125, "Celje", 43),

            (126, "Madrid", 44),
            (127, "Barcelona", 44),
            (128, "Valencia", 44),

            (129, "Stockholm", 45),
            (130, "Gothenburg", 45),
            (131, "Malmo", 45),

            (132, "Zurich", 46),
            (133, "Geneva", 46),
            (134, "Basel", 46),

            (135, "Kyiv", 47),
            (136, "Kharkiv", 47),
            (137, "Odesa", 47),

            (138, "Vatican City", 48),

            (139, "Pristina", 49),
            (140, "Prizren", 49),
            (141, "Peja", 49),

            (142, "Tbilisi", 50),
            (143, "Batumi", 50),
            (144, "Kutaisi", 50),

            (145, "Yerevan", 51),
            (146, "Gyumri", 51),
            (147, "Vanadzor", 51),

            (148, "Baku", 52),
            (149, "Ganja", 52),
            (150, "Sumqayit", 52),

            (151, "Istanbul", 53),
            (152, "Ankara", 53),
            (153, "Izmir", 53)
        };
    }
}
