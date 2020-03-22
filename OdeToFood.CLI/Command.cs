using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.CLI
{
    class Command
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
    }
}
