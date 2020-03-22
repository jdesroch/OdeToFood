using OdeToFood.Core;
using OdeToFood.Data;
using System;
using System.ComponentModel.Design;

namespace OdeToFood.CLI
{
    class Program
    {
        enum Action
        {
            Exit, List, View
        }
        static private void printHelp()
        {
            Console.WriteLine($"\nAvailable Actions");
            foreach (string a in Enum.GetNames(typeof(Action)))
            {
                Console.WriteLine($"{a}");
            }
        }

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                interactiveMode();
            else
                parseInput(args);
        }

        private static void interactiveMode()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine();
                if (input.ToLower().Equals("exit"))
                    exit = true;
                else
                    parseInput(input.Split());

            }
        }

        private static void parseInput(string[] input)
        {
            try
            {
                Action action = Enum.Parse<Action>(input[0], true);
                switch (action)
                {
                    case Action.List:
                        Console.WriteLine($"\nRunning List Action");
                        ListRestaurants();
                        break;
                    case Action.View:
                        Console.WriteLine($"\nRunning View Action");
                        break;
                    default:
                        printHelp();
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                printHelp();
            }
        }

        private static void ListRestaurants(string name = null)
        {
            IRestaurantData restaurantData = new InMemoryRestaurantData();
            foreach (Restaurant r in restaurantData.GetRestaurantsByName(name))
            {
                //Console.WriteLine($"Id: {r.Id}\tName: {r.Name}\tCuisine: {r.Cuisine}\tLocation: {r.Location}");

                Console.WriteLine(r.ToJSON());
            }
        }


    }
}
