using System;
using System.Collections.Generic;

namespace TrainStationSimulation
{
    public class Station
    {
        // Lists asked in the PW
        private List<Platform> platforms;
        private List<Train> trains;

        // Constructor
        public Station(int numberOfPlatforms)
        {
            platforms = new List<Platform>();
            trains = new List<Train>();

            for (int i = 0; i < numberOfPlatforms; i++)
            {
                platforms.Add(new Platform("P" + (i + 1)));
            }
        }

        // Add a train to the station (from file)
        public void AddTrain(Train train)
        {
            trains.Add(train);
        }

        // Display current status of trains and platforms
        public void DisplayStatus()
        {
            Console.WriteLine("\n--- TRAINS ---");
            foreach (Train train in trains)
            {
                Console.WriteLine("ID: " + train.GetID() + " | Status: " + train.GetStatus() + " | ArrivalTime: " + train.GetArrivalTime() + " | Info: " + train.GetInfo());
            }

            Console.WriteLine("\n--- PLATFORMS ---");
            foreach (Platform platform in platforms)
            {
                string trainInfo = platform.GetCurrentTrain() != null ? "Occupied by " + platform.GetCurrentTrain().GetID() + " | Time Remaining: " + platform.GetDockingTimeRemaining() : "Free";

                Console.WriteLine("Platform " + platform.GetID() + ": " + trainInfo);
            }
        }

        // Advances time by 15 minutes per tick
        public void AdvanceTick()
        {
            Console.WriteLine("\nAdvancing simulation by 15 minutes...");

            foreach (Train train in trains)
            {
                if (train.GetStatus() == TrainStatus.EnRoute)
                {
                    int updatedTime = train.GetArrivalTime() - 15;
                    train.SetArrivalTime(updatedTime);

                    if (updatedTime <= 0)
                    {
                        Platform freePlatform = FindFreePlatform();

                        if (freePlatform != null)
                        {
                            train.SetStatus(TrainStatus.Docking);
                            freePlatform.SetStatus(PlatformStatus.Occupied);
                            freePlatform.SetCurrentTrain(train);
                            freePlatform.SetDockingTimeRemaining(2);
                        }
                        else
                        {
                            train.SetStatus(TrainStatus.Waiting);
                        }
                    }
                }
            }

            // Handle docking timers
            foreach (Platform platform in platforms)
            {
                if (platform.GetStatus() == PlatformStatus.Occupied)
                {
                    int timeLeft = platform.GetDockingTimeRemaining();
                    platform.SetDockingTimeRemaining(timeLeft - 1);

                    if (timeLeft - 1 <= 0)
                    {
                        platform.GetCurrentTrain().SetStatus(TrainStatus.Docked);
                        platform.SetStatus(PlatformStatus.Free);
                        platform.SetCurrentTrain(null);
                    }
                }
            }
        }

        // Utility method to check if simulation is complete
        public bool AllTrainsDocked()
        {
            foreach (Train train in trains)
            {
                if (train.GetStatus() != TrainStatus.Docked)
                {
                    return false;
                }
            }
            return true;
        }

        // Utility: find first free platform
        private Platform FindFreePlatform()
        {
            foreach (Platform platform in platforms)
            {
                if (platform.IsFree())
                {
                    return platform;
                }
            }
            return null;
        }
    }
}