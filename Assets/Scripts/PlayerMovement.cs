using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeedCoefficient = 10f;
        [SerializeField] private float _rotationSpeedCoefficient = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _gravityValue = -9.81f;
        [SerializeField] private Transform _respawnTransform;
        [SerializeField] private float _movementJoyStickDeathZone = 0.3f;
        [SerializeField] private float _horizontalJoyStickRotationDeathZone = 0.3f;
        
        private CharacterController _controller;
        private PlayerInput _input;
        private float _verticalVelocity;
        private bool _isJumping;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = Input.EnabledPlayerInput;
            
            _input.Jump.Jump.performed += JumpCallback;

            // Cursor.lockState = CursorLockMode.Locked;

            if (_respawnTransform == null)
            {
                Debug.LogError("Respawn transform is null!");
            }
            else
            {
                PlayerWaterDie.DieEvent.AddListener(MoveToRespawnPoint);
            }
        }

        private void MoveToRespawnPoint()
        {
            Vector3 motion = _respawnTransform.position - transform.position;
            _controller.Move(motion);
        }
        
        private void Update()
        {
            Move();
        }

        private void RotateHorizontally(float rotationMovement)
        {
            transform.Rotate(rotationMovement * Vector3.up, Space.World);
        }

        private void Move()
        {
            Vector2 movementInput = GetMovementInput();
            
            if (Mathf.Abs(movementInput.x) > _horizontalJoyStickRotationDeathZone)
            {
                RotateHorizontally(_rotationSpeedCoefficient * Time.deltaTime * movementInput.normalized.x);
            }
            
            Vector3 moveDirection = Vector3.zero;
            if (Mathf.Abs(movementInput.y) > _movementJoyStickDeathZone)
            {
                moveDirection = _movementSpeedCoefficient * Time.deltaTime *  movementInput.normalized.y * transform.forward.normalized;
            }

            SetVerticalVelocity();

            moveDirection.y = _verticalVelocity * Time.deltaTime;
            _controller.Move(moveDirection);
        }

        private void SetVerticalVelocity()
        {
            if (_controller.isGrounded && _isJumping)
            {
                _verticalVelocity = _jumpForce;
                _isJumping = false;
            }
            else
            {
                _verticalVelocity += _gravityValue * Time.deltaTime;
            }
        }

        private Vector2 GetMovementInput()
        {
            Vector2 joystickMovementInput = _input.Movement.MovementJoyStick.ReadValue<Vector2>();
            return joystickMovementInput;
        }

        private void JumpCallback(InputAction.CallbackContext context)
        {
            Jump();
        }

        public void Jump()
        {
            if (_controller.isGrounded)
            {
                _isJumping = true;
            }
        }
    }   
}
