using UnityEngine;


namespace Player
{
    public struct QueueMovementComponent
    {
        public Vector3 Movement { get; private set; }
        public string SourceOfMovement { get; private set; }

        public QueueMovementComponent(Vector3 movement, string sourceOfMovement)
        {
            Movement = movement;
            SourceOfMovement = sourceOfMovement;
        }
    }
}
