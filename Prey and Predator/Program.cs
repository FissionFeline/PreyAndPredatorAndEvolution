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
                GeneticSequenze = SetGeneticSequenze; // genetic sequenzes dictate the hunting / breeding behavour
                BodyFat = defaultBirthbodyFat; 
                Type = SetType; // prey or predator 
                BodyFatRequiredForBreeding = SetBodyFatRequiredForBreeding;
            }
            public void SetBodyFat(int NewBodyFat)
            {
                BodyFat = NewBodyFat; // sets body fat 
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
            if (rnd.Next(100) < precentage)
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
            List<Animal> Prey = new List<Animal>(); // these are used to store the classes
            List<Animal> Predator = new List<Animal>();
            int PreyCreationInt = int_loop("Enter the amount of prey");
            int FoodAviablableEachDayCInt = int_loop("How much food can the pray locate ?");
            int PredatorCreationInt = int_loop("Enter the amount of predators");
            int MaxAgeForBothPreyAndPred = int_loop("Enter the max age that something can achieve before dying");
            //creates Prey and Predators init
            for (int i = 0; i < PreyCreationInt; i++)
            {
                Prey.Add(new Animal("Prey", RandomArrayElement(possibleGenders),rnd.Next(2), 5,rnd.Next(3)));
            }
            for (int i = 0; i < PredatorCreationInt;i++)
            {
                Predator.Add(new Animal("Predator", RandomArrayElement(possibleGenders),rnd.Next(2), 5,rnd.Next(3)));
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
                foreach(var PredToEat in Predator.ToList())
                {
                    if (GenerateRandombool(5))
                    {
                        Console.WriteLine("Predator has died during the hunt");
                        Predator.Remove(PredToEat);
                        DeathsPred++;
                        continue;
                    }
                    int[] PossibleVic = { rnd.Next(Prey.Count), rnd.Next(Prey.Count) };
                    foreach (int PossiblePreyToBeEaten in PossibleVic)
                    {
                        if(GenerateRandombool(PredToEat.GeneticSequenze * 25))
                        {
                            Console.WriteLine("Pred has found some food");
                            try
                            {
                                var PreyToBeEaten = Prey[PossiblePreyToBeEaten];
                                PredToEat.SetBodyFat(PredToEat.BodyFat + PreyToBeEaten.BodyFat);
                                Prey.RemoveAt(PossiblePreyToBeEaten);
                                DeathsPrey++;
                            }
                            catch
                            {//yeah this may be bAd PrAcTiSE but i dont really care
                            }
                        }
                    }                    
                }
                // Body fat check and also remove body fat required for living
                foreach(var PreyToCheck in Prey.ToList())//The to list thing has to be here or else the code will throw a runtime error do not remove it or else I will murder I dont know why that happens but it happens
                {
                    PreyToCheck.SetBodyFat(PreyToCheck.BodyFat - 1);
                    if(PreyToCheck.BodyFat <= 0 || PreyToCheck.Age > MaxAgeForBothPreyAndPred)
                    {
                        Prey.Remove(PreyToCheck);
                        Console.WriteLine("prey died");
                        DeathsPrey++;
                    }
                    else
                    {
                        PreyToCheck.Age++;
                    }
                }
                foreach(var PredToCheck in Predator.ToList())//ToList is important and cant be left out it cant 
                {
                    PredToCheck.SetBodyFat(PredToCheck.BodyFat - 1);
                    if (PredToCheck.BodyFat <= 0 || PredToCheck.Age > MaxAgeForBothPreyAndPred)
                    {
                        Predator.Remove(PredToCheck);
                        Console.WriteLine("pred died");
                        DeathsPred++;
                    }
                    else
                    {
                        PredToCheck.Age++;
                    }
                }
                // reproduction of preds and prey
                if (Predator.Count < 2)
                {
                    List<Animal> PredM = new List<Animal>();
                    List<Animal> PredF = new List<Animal>();
                    foreach (var PredToRep in Predator)
                    {
                        if(PredToRep.Gender == "Female" && PredToRep.BodyFat >= PredToRep.BodyFatRequiredForBreeding)
                        {
                            PredF.Add(PredToRep);
                        }else if(PredToRep.Gender == "Male" && PredToRep.BodyFat >= PredToRep.BodyFatRequiredForBreeding)
                        {
                            PredM.Add(PredToRep);
                        }
                    }
                    foreach (var PredToRep in PredF)
                    {
                        if (PredM.Count > 0)
                        {
                            Prey.Add(new Animal("Predator", RandomArrayElement(possibleGenders), PredToRep.GeneticSequenze, 5, rnd.Next(3)));
                        }
                    }
                }
                if (Prey.Count == 0 || Prey.Count == 1)
                {
                    List<Animal> PreyM = new List<Animal>();
                    List<Animal> PreyF = new List<Animal>();
                    foreach (var PreyToRep in Prey)
                    {
                        if (PreyToRep.Gender == "Female" && PreyToRep.BodyFat >= PreyToRep.BodyFatRequiredForBreeding)
                        {
                            PreyF.Add(PreyToRep);
                        }
                        else if(PreyToRep.Gender == "Female" && PreyToRep.BodyFat >= PreyToRep.BodyFatRequiredForBreeding)
                        {
                            PreyM.Add(PreyToRep);
                        }
                    }
                    foreach (var PreyToRep in PreyF)
                    {
                        if (PreyM.Count > 0)
                        {
                            Prey.Add(new Animal("Prey", RandomArrayElement(possibleGenders), PreyToRep.GeneticSequenze, 5, rnd.Next(3)));
                        }
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
