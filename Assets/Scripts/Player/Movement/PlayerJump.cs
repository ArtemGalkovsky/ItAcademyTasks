using System;
using Player.Config;
using UnityEngine;
using Player.States;

namespace Player
{
    [RequireComponent(typeof(StatesDataStorage), typeof(PlayerMovementStateMachine))]
    internal class PlayerJump : MonoBehaviour
    {
        private StatesDataStorage _statesDataStorage;
        private PlayerMovementStateMachine _stateMachine;
        private CharacterController _characterController;
        private PlayerConfig _playerConfig;
        private Action<float> _jumpAction = null;

        private bool _isJumping = false;
        private float _currentYVelocity = 0f;
        private bool _jumpPeakGotten = false;
        
        private void Awake()
        {
            _stateMachine = GetComponent<PlayerMovementStateMachine>();
            _stateMachine.StateChanged.AddListener(OnStateChanged);
        }

        private void OnStateChanged(IMovementState newState, StatesDataStorage statesDataStorage)
        {
            _statesDataStorage = statesDataStorage;
            _characterController = statesDataStorage.Components.PlayerCharacterController;

            if (newState is JumpState)
            {
                _jumpAction = JumpUpdate;
            }
            else
            {
                _jumpAction = null;
            }
        }

        private void FixedUpdate()
        {
            if (_jumpAction == null)
            {
                return;
            }
            
            _jumpAction?.Invoke(Time.fixedDeltaTime);    
        }

        
        private void JumpUpdate(float deltaTime)
        {
            _currentYVelocity = _characterController.velocity.y;
            
            if (_currentYVelocity >= _playerConfig.JumpVelocity)
            {
                _jumpPeakGotten = true;
                _isJumping = false;
            } else if (_jumpPeakGotten && _currentYVelocity > _playerConfig.GravityY)
            {
                _currentYVelocity -= _playerConfig.GravityY * deltaTime * _playerConfig.FallingGravityMultiplier;
            }
            else if (_characterController.isGrounded && !_isJumping)
            {
                _statesDataStorage.Components.MovementStateMachine.TransitionToState(_statesDataStorage.PlayerMovementStates.Idle);
            }

            _currentYVelocity += _playerConfig.JumpVelocity - (_playerConfig.JumpVelocity * deltaTime);
        }

        private void StartJump()
        {
            if (_characterController.isGrounded)
            {
                _isJumping = true;
            }
        }
        
        private void OnDestroy()
        {
            _stateMachine?.StateChanged.RemoveListener(OnStateChanged);
        }
    }
}

