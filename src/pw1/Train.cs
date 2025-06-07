using System;

namespace TrainStationSimulation
{
    // Abstract base class with shared attributes for all trains.
    public abstract class Train
    {
        private string id;
        private TrainStatus status;
        private int arrivalTime;
        private string type;

        // Constructor
        public Train(string id, int arrivalTime, string type, TrainStatus status = TrainStatus.EnRoute)
        {
            this.id = id;
            this.arrivalTime = arrivalTime;
            this.type = type;
            this.status = status;
        }

        // Getters and Setters
        public string GetID()
        {
            return this.id;
        }

        public TrainStatus GetStatus()
        {
            return this.status;
        }

        public void SetStatus(TrainStatus status) // Set the status of the train based on what is told in PW
        {
            this.status = status;
        }

        public int GetArrivalTime()
        {
            return this.arrivalTime;
        }

        public void SetArrivalTime(int value)
        {
            this.arrivalTime = value;
        }

        public string GetTrainType()
        {
            return this.type;
        }
    }
}