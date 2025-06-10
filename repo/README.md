# oop_pw1_ext_2425
This repository is the base element for the development of Practice 1 for the extraordinary OOP exam session. 

**Student ID**: 9405638

## Table of Contents

1. [Introduction](#introduction)
2. [Description of the Solution](#description-of-the-solution)
    - [Design Decisions](#design-decisions)
    - [Class Diagram](#class-diagram)
3. [Problems faced](#problems-and-challenges)
4. [Conclusions](#conclusions)

------

## Introduction

**Group Member**: Carlos Jiménez Pelegrín

This document presents the detailed design for the Train Station Simulation project developed in C# using .NET Core 8. The goal of the project is to simulate the arrival and management of trains at a station with limited platform availability, which the user chooses the number, while I use the OOP principal fundamentals learnt in class.

## Description of the Solution

The system simulates a series of trains arriving over time, being docked to platforms if available, and waiting if not. The simulation progresses in 15-minute intervals (ticks) and updates train statuses accordingly.

### Design Decisions

- **Abstraction**: The `Train` abstract class encapsulates common attributes and behaviors for both passenger and freight trains.
- **Inheritance**: `PassengerTrain` and `FreightTrain` extend the `Train` class and implement specific properties like number of carriages or cargo weight.
- **Encapsulation**: All fields are private and accessed through getters and setters (only when necessary) to maintain control over the data.
- **Polymorphism**: The abstract method `GetInfo()` allows each train type to return its formatted string differently.
- **Platform Logic**: Platforms handle docking timing using a tick-based countdown system. Only `Free` platforms can be assigned.
- **Trains order**: Trains with `Waiting` status are always prioritized over newly arriving trains to ensure fair queue handling.

### Class Diagram

- **Train Class**
+-------------------------+
|        *Train*          |
+-------------------------+
| - id: string            |
| - status: TrainStatus   |
| - arrivalTime: int      |
| - type: string          |
+-------------------------+
| +GetID(): string        |
| +GetStatus(): enum      |
| +SetStatus(TrainStatus) |
| +GetArrivalTime(): int  |
| +SetArrivalTime(int)    |
| +GetTrainType(): string |
| +SetTrainType(string)   |
| *+GetInfo()*: string    | 
+-------------------------+

- **Passenger Train**
+-------------------------------+
|      PassengerTrain           |
+-------------------------------+
| - numberOfCarriages: int      |
| - capacity: int               |
+-------------------------------+
| +GetNumberOfCarriages(): int  |
| +SetNumberOfCarriages(int)    |
| +GetCapacity(): int           |
| +SetCapacity(int)             |
| +GetInfo(): string            | (override)
+-------------------------------+

- **Freight Train**
+-------------------------------+
|         FreightTrain          |
+-------------------------------+
| - maxWeight: int              |
| - freightType: string         |
+-------------------------------+
| +GetMaxWeight(): int          |
| +SetMaxWeight(int)            |
| +GetFreightType(): string     |
| +SetFreightType(string)       |
| +GetInfo(): string            | (override)
+-------------------------------+

- **Platform**
+------------------------------------+
|            Platform                |
+------------------------------------+
| - id: string                       |
| - status: PlatformStatus           |
| - currentTrain: Train              |
| - dockingTimeRemaining: int        |
+------------------------------------+
| +GetID(): string                   |
| +GetStatus(): PlatformStatus       |
| +SetStatus(PlatformStatus)         |
| +GetCurrentTrain(): Train          |
| +SetCurrentTrain(Train)            |
| +GetDockingTimeRemaining(): int    |
| +SetDockingTimeRemaining(int)      |
| +IsFree(): bool                    |
+------------------------------------+

- **Station**
+--------------------------------------------------+
|                    Station                       |
+--------------------------------------------------+
| - platforms: List<Platform>                      |
| - trains: List<Train>                            |
| - tickCounter: int                               |
+--------------------------------------------------+
| +AddTrain(Train): void                           |
| +DisplayStatus(): void                           |
| +AdvanceTick(): void                             |
| +AllTrainsDocked(): bool                         |
| +GetTickCount(): int                             |
| - FindFreePlatform(): Platform                   |
+--------------------------------------------------+

- **Program**
+--------------------------------------------------+
|                    Program                       |
+--------------------------------------------------+
| + static Main(): void                            |
| + static LoadFilesFromFile(): void               |
| + static RunSimulation(): void                   |
+--------------------------------------------------+

- **Train Status**
Enum TrainStatus {EnRoute, Waiting, Docking, Docked}

- **Platform Status**
Enum PlatformStatus {Free, Occupied}

## Problems and Challenges

- **Tick Synchronization**: It was difficult to manage when and how docking timers are reduced in sync with other updates.
- **Fairness Logic**: Initially, newly arrived trains could take free platforms while other trains were waiting. I have solved this with a prioritized platform assignment loop.
- **Input File Validation**: The system needed to have a specific ubication for the CSV file, to handle both `passenger` and `freight` trains in a common CSV.
- **Null Handling**: Avoiding platform `null` errors required careful checks before accessing the train assigned to a platform.
- **Abiding by OOP Guidelines**: It was important to align the structure with examples and constraints given in lectures and in the corrections of the PW1, such as placing enums outside classes and using explicit getters setters.
- **Basing the project on the corrections from PW1**: I have checked the rubric and the marks from PW1 and I have tried to correct the mistakes and to follow the instructions.

## Conclusions

This simulation has helped me to reinforce key OOP concepts, such as encapsulation, abstraction, inheritance, and polymorphism. Building a simulation from scratch also encouraged careful system design and attention to fairness in resource allocation, moreover it has remembered me how to structure this type of programs.
I hope that with what I have done here and with the work I am giving to this appart from this PW, I will be able to program not perfect but yes in a better way than I was.

I have learnt:

- How to properly manage object states and transitions over time.
- The importance of designing clean, reusable class hierarchies.
- The value of prioritizing fairness in concurrent systems, as I mentioned before, there was a problem with the order of the trains.
- How to handle file input, parsing, and error checking, it has helped me to review the exception handling.









