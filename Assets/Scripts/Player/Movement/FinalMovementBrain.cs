using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    internal class FinalMovementBrain : MonoBehaviour
    {
        private CharacterController _characterController;
        private Queue<Vector3> _movementQueue = new Queue<Vector3>();
        private Queue<Quaternion> _rotationQueue = new Queue<Quaternion>();

        private bool _isJumping = false;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void AddMovementToQueue(Vector3 movement)
        {
            _movementQueue.Enqueue(movement);
        }

        public void AddRotationToQueue(Quaternion rotation)
        {
            _rotationQueue.Enqueue(rotation);
        }

        private void FixedUpdate()
        {
            List<Vector3> movements = new List<Vector3>();
            for (int i = 0; i < _movementQueue.Count; i++)
            {
                Vector3 newMovement = _movementQueue.Dequeue();
                movements.Add(newMovement);
                
                if (newMovement.y > 0)
                {
                    _isJumping = true;
                }
            }
            
            AddMovement(movements);
            
            Quaternion rotation = Quaternion.identity;
            
            for (int i = 0; i < _rotationQueue.Count; i++)
            {
                rotation *= _rotationQueue.Dequeue();
            }

            transform.Rotate(rotation.eulerAngles);
        }

        private void AddMovement(IEnumerable<Vector3> movements)
        {
            Vector3 movement = Vector3.zero;

            foreach (Vector3 movementVector in movements)
            {
                if (movementVector.y < 0 && _isJumping)
                {
                    continue;
                }
                
                movement += movementVector;
            }

            _isJumping = false;
            _characterController.Move(movement);
        }
    }
}

