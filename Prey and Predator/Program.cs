using System;
using System.Collections.Generic;

namespace Prey_and_Predator
{
    class Program
    {
        //this here is an input validator for 32 bit int 
        public static int int_loop(string message = "Input sth")
        {
            int var = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    var = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Make sure the input is valid");
                }
            }
            return var;
        }
        //Main class used to store data 
        class Animal
        {
            public readonly string Type;
            public readonly int GeneticSequenze;
            public readonly string Gender;
            public readonly int BodyFatRequiredForBreeding;
            public int BodyFat;
            public Animal(string SetType, string SetGender,int SetGeneticSequenze,int defaultBirthbodyFat,int SetBodyFatRequiredForBreeding)
            {
                Gender = SetGender;
                GeneticSequenze = SetGeneticSequenze;
                BodyFat = defaultBirthbodyFat;
                Type = SetType;
                BodyFatRequiredForBreeding = SetBodyFatRequiredForBreeding;
            }
            public void SetBodyFat(int NewBodyFat)
            {
                BodyFat += NewBodyFat;
            }
            
        }
        //Object creation for later usage
        private static Random rnd = new Random();
        //chooses random array content
        public static string RandomArrayElement(string[] ArrayToChoose)
        {
            return ArrayToChoose[rnd.Next(ArrayToChoose.Length)];
        }
        //Generates Random boolean 
        public static bool GenerateRandombool(int precentage)
        {
            var rand = new Random();

            if (rand.Next(1000) < precentage * 10)
            {
                return true;
            }
            return false;
        }
        private static string[] possibleGenders = { "Male", "Female" };
        private static int DeathsPrey;//Record Variables
        private static int DeathsPred;
        static void Main(string[] args)
        {
            //septup start
            Console.WriteLine("Welcome");
            int killme = int_loop("Enter the amount of days to simulate");
            List<Animal> Prey = new List<Animal>();
            List<Animal> Predator = new List<Animal>();
            int PreyCreationInt = int_loop("Enter the amount of prey");
            int FoodAviablableEachDay = int_loop("How much food can the pray locate ?");
            int PredatorCreationInt = int_loop("Enter the amount of predators");
            //creates Prey and Predators init
            for (int i = 0; i < PreyCreationInt; i++)
            {
                Prey.Add(new Animal("Prey", RandomArrayElement(possibleGenders),rnd.Next(2), 5,rnd.Next(10)));
            }
            for (int i = 0; i < PredatorCreationInt;i++)
            {
                Predator.Add(new Animal("Predator", RandomArrayElement(possibleGenders),rnd.Next(2), 5,rnd.Next(10)));
            }
            //setup complete
            //Start simulation
            for (int i = 0; i != killme; i++)
            {
                Console.WriteLine("-----Day "+i+"-----");
                foreach(var ii in Prey)
                {
                    if(ii.GeneticSequenze == 1)//basic prototype of Bavior based on genetic code
                    {
                        FoodAviablableEachDay = FoodAviablableEachDay - 2;
                        ii.SetBodyFat(2);
                    }
                    if(ii.GeneticSequenze == 2)
                    {
                        FoodAviablableEachDay = FoodAviablableEachDay - 1;
                        ii.SetBodyFat(1);
                    }

                    Console.WriteLine(ii.BodyFatRequiredForBreeding);
                }
                foreach(var ii in Predator)
                {
                    Console.WriteLine(ii.Gender);
                }
                // Body fat check and also remove body fat required for living
                foreach(var PreyToCheck in Prey)
                {
                    PreyToCheck.SetBodyFat(PreyToCheck.BodyFat - 1);
                    if(PreyToCheck.BodyFat <= 0)
                    {
                        Console.WriteLine("##"+PreyToCheck.BodyFat);
                        Prey.Remove(PreyToCheck);
                        Console.WriteLine("prey died");
                        DeathsPrey++;
                    }
                }
                foreach (var PredToCheck in Predator)
                {
                    PredToCheck.SetBodyFat(PredToCheck.BodyFat - 1);
                    if (PredToCheck.BodyFat <= 0)
                    {
                        Console.WriteLine("##"+PredToCheck.BodyFat);
                        Prey.Remove(PredToCheck);
                        Console.WriteLine("pred died");
                        DeathsPred++;
                    }
                }
                Console.WriteLine(DeathsPred +"-"+DeathsPrey);
            }
        }
    }
}
