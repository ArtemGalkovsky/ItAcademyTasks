using System;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] private Vector3 initialStartPosition;
    [SerializeField] private Vector3 initialEndPosition;
    [SerializeField, Min(0.001f)] private float speed = 0.001f;
    [SerializeField, Range(1, 15)] private float distanceBetweenVectorsToChangeDirection = 1f;

    private MoveDirections moveDirection = MoveDirections.Start2End;
    private Vector3 currentEndPosition;

    void Awake()
    {
        if (moveDirection == MoveDirections.Start2End)
            currentEndPosition = initialEndPosition;
        else
            currentEndPosition = initialStartPosition;

        transform.position = initialStartPosition;
    }

    void Update()
    {
        ChangeDirectionIfNeeded();
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, currentEndPosition, Time.deltaTime * speed);
    }

    private void ChangeDirectionIfNeeded()
    {
        if (Vector3.Distance(transform.position, currentEndPosition) < distanceBetweenVectorsToChangeDirection)
        {
            ChangeEndPosition();
        }
    }

    private void ChangeEndPosition()
    {
        switch (moveDirection)
        {
            case MoveDirections.Start2End:
                currentEndPosition = initialStartPosition;
                moveDirection = MoveDirections.End2Start;
                break;
            case MoveDirections.End2Start:
                currentEndPosition = initialEndPosition;
                moveDirection = MoveDirections.Start2End;
                break;
            default:
                Debug.LogError("Field 'moveDirection' is incorrect!");
                break;
            }
    }
    
}
