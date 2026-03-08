using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public List<Elevator> elevators = new List<Elevator>();

    public int totalFloors = 4;

    public void RequestElevator(int floor) // Method to simply call the Elevator to the floor
    {
        if(floor < 0 || floor >= totalFloors)
        {
            Debug.LogWarning("Invalid floor Request");
            return;
        }

        Elevator bestElevator = FindBestElevator(floor);

        if(bestElevator != null)
        {
            bestElevator.AddRequest(floor); 
        }
    }
    public void RequestElevator(int floor, Direction requestDirection) // Method to call the Elevator to the floor with Up or Down buttons
    {
        if (floor < 0 || floor >= totalFloors)
        {
            Debug.LogWarning("Invalid floor Request");
            return;
        }

        Elevator bestElevator = FindBestElevator(floor, requestDirection);

        if (bestElevator != null)
        {
            bestElevator.AddRequest(floor);
        }
    }
    Elevator FindBestElevator(int floor)
    {
        Elevator best = null;
        float bestCost = float.MaxValue;

        foreach(Elevator elevator in elevators)
        {
            float cost = CalculateCost(elevator, floor);

            if(cost < bestCost)
            {
                bestCost = cost;
                best = elevator;
            }
        }

        return best;
    }

    Elevator FindBestElevator(int floor, Direction requestDirection)
    {
        Elevator best = null;
        float bestCost = float.MaxValue;

        foreach(Elevator elevator in elevators)
        {
            float cost = CalculateCost(elevator, floor, requestDirection);

            if(cost< bestCost)
            {
                bestCost = cost;
                best = elevator;
            }
        }
        return best;
    }
    float CalculateCost(Elevator elevator, int requestedFloor)
    {
        float cost = Mathf.Abs(elevator.currentFloor - requestedFloor);

        // Penalize elevators moving in the opposite direction of the request
        if (elevator.direction == Direction.Up && requestedFloor < elevator.currentFloor)
        {
            cost += 2f;
        }
        else if(elevator.direction == Direction.Down && requestedFloor > elevator.currentFloor)
        {
            cost += 2f;
        }

        cost += elevator.GetQueueCount();

        return cost;
    }
    float CalculateCost(Elevator elevator, int requestedFloor, Direction requestDirection)
    {
        float cost = Mathf.Abs(elevator.currentFloor - requestedFloor);

        // Penalize elevators moving opposite to requested direction
        if(requestDirection == Direction.Up && elevator.direction == Direction.Down)
        {
            cost += 2f;
        }
        else if(requestDirection == Direction.Down && elevator.direction == Direction.Up)
        {
            cost += 2f;
        }

        cost += elevator.GetQueueCount();

        return cost;
    }
}
