using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecosystemsimulation
{

    class Rabbit
    {
        public bool Haseaten   // property
        {
            get; set;
        }
        public Rabbit(bool hasalreadyeaten)
        {
            Haseaten = hasalreadyeaten;
        }
    }
    class Program
    {
        public static bool GenerateRandombool(int precentage)
        {
            var rand = new Random();

            if (rand.Next(1000) < precentage * 10)
            {
                return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Ecosystem Simulation");
            Console.WriteLine("---Enter the amount of prey---");
            int RabbitsAmount = Int32.Parse(Console.ReadLine());
            List<Rabbit> rabbits = new List<Rabbit>();
            for (int i = 0; i < RabbitsAmount; i++)
            {
                rabbits.Add(new Rabbit(false));
            }
            
            Console.WriteLine("---Enter the amount of avialable crops per day---");
            int Foodavailable = Int32.Parse(Console.ReadLine());
            Console.WriteLine("---Write the amount of days to siumlate---");
            int Daysamount = Int32.Parse(Console.ReadLine());
            Console.WriteLine("---Write the probabilty of repuduction---");
            int reproductionperc = Int32.Parse(Console.ReadLine());
            int dead = 0;
            Console.WriteLine("------------------Starting Simulation------------------");
            int Foodavailablefortheday = Foodavailable;
            int deathstoday = 0;
            for (int i = 0; i < Daysamount; i++)
            {
                foreach (Rabbit obj in rabbits.ToList())
                {
                    if (!obj.Haseaten)
                    {
                        if (Foodavailablefortheday > 0)
                        {
                            obj.Haseaten = true;
                            Foodavailablefortheday = Foodavailablefortheday - 1;
                        }
                    }
                }

                /*if (GenerateRandombool(reproductionperc))
                {
                    rabbits.Add(new Rabbit(true));
                }*/
                foreach (Rabbit obj in rabbits.ToList())
                {
                    if (!obj.Haseaten)
                    {
                        dead = dead + 1;
                        deathstoday = deathstoday + 1;
                        rabbits.Remove(obj);
                    }
                }
                if (rabbits.Count % 2 == 0 && rabbits.Count != 0 && rabbits.Count != 1)
                {
                    int Rabbitcountrepu = rabbits.Count / 2;
                    for (int u = 0; u != Rabbitcountrepu; u++)
                    {
                        rabbits.Add(new Rabbit(true));
                    }
                }
                else if (rabbits.Count >= 3)
                {
                    int Rabbitcountrepu = rabbits.Count - 1;
                    Rabbitcountrepu = reproductionperc / 2;
                    for (int u = 0; u != Rabbitcountrepu; u++)
                    {
                        rabbits.Add(new Rabbit(true));
                    }
                }
                foreach (Rabbit obj in rabbits.ToList())
                {
                    obj.Haseaten = false;
                }
                Console.WriteLine($"Deaths total day {i}:{deathstoday}");
                Console.WriteLine($"Alive total day {i}:{rabbits.Count}");
                Console.WriteLine("------------------");
                Foodavailablefortheday = Foodavailable;
                deathstoday = 0;
            }
            int alive = 0;
            foreach (Rabbit obj in rabbits.ToList())
            {
                alive = alive + 1;
            }
            Console.WriteLine("------------------Simulation over------------------");
            Console.WriteLine("We ended up with " + dead + " casualt(ies)(y)");
            Console.WriteLine("We ended up with " + alive + " Still alive");
            Console.WriteLine($"Amount of creatures who lived once { alive + dead}");

        }
    }
}
