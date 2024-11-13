using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(Collider))]
    internal class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _playerMovementSpeedCoefficient = 150f;
        [SerializeField] private float _runSpeedMultiplier = 2f;

        [Header("Player Horizontal Rotation")]
        [SerializeField] private float _rotationHorizontalSpeedCoefficient = 300f;
        
        private PlayerInput _playerInput;
        private PlayerActions _playerInputActions;
        private Rigidbody _rigidbody;

        
        private bool _isRunning = false;

        private void Start()
        {
            SetupPlayerSettings();   
        }

        private void SetupPlayerSettings()
        {
            _playerInput = GetComponent<PlayerInput>();
            
            _playerInput.Initialize();
            _playerInputActions = _playerInput.EnabledPlayerInputActions;
            _rigidbody = GetComponent<Rigidbody>();
            
            _playerInputActions.Movement.Run.performed += context => _isRunning = true;
            _playerInputActions.Movement.Run.canceled += context => _isRunning = false;
            
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void FixedUpdate()
        {
            Move();
         
            Vector2 rotationDelta = _playerInput.GetRotationDelta();
            
            RotatePlayerHorizontally(rotationDelta.x);
        }
        
        private void Move()
        {
            Vector2 movementDirections = _playerInputActions.Movement.Move.ReadValue<Vector2>().normalized;
            
            Vector3 movement = transform.forward * movementDirections.y + transform.right * movementDirections.x;
            Vector3 velocity = _playerMovementSpeedCoefficient * Time.fixedDeltaTime * movement;

            if (_isRunning)
            {
                velocity *= _runSpeedMultiplier;
            }

            velocity.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = velocity;
        }
        
        private void RotatePlayerHorizontally(float rotationDeltaHorizontal)
        {
            float rotationHorizontal = rotationDeltaHorizontal * _rotationHorizontalSpeedCoefficient * Time.deltaTime;
            _rigidbody.angularVelocity = new Vector3(0f, rotationHorizontal, 0f);
        }
    }
}
