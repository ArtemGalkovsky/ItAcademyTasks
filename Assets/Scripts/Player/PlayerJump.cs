using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D), typeof(PlayerGroundedChecker))]
    internal class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float _jumpImpulseForce = 10;

        private PlayerInputActions _playerInputActions;
        private PlayerGroundedChecker _playerGroundedChecker;
        private Rigidbody2D _rigidbody2D;
        
        
        private void Awake()
        {
            PlayerInput input = GetComponent<PlayerInput>();
            
            input.Initialize();
            _playerInputActions = input.EnabledPlayerInputActions;
            _playerInputActions.Jump.Jump.performed += Jump;
            
            _playerGroundedChecker = GetComponent<PlayerGroundedChecker>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_playerGroundedChecker.CheckIfGrounded())
            {
                _rigidbody2D.AddForce(_jumpImpulseForce * Vector2.up, ForceMode2D.Impulse);   
            }
        }
    }

}
