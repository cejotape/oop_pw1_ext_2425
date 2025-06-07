// Stores all shared enumerations for the simulation.

namespace TrainStationSimulation
{
    // Enum for train
    public enum TrainStatus
    {
        EnRoute, Waiting, Docking, Docked
    }
    
    // Enum for platform status
    public enum PlatformStatus
    {
        Free, Occupied
    }
}