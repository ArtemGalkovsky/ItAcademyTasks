using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerVerticalMovementBrain : MonoBehaviour
    {
        private CharacterController _characterController;
        public Queue<float> MovementQueue { get; }= new Queue<float>();

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void LateUpdate()
        {
            float finalMovementY = 0f;
            while (MovementQueue.Count > 0)
            {
                float movement = MovementQueue.Dequeue();
                finalMovementY += movement;
            }
            _characterController.Move(finalMovementY * Vector3.up);
        }
    }
}

