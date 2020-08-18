using System;
using System.Linq;
using System.Collections.Generic;

namespace GITtraincs
{
    class Program
    {
        class City
        {
            public City(int population, int gold)
            {
                this.Population = population;
                this.Gold = gold;
            }
            public int Population { get; set; }
            public int Gold { get; set; }
        }
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, City>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Sail")
                {
                    break;
                }
                string[] splittedInput = input.Split("||");
                string name = splittedInput[0];
                int population = int.Parse(splittedInput[1]);
                int gold = int.Parse(splittedInput[2]);
                if (!dict.ContainsKey(name))
                {
                    City city = new City(population, gold);
                    dict.Add(name, city);
                }
                else
                {
                    dict[name].Gold += gold;
                    dict[name].Population += population;
                }
            }
            while (true)
            {
                string currLine = Console.ReadLine();
                if (currLine == "End")
                {
                    break;
                }
                string[] splittedLine = currLine.Split("=>");
                if (currLine.Contains("Plunder"))
                {
                    string name = splittedLine[1];
                    int people = int.Parse(splittedLine[2]);
                    int gold = int.Parse(splittedLine[3]);
                    Console.WriteLine($"{name} plundered! {gold} gold stolen, {people} citizens killed.");
                    dict[name].Population -= people;
                    dict[name].Gold -= gold;
                    if (dict[name].Gold <= 0 || dict[name].Population <= 0)
                    {
                        Console.WriteLine($"{name} has been wiped off the map!");
                        dict.Remove(name);
                    }
                }
                if (currLine.Contains("Prosper"))
                {
                    string name = splittedLine[1];
                    int gold = int.Parse(splittedLine[2]);
                    if (gold < 0)
                    {
                        Console.WriteLine("Gold added cannot be a negative number!");
                    }
                    else
                    {
                        dict[name].Gold += gold;
                        Console.WriteLine($"{gold} gold added to the city treasury. {name} now has {dict[name].Gold} gold.");

                    }
                }
            }
            Console.WriteLine($"Ahoy, Captain! There are {dict.Count} wealthy settlements to go to:");
            var sortDict = dict.OrderByDescending(x => x.Value.Gold).ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
            foreach (var kvp in sortDict)
            {
                Console.WriteLine($"{kvp.Key} -> Population: {kvp.Value.Population} citizens, Gold: {kvp.Value.Gold} kg");
            }
        }
    }
}
