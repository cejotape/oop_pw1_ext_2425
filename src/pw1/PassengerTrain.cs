using System;

namespace TrainStationSimulation
{
    public class PassengerTrain : Train
    {
        // Attributes specific to passenger trains
        private int numberOfCarriages;
        private int capacity;

        // Constructor
        public PassengerTrain(string id, int arrivalTime, int numberOfCarriages, int capacity) : base(id, arrivalTime, "passenger")
        {
            this.numberOfCarriages = numberOfCarriages;
            this.capacity = capacity;
        }

        // There are no setters for these attributes as they are set in the constructor and should not change during the simulation.
        public int GetNumberOfCarriages()
        {
            return this.numberOfCarriages;
        }

        public int GetCapacity()
        {
            return this.capacity;
        }

        // Here I override the GetInfo method to provide specific information about the passenger train.
        // This method returns a string with the train's ID, number of carriages, and capacity.
        public override string GetInfo()
        {
            return "[Passenger] ID: " + GetID() + ", Carriages: " + this.numberOfCarriages + ", Capacity: " + this.capacity;
        }
    }
}