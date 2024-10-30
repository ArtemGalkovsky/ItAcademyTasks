using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(Collider))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _playerMovementSpeedCoefficient = 0.6f;
        [SerializeField] private float _runSpeedMultiplier = 2f;

        [Header("Player Horizontal Rotation")]
        [SerializeField] private float _rotationHorizontalSpeedCoefficient = 300f;
        
        private PlayerActions _playerInput;
        private Rigidbody _rigidbody;
        
        private bool _isRunning = false;

        private void Start()
        {
            SetupPlayerSettings();   
        }

        private void SetupPlayerSettings()
        {
            PlayerInput.Initialize();
            
            _playerInput = PlayerInput.EnabledPlayerInputActions;
            _rigidbody = GetComponent<Rigidbody>();
            
            _playerInput.Movement.Run.performed += context => _isRunning = true;
            _playerInput.Movement.Run.canceled += context => _isRunning = false;
            
            _playerInput.Menu.Escape.performed += context => Cursor.lockState = CursorLockMode.None;
            _playerInput.Menu.Escape.canceled += context => Cursor.lockState = CursorLockMode.Locked;
            
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void FixedUpdate()
        {
            Move();
         
            Vector2 rotationDelta = PlayerInput.GetRotationDelta();
            
            RotatePlayerHorizontally(rotationDelta.x);
        }
        
        private void Move()
        {
            Vector2 movementDirections = _playerInput.Movement.Move.ReadValue<Vector2>().normalized;
            
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
            float rotationHorizontal = rotationDeltaHorizontal * _rotationHorizontalSpeedCoefficient;
            _rigidbody.angularVelocity = new Vector3(0f, rotationHorizontal, 0f);
        }
    }
}
