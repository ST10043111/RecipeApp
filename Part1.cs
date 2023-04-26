// See https://aka.ms/new-console-template for more information

using System;

namespace PancakeRecipe;
{
class program
{
    static Dictionary<string, int> DefaultQuantities = new Dictionary<string, int>
    {
        { "floor",2},
        { "eggs",2},
        { "milk",1},
        { "sugar",1},
        { "baking powder",2},

        };

    const string OriginalQuantitiesFile = "original_quantities.txt";

    static void Main(string[] args)
    {
        if (args.Length ==0)
        {
            Console.WriteLine("Error:Command not clearly specified");
            return;

        }

        string command = args[0].ToLower();

        switch (command)
        {

            case "print":
                PrintRecipe();
                break;

            case "Reset":
                ResetQuantities();
                break;

            default:
                Console.WriteLine($"Error:invalid command {command}");
                break;
        }
    }

    static Dictionary<string, int> ReadoriginalQuantities()
    {

        Dictionary<string, int> originalQuantities = new Dictionary<string, int>();

        if (File.Exists(OriginalQuantitiesFile))
        {
            foreach (string line in File.ReadAllLines(OriginalQuantitiesFile))
            {

                string[] parts = line.Split(":");
                string ingredients = parts[0].Trim().ToLower();
                int quantity = int.Parse(parts[1].Trim());
                originalQuantities[ingredients] = quantity;
            }
        }
        else
        {
            originalQuantities = DefaultQuantites;

        }

        return originalQuantities;

    }
    static void WriteOriginalQuantities(Dictionary<string, int> quantites)
    {
        using (StreamWriter file = new StreamWriter(OriginalQuantities))
        {
            foreach (KeyValuePair<string, int> pair in quantities)
            {
                file.WriteLine($"{pair.Key}:{pair.Value}");
            }

        }
    }
    static void ResetQuantities()
    {
        Dictionary<string, int> originalQuantities = ReadoriginalQuantities();
        WriteOriginalQuantities(originalQuantities);
        Console.WriteLine("Quantities have reset to default values:");
        PrintRecipe(originalQuantities);
    }
    static void PrintRecipe()
    {
        Dictionary<string, int> quantities = ReadoriginalQuantities();
        PrintRecipe(quantities);

        static void PrintRecipe(Dictionary<string, int> quantities)
        {
            Console.WriteLine("Pancake Recipe:");
            foreach (KeyValuePair<string, int> pair in quantities)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
    
using System;

namespace RecipeApplication
{
    class Recipe
    {
        public string Name { get; set; }
        public string[] Ingredients { get; set; }
        public string Instructions { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the name of the recipe:");
            recipe.Name = Console.ReadLine();

            Console.WriteLine("Enter the ingredients (separated by commas):");
            string ingredientsString = Console.ReadLine();
            recipe.Ingredients = ingredientsString.Split(',');

            Console.WriteLine("Enter the cooking instructions:");
            recipe.Instructions = Console.ReadLine();

            Console.WriteLine("\nRecipe Details:");
            Console.WriteLine("Name: {0}", recipe.Name);
            Console.WriteLine("Ingredients:");
            foreach (string ingredient in recipe.Ingredients)
            {
                Console.WriteLine("- {0}", ingredient);
            }
            Console.WriteLine("Instructions: {0}", recipe.Instructions);

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}

