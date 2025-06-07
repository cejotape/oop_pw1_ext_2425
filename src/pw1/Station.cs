using System;
using System.Collections.Generic;

namespace TrainStationSimulation
{
    public class Station
    {
        // Lists asked in the PW
        private List<Platform> platforms;
        private List<Train> trains;
        private int tickCounter = 0; // Counter for simulation ticks

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
            tickCounter++;
            Console.WriteLine($"\n|------ TICK {tickCounter} ({tickCounter * 15} min) ------|");
            Console.WriteLine("Advancing simulation by 15 minutes...");

            // Step 1: Decrease arrival time for EnRoute trains
            foreach (Train train in trains)
            {
                if (train.GetStatus() == TrainStatus.EnRoute)
                {
                    int updatedTime = train.GetArrivalTime() - 15;
                    train.SetArrivalTime(updatedTime);
                }
            }

            // Step 2: Process docking timers and free platforms if docking ends
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

            // Step 3: While there are waiting trains and free platforms, assign them
            bool assigned;
            do
            {
                assigned = false;
                foreach (Train waitingTrain in trains)
                {
                    if (waitingTrain.GetStatus() == TrainStatus.Waiting)
                    {
                        Platform freePlatform = FindFreePlatform();
                        if (freePlatform != null)
                        {
                            waitingTrain.SetStatus(TrainStatus.Docking);
                            freePlatform.SetStatus(PlatformStatus.Occupied);
                            freePlatform.SetCurrentTrain(waitingTrain);
                            freePlatform.SetDockingTimeRemaining(2);
                            assigned = true;
                        }
                    }
                }
            } while (assigned); // Keep assigning while there are Waiting trains and free platforms

            // Step 4: Now consider EnRoute trains that have just arrived
            foreach (Train train in trains)
            {
                if (train.GetStatus() == TrainStatus.EnRoute && train.GetArrivalTime() <= 0)
                {
                    Platform freePlatform = FindFreePlatform();

                    // ONLY allow if no one is waiting
                    bool hasWaiting = trains.Exists(t => t.GetStatus() == TrainStatus.Waiting);
                    if (freePlatform != null && !hasWaiting)
                    {
                        train.SetStatus(TrainStatus.Docking);
                        freePlatform.SetStatus(PlatformStatus.Occupied);
                        freePlatform.SetCurrentTrain(train);
                        freePlatform.SetDockingTimeRemaining(2);
                    }
                    else if (hasWaiting)
                    {
                        train.SetStatus(TrainStatus.Waiting);
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

        public int GetTickCount()
        {
            return tickCounter;
        }
    }
}