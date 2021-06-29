using System;
using System.Collections.Generic;
using System.Linq;

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
            public int Age = 0;
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
                BodyFat = NewBodyFat;
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
            int FoodAviablableEachDayCInt = int_loop("How much food can the pray locate ?");
            int PredatorCreationInt = int_loop("Enter the amount of predators");
            int MaxAgeForBothPreyAndPred = int_loop("Enter the max age that something can achieve before dying");
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
                int FoodAviablableEachDay = FoodAviablableEachDayCInt;
                Console.WriteLine("-----Day "+i+"-----");
                foreach(var ii in Prey)
                {
                    if(ii.GeneticSequenze == 1 && FoodAviablableEachDay >= 2)//basic prototype of Bavior based on genetic code
                    {
                        FoodAviablableEachDay = FoodAviablableEachDay - 2;
                        ii.SetBodyFat(ii.BodyFat + 2);
                    }
                    if(ii.GeneticSequenze == 0 && FoodAviablableEachDay >= 1)
                    {
                        FoodAviablableEachDay = FoodAviablableEachDay - 1;
                        ii.SetBodyFat(ii.BodyFat + 1);
                    } 
                }
                // Body fat check and also remove body fat required for living
                foreach(var PreyToCheck in Prey.ToList())//The to list thing has to be here or else the code will throw a runtime error do not remove it or else I will murder I dont know why that happens but it happens
                {
                    PreyToCheck.SetBodyFat(PreyToCheck.BodyFat - 1);
                    if(PreyToCheck.BodyFat <= 0 || PreyToCheck.Age > MaxAgeForBothPreyAndPred)
                    {
                        Console.WriteLine("##"+PreyToCheck.BodyFat);
                        Prey.Remove(PreyToCheck);
                        Console.WriteLine("prey died");
                        DeathsPrey++;
                    }
                    else
                    {
                        PreyToCheck.Age++;
                    }
                }
                foreach(var PredToCheck in Predator.ToList())//ToList is important and cant be left out
                {
                    PredToCheck.SetBodyFat(PredToCheck.BodyFat - 1);
                    if (PredToCheck.BodyFat <= 0 || PredToCheck.Age > MaxAgeForBothPreyAndPred)
                    {
                        Console.WriteLine("##" + PredToCheck.BodyFat);
                        Prey.Remove(PredToCheck);
                        Console.WriteLine("pred died");
                        DeathsPred++;//make this shit work 
                    }
                    else
                    {
                        PredToCheck.Age++;
                    }
                }
            }
            Console.WriteLine("---------------------------------");
            Console.WriteLine(DeathsPred+" Predators have died");
            Console.WriteLine(DeathsPrey + " Preys have died");
            Console.WriteLine(Predator.Count + " predators are still alive");
            Console.WriteLine(Prey.Count + " prey's are still alive");
            Console.WriteLine("---------------------------------");
        }
    }
}
