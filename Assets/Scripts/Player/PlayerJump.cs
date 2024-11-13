using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(Collider))]
    internal class PlayerJump : MonoBehaviour
    {
        [Header("Jumping")]
        [SerializeField] private float _stairMaxHeightCanClimbMeters = 1.2f;
        [SerializeField] private float _autoJumpCoolDownSeconds = 0.2f;
        [SerializeField] private float _jumpForce = 6f;
        [SerializeField] private float _gravityY = Physics.gravity.y;
        
        [Header("Jump Checking")]
        [SerializeField] private float _maxStairsCheckDistance = 0.3f;
        [SerializeField] private float _groundCheckRadius = 0.3f;
        [SerializeField] private LayerMask _groundLayerMask;

        private PlayerInput _playerInput;
        private PlayerActions _playerActions;
        private Rigidbody _rigidbody;
        private Collider _collider;
        
        private bool _isAlreadyAtStair = false;
        private float _previousJumpTime = float.MinValue;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            
            _playerInput = GetComponent<PlayerInput>();
            
            _playerInput.Initialize();
            _playerActions = _playerInput.EnabledPlayerInputActions;
            
            _playerActions.Movement.Jump.performed += Jump;
        }

        private void FixedUpdate()
        {
            float currentPlayerMovementInputHorizontal = _playerActions.Movement.Move.ReadValue<Vector2>().normalized.y;
            bool isStairAtFront = IsNewStairAtFront();
            
            if (isStairAtFront && AmIGrounded() && currentPlayerMovementInputHorizontal > 0 
                && Time.time >= _previousJumpTime + _autoJumpCoolDownSeconds)
            {
                _previousJumpTime = Time.time;
                Jump();
            } 
        }

        private void Jump(InputAction.CallbackContext context)
        {
            Jump();
        }

        private void Jump()
        {
            if (AmIGrounded())
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);   
            }
        }

        internal bool AmIGrounded()
        {
            Vector3 sphereCastOrigin = new Vector3(transform.position.x, _collider.bounds.min.y, transform.position.z);
            
            return Physics.CheckSphere(sphereCastOrigin, _groundCheckRadius, _groundLayerMask, QueryTriggerInteraction.Ignore);
        }
        
        private bool IsNewStairAtFront()
        {
            Vector3 feetRayOrigin = transform.position;
            feetRayOrigin.y = _collider.bounds.min.y + 0.001f;

            Vector3 stairMaxHeightRayOrigin = feetRayOrigin + Vector3.up * _stairMaxHeightCanClimbMeters;

            bool wasHitFromFit = Physics.Raycast(feetRayOrigin, transform.forward, out RaycastHit hitFromFeet, _maxStairsCheckDistance);
            bool wasntHitFromBody = !Physics.Raycast(stairMaxHeightRayOrigin, transform.forward, out RaycastHit hitFromBody, _maxStairsCheckDistance);
            
            if (wasHitFromFit && wasntHitFromBody && !_isAlreadyAtStair)
            {
                _isAlreadyAtStair = true;
                return true;
            }

            _isAlreadyAtStair = false;
            return false;
        }
    }
}

