using MarsRovers.Controller;
using MarsRovers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    class Program
    {
        public static void Main(string[] args)
        {
            DisplayMainMenu();
        }
        /// <summary>
        /// Show the Menu in the console.
        /// </summary>
        public static void DisplayMainMenu()
        {
            string option = "";
            RoversSet set = new RoversSet();
            while (option != "X")
            {
                Console.Clear();
                Console.WriteLine("Mars Rovers =========================");
                Console.WriteLine("Select one option:");
                Console.WriteLine("1. Configure new set");
                if (set.IsConfigured)
                    Console.WriteLine("2. Execute current set");
                Console.WriteLine("X. Exit program");
                Console.WriteLine("Option:");
                option = Console.ReadLine();
                if (!string.IsNullOrEmpty(option))
                    option = option.Trim().ToUpper();
                switch (option)
                {
                    case "1":
                        if (set.IsConfigured)
                        {
                            option = InputHelper.GetValidInputByRegex("Do you want to replace the current set? (Y/N)", @"^\s*(Y|N)\s*$");
                            if (option.Equals("Y"))
                                set = ConfigureSet();
                        }
                        else
                            set = ConfigureSet();
                        break;
                    case "2": ExcecuteSet(set); break;
                    case "X": Console.WriteLine("Good bye!"); break;
                    default: Console.WriteLine("Please, select a valid option:"); break;
                }
            }
        }
        /// <summary>
        /// Creates a new RoverSet and configures it accordingly to the user inputs.
        /// </summary>
        /// <returns></returns>
        public static RoversSet ConfigureSet()
        {
            Console.Clear();
            Console.WriteLine("Configuration of a new set =========================");
            //Get Boundary
            string BoundariesText = InputHelper.GetValidInputByRegex("Enter exploration grid bounds [X Y] (0 < X and 0 < Y):", @"^\s*(?![0])\d{1,}(\s|,)+(?![0])\d{1,}\s*$");
            BoundariesText = Regex.Replace(BoundariesText, @"(\s|,)+", ",");
            int[] Limits = BoundariesText.Split(',').Select(t => int.Parse(t)).ToArray();
            //Create Set
            RoversSet newSet = new RoversSet(Limits);
            //Get Total Rovers
            int TotalRovers = InputHelper.GetIntFromInput("How many rovers do you need? (0 < n): ");
            for (int i = 1; i <= TotalRovers; i++)
            {
                bool validData;
                string inputStr = "";
                string[] Coords = new string[3];
                do
                {
                    //Get Coords
                    inputStr = InputHelper.GetValidInputByRegex($"Set coordinates for rover [{i}]: [X' Y' Z]", @"^\s*\d{1,}(\s|,)+\d{1,}(\s|,)+(N|E|W|S)\s*$");
                    inputStr = Regex.Replace(inputStr, @"(\s|,)+", ",");
                    Coords = inputStr.Split(',');
                    if (int.Parse(Coords[0]) > Limits[0] || int.Parse(Coords[1]) > Limits[1])
                    {
                        Console.WriteLine("Coordinates are out of boundaries");
                        validData = false;
                    }
                    else
                        validData = true;
                }
                while (!validData);
                //Get Instructions
                inputStr = InputHelper.GetValidInputByRegex($"Set instructions for rover [{i}] (L => 90° Left | R => 90° Rigth | M => Move FWD 1 place)", @"^\s*(L|R|M)+\s*$");
                //Add rover
                newSet.AddNewRover(i, Coords, inputStr);
            }
            return newSet;
        }
        /// <summary>
        /// Call the Execute Method of the RoverSet and displays its results on screen.
        /// </summary>
        /// <param name="set"></param>
        public static void ExcecuteSet(RoversSet set)
        {
            Console.WriteLine($"Map boundaries {set.GetBoundaries()} =========================");
            Console.WriteLine("Executing instructions...");
            List<string> outputs = set.Run();
            foreach (string output in outputs)
            {
                Console.WriteLine(output);
            }
            Console.ReadLine();
        }
    }
}