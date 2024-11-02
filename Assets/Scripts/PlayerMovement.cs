using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(Animator), typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerRespawnAndDeath))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Header("Movement")] [SerializeField] private float _movementSpeedCoefficient = 10f;
        [SerializeField] private float _rotationSpeedCoefficient = 10f;
        [SerializeField] private float _jumpVelocity = 0.4f;
        [SerializeField] private float _fallingGravityMultiplier = 0.2f;
        [SerializeField, Range(0f, 1f)] private float _movementStateAnimatorStandValue = 0f;
        [SerializeField, Range(0f, 1f)] private float _movementStateAnimatorRunValue = 1f;

        [Header("Animator Config")]
        [SerializeField] private string _animatorMovementParameterName = "Movement";
        [SerializeField] private string _animatorJumpParameterName = "Jumping";
            
        private readonly float _gravity = Physics.gravity.y;
        private CharacterController _characterController;   
        private PlayerInputActions _playerInputActions;
        private PlayerRespawnAndDeath _playerRespawnAndDeath;
        private bool _isJumping = false;
        private float _verticalVelocity = 0f;
        private bool _canMoveNow = false;
        
        private void Awake()
        {
            Spawn();
            _characterController = GetComponent<CharacterController>();
            _playerRespawnAndDeath = GetComponent<PlayerRespawnAndDeath>();
            
            _playerRespawnAndDeath.Death.AddListener(Die);
            _playerRespawnAndDeath.Spawn.AddListener(Spawn);
            _playerRespawnAndDeath.CanMoveNow.AddListener(EnableMovement);
            
            PlayerInput playerInput = GetComponent<PlayerInput>();
            playerInput.Initialize();
            _playerInputActions = playerInput.EnabledPlayerActions;
            
            _playerInputActions.Jump.Jump.performed += Jump;
        }

        private void Update()
        {
            Vector2 timedInputMovement = GetMovement(); // TODO: rename variable.

            Move(timedInputMovement);
            Rotate(timedInputMovement.x * _rotationSpeedCoefficient);
        }

        private void Spawn()
        {
            _canMoveNow = false;
        }

        private void Die()
        {
            _canMoveNow = false;
        }

        private void EnableMovement()
        {
            _canMoveNow = true;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_characterController.isGrounded && _canMoveNow)
            {
                _isJumping = true;
                _animator.SetBool(_animatorJumpParameterName, true);
            }
        }
        
        private void SetVerticalVelocity()
        {
            if (_isJumping && _canMoveNow)
            {
                _verticalVelocity = Mathf.Clamp(_verticalVelocity + _jumpVelocity - (_jumpVelocity * Time.deltaTime), 0, _jumpVelocity);

                if (_verticalVelocity >= _jumpVelocity)
                {
                    _isJumping = false;
                    _animator.SetBool(_animatorJumpParameterName, false);
                }
            }
            else if (_verticalVelocity <= _gravity)
            {
                _verticalVelocity = _gravity;
            }
            else
            {
                _verticalVelocity += _gravity * Time.deltaTime * _fallingGravityMultiplier;
            }
        }

        private void Move(Vector2 inputMovement)
        {
            Vector3 movement;
            
            if (_canMoveNow)
            {
                ChangeSpeedOnAnimator(inputMovement.y == 0 ? _movementStateAnimatorStandValue : _movementStateAnimatorRunValue);
                movement = inputMovement.y * _movementSpeedCoefficient * transform.forward; 
            }
            else
            {
                movement = Vector3.zero;
            }
            
            
            SetVerticalVelocity();
            movement.y = _verticalVelocity;
            _characterController.Move(movement);
        }

        private void Rotate(float rotationAngleWithSpeed)
        {
            transform.Rotate(0f, rotationAngleWithSpeed, 0f);
        }

        private Vector2 GetMovement()
        {
            Vector2 movement = _playerInputActions.Movement.Move.ReadValue<Vector2>();
            
            return Time.deltaTime * movement;
        }

        private void ChangeSpeedOnAnimator(float inputMovementMovement)
        {
            _animator.SetFloat(_animatorMovementParameterName, inputMovementMovement);
        }
    }
}

