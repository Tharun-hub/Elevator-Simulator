using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public int floorNumber;
    public Direction direction;

    public ElevatorManager elevatorManager;

    public void SimplePressButton()
    {
        elevatorManager.RequestElevator(floorNumber);
    }

    public void UpDownPressButton()
    {
        elevatorManager.RequestElevator(floorNumber, direction);
    }
}
