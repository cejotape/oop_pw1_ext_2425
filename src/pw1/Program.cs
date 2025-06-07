using System;
using System.IO;
using System.Collections.Generic;

namespace TrainStationSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Train Station Simulator!");

            // Ask user for number of platforms
            Console.Write("Enter number of platforms to create: ");
            int platformCount = int.Parse(Console.ReadLine());

            // Create station
            Station station = new Station(platformCount);

            bool trainsLoaded = false;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("|----------------------------|");
                Console.WriteLine("|       TRAIN SIMULATOR      |");
                Console.WriteLine("|----------------------------|");
                Console.WriteLine("| 1. Load trains from file   |");
                Console.WriteLine("| 2. Start simulation        |");
                Console.WriteLine("| 3. Display system state    |");
                Console.WriteLine("| 4. Exit                    |");
                Console.WriteLine("|----------------------------|");
                Console.Write("Choose an option: ");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LoadTrainsFromFile(station, ref trainsLoaded);
                        break;

                    case "2":
                        if (!trainsLoaded)
                        {
                            Console.WriteLine("Please load trains first.");
                        }
                        else
                        {
                            RunSimulation(station);
                        }
                        break;

                    case "3":
                        station.DisplayStatus();
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            Console.WriteLine("Goodbye!");
        }

        static void LoadTrainsFromFile(Station station, ref bool trainsLoaded)
        {
            Console.Write("Enter CSV filename: ");
            string filename = Console.ReadLine();

            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length != 5)
                    {
                        Console.WriteLine("Invalid line format: " + line);
                        continue;
                    }

                    string id = parts[0];
                    int arrivalTime = int.Parse(parts[1]);
                    string type = parts[2];

                    if (type == "passenger")
                    {
                        int carriages = int.Parse(parts[3]);
                        int capacity = int.Parse(parts[4]);
                        PassengerTrain pTrain = new PassengerTrain(id, arrivalTime, carriages, capacity);
                        station.AddTrain(pTrain);
                    }
                    else if (type == "freight")
                    {
                        int maxWeight = int.Parse(parts[3]);
                        string freightType = parts[4];
                        FreightTrain fTrain = new FreightTrain(id, arrivalTime, maxWeight, freightType);
                        station.AddTrain(fTrain);
                    }
                    else
                    {
                        Console.WriteLine("Unknown train type: " + type);
                    }
                }

                trainsLoaded = true;
                Console.WriteLine("Trains loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
            }
        }

        static void RunSimulation(Station station)
        {
            Console.WriteLine("\nStarting simulation...");

            while (!station.AllTrainsDocked())
            {
                station.AdvanceTick();
                station.DisplayStatus();

                Console.WriteLine("\nPress ENTER to continue to next tick...");
                Console.ReadLine(); // Wait between ticks
            }

            Console.WriteLine("\n All trains are now docked. Simulation complete.");
        }
    }
}