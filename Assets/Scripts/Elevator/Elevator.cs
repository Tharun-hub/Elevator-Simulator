using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int currentFloor = 0;

    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float floorHeight = 3f;

    [SerializeField] public float baseHeight;

    public bool useBaseHeight = false;

    public Direction direction = Direction.Idle;

    public TextMeshPro floorDisplay;

    [SerializeField] private List<int> debugQueue = new List<int>(); // For debugging the request queue in inspector

    private Queue<int> requestQueue = new Queue<int>();

    private bool isMoving = false;

    private int targetFloor;

    private void Start()
    {
        //AddRequest(3); // Testing
        currentFloor = Mathf.RoundToInt(transform.position.y / floorHeight);
        UpdateDisplay();
    } 
    private void Update()
    {
        ProcessQueue();
    }
    public void AddRequest(int floor)
    {
        if(!requestQueue.Contains(floor))
        {
            requestQueue.Enqueue(floor);
            debugQueue.Add(floor);
        }
        
    }
    void ProcessQueue()
    {
        if (isMoving) return;

        if(requestQueue.Count == 0)
        {
            direction = Direction.Idle;
            return;
        }

        targetFloor = requestQueue.Dequeue();
        debugQueue.RemoveAt(0);

        StartCoroutine(MoveToFloor(targetFloor));
    }

    IEnumerator MoveToFloor(int floor)
    {
        isMoving = true;

        float targetY = 0;

        if(useBaseHeight)
        {
            targetY = baseHeight + floor * floorHeight;
        }
        else
        {
            targetY = floor * floorHeight;
        }


        Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

        if(floor > currentFloor)
        {
            direction = Direction.Up;
        }
        else if ( floor < currentFloor)
        {
            direction = Direction.Down;
        }

        while(Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            yield return null;
        }
        transform.position = targetPosition;
        currentFloor = floor;
        UpdateDisplay();
        isMoving = false;
    }
    void UpdateDisplay()
    {
        if (floorDisplay != null)
        {
            floorDisplay.text = currentFloor.ToString();
        }
        
    }
    public int GetQueueCount()
    {
        return requestQueue.Count;
    }
}
