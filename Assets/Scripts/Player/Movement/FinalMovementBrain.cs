using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    internal class FinalMovementBrain : MonoBehaviour
    {
        private CharacterController _characterController;
        private Queue<QueueMovementComponent> _movementQueue = new Queue<QueueMovementComponent>();
        private Queue<Quaternion> _rotationQueue = new Queue<Quaternion>();

        private bool _isJumping = false;
        private bool _applyGravity = true;
        private Vector3 _lastGroundedMovement = Vector3.zero;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void AddMovementToQueue(QueueMovementComponent movementComponent)
        {
            _movementQueue.Enqueue(movementComponent);
        }

        public void AddRotationToQueue(Quaternion rotation)
        {
            _rotationQueue.Enqueue(rotation);
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            ApplyRotation();
        }

        private void ApplyMovement()
        {
            if (_characterController.isGrounded)
            {
                _lastGroundedMovement = _characterController.velocity * Time.fixedDeltaTime;
                _isJumping = false;
            }
            
            _applyGravity = true;

            List<QueueMovementComponent> movements = PrepareMovement();
            
            Vector3 finalMovement = Vector3.zero;

            foreach (QueueMovementComponent component in movements)
            {
                if (!_applyGravity && component.SourceOfMovement == "Gravity")
                {
                    continue;
                }
                
                finalMovement += component.Movement;
            }
            
            if (_isJumping)
            {
                finalMovement += new Vector3(_lastGroundedMovement.x, 0f, _lastGroundedMovement.z); // Saving forward moving when jump
            }
            
            _characterController.Move(finalMovement);
        }

        private List<QueueMovementComponent> PrepareMovement()
        {
            List<QueueMovementComponent> movements = new List<QueueMovementComponent>();

            while (_movementQueue.Count > 0)
            {
                QueueMovementComponent component = _movementQueue.Dequeue();
                movements.Add(component);
                
                if (component.Movement.y > 0)
                {
                    _isJumping = true;
                    _applyGravity = false;
                }
            }

            return movements;
        }

        private void ApplyRotation()
        {
            Quaternion rotation = Quaternion.identity;
            
            while (_rotationQueue.Count > 0)
            {
                rotation *= _rotationQueue.Dequeue();
            }
            
            transform.Rotate(rotation.eulerAngles);
        }
    }
}
