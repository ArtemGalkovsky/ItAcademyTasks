using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerHorizontalMovementBrain))]
    internal class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _rotationSpeed = 5f;
        
        private PlayerInput _playerInput;
        private PlayerHorizontalMovementBrain _playerHorizontalMovementBrain;

        private void Awake()
        {
            _playerHorizontalMovementBrain = GetComponent<PlayerHorizontalMovementBrain>();
            
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.Initialize();
        }

        private void Update()
        {
            Vector2 input = _playerInput.EnabledPlayerInputActions.Movement.Move.ReadValue<Vector2>().normalized;

            Vector2 movement = Time.deltaTime * new Vector2(input.x * _rotationSpeed, input.y * _movementSpeed);
            _playerHorizontalMovementBrain.MovementQueue.Enqueue(movement);
        }
    }
}

