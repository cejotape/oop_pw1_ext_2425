using System;

namespace TrainStationSimulation
{
    public class FreightTrain : Train
    {
        // Attributes specific to freight trains
        private int maxWeight;
        private string freightType;

        // Constructor
        public FreightTrain(string id, int arrivalTime, int maxWeight, string freightType) : base(id, arrivalTime, "freight")
        {
            this.maxWeight = maxWeight;
            this.freightType = freightType;
        }

        // Here the same as in PassengersTrain, there are no setters for these attributes as they are set in the constructor and should not change during the simulation.
        public int GetMaxWeight()
        {
            return this.maxWeight;
        }

        public string GetFreightType()
        {
            return this.freightType;
        }

        // Here I override the GetInfo method to provide specific information about the freight train, the same as in PassengerTrain.
        // This method returns a string with the train's ID, maximum weight, and type of freight.
        public override string GetInfo()
        {
            return "[Freight] ID: " + GetID() + ", MaxWeight: " + this.maxWeight + ", Type: " + this.freightType;
        }
    }
}