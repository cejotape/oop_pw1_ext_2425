using System;

namespace TrainStationSimulation
{
    public class Platform
    {
        // Private fields
        private string id;
        private PlatformStatus status;
        private Train currentTrain;
        private int dockingTime;
        private int dockingTimeRemaining;

        // Constructor
        public Platform(string id)
        {
            this.id = id;
            this.status = PlatformStatus.Free;
            this.currentTrain = null;
            this.dockingTime = 2;
            this.dockingTimeRemaining = 0;
        }

        // Getters and setters
        public string GetID()
        {
            return this.id;
        }

        public PlatformStatus GetStatus()
        {
            return this.status;
        }

        public void SetStatus(PlatformStatus status)
        {
            this.status = status;
        }

        public Train GetCurrentTrain()
        {
            return this.currentTrain;
        }

        public void SetCurrentTrain(Train train)
        {
            this.currentTrain = train;
        }

        public int GetDockingTime()
        {
            return this.dockingTime;
        }

        public int GetDockingTimeRemaining()
        {
            return this.dockingTimeRemaining;
        }

        public void SetDockingTimeRemaining(int ticks)
        {
            this.dockingTimeRemaining = ticks;
        }

        // Utility method to check if platform is free
        public bool IsFree()
        {
            return this.status == PlatformStatus.Free;
        }
    }
}