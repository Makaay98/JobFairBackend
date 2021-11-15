using System;
using System.Collections.Generic;
using System.Linq;

namespace TestBackEnd
{
    class Program
    {
        static string[] clubNameArray = { "MCU", "MU", "ARS", "INT", "JUV", "CZV", "PAR", "SUM", "RM", "BM" };
        static int skillGapLimiterValue = 10; 
        static void Main(string[] args)
        {
            Console.WriteLine($"Skill gap: {skillGapLimiterValue} \n");
            Console.WriteLine($"Clubs:");
            List<Club> clubs = new List<Club>();

            clubs.Add(new Club("MCU",30));
            clubs.Add(new Club("MU",20));
            clubs.Add(new Club("BAR",18));
            clubs.Add(new Club("ARS",22));
            clubs.Add(new Club("RM",23));
            clubs.Add(new Club("JUV",30));
            clubs.Add(new Club("BM",21));
            clubs.Add(new Club("INT",27));

            clubs.Add(new Club("AJX",19));

            PrintNonPairedClubs(clubs);
            Console.WriteLine("Press SPACE to pair clubs!!!\n");
            Console.ReadKey();

            Tuple<List<Tuple<Club, Club>>, List<Club>> result = SkillBasedMatchMaking(clubs, skillGapLimiterValue);

            PrintPairedClubs(result.Item1);
            PrintNonPairedClubs(result.Item2);

            clubs = new List<Club>();
            result.Item2.ForEach((Club club) =>
            {
                clubs.Add(club);
            });
            Console.WriteLine("\n");
            Console.WriteLine("Awaiting new players...\n");
            Console.WriteLine("Press SPACE to connect player!!!\n");

            while (true)
            {
                Console.ReadKey();
                Club club = new Club(ClubNameRandomiser(), new Random().Next(18, 31));
                clubs.Add(club);
                Console.WriteLine($"New player connected! Club name: {club.Name}, Rating: {club.Rating}");

                result = SkillBasedMatchMaking(clubs, skillGapLimiterValue);

                PrintPairedClubs(result.Item1);
                Console.WriteLine("\n");
                PrintNonPairedClubs(result.Item2);
                Console.WriteLine("\n");


                clubs = new List<Club>();
                result.Item2.ForEach((Club club) =>
                {
                    clubs.Add(club);
                });
            }

        }

        static Tuple<List<Tuple<Club,Club>>, List<Club>> SkillBasedMatchMaking(List<Club> clubs, int skillGapLimiter)// "The Function"
        {
            List<Tuple<Club, Club>> pairedClubs = new List<Tuple<Club, Club>>();
            List<Club> nonPairedClubs = new List<Club>();

            clubs = clubs.OrderByDescending(club => club.Rating).ToList();

            if (!(clubs.Count % 2 == 0))
            {
                nonPairedClubs.Add(clubs[clubs.Count - 1]);
                clubs.RemoveAt(clubs.Count - 1);
            }

            for (int i = 0; i < clubs.Count; i += 2)
            {
                if (clubs[i].Rating < clubs[i + 1].Rating + skillGapLimiter && // check if clubs pair are in set skill gap
                    clubs[i + 1].Rating < clubs[i].Rating + skillGapLimiter)
                {
                    pairedClubs.Add(new Tuple<Club, Club>(clubs[i], clubs[i + 1]));
                }
                else
                {
                    nonPairedClubs.Add(clubs[i]);
                    nonPairedClubs.Add(clubs[i + 1]);
                }

            }

            return new Tuple<List<Tuple<Club, Club>>, List<Club>>(pairedClubs, nonPairedClubs);
        }
        static void PrintPairedClubs(List<Tuple<Club, Club>> clubs)
        {
            clubs.ForEach((Tuple<Club, Club> pair) =>
            {
                Console.WriteLine("Match starting:"+pair.Item1.Name + ": " + pair.Item1.Rating + " VS " + pair.Item2.Name + ": " + pair.Item2.Rating);
            });
        }
        static void PrintNonPairedClubs(List<Club> clubs)
        {
            clubs.ForEach((Club club) =>
            {
                Console.WriteLine("No pair: " + club.Name + " Rating: "+ club.Rating);
            });
        }
        static string ClubNameRandomiser()
        {
            int rnd = new Random().Next(0, clubNameArray.Count() - 1);
            return clubNameArray[rnd];
        }
    }

    class Club
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public List<Player> Team { get; set; }

        public Club(string name, int numberOfPlayers)
        {
            this.Team = new List<Player>();

            this.Name = name;
            this.FillTeamWithPlayers(numberOfPlayers);
            this.CalculateClubRating();
            
        }
        private void FillTeamWithPlayers(int numberOfPlayers)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                this.Team.Add(new Player());
            }
        }
        private void CalculateClubRating()
        {
            int sum = 0;
            this.Team.ForEach((Player player) =>
            {
                sum += player.Rating;
            });
            this.Rating = sum / Team.Count;
        }
    }
    class Player
    {
        public int Rating { get; set; }

        public Player()
        {
            this.CalculatePlayerRating();
        }
        private void CalculatePlayerRating()
        {
            this.Rating = new Random().Next(0, 101);
        }
    }
}
