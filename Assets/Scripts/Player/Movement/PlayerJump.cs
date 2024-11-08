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
        private FinalMovementBrain _finalMovementBrain;
        private CharacterController _characterController;
        private PlayerConfig _playerConfig;
        private Action<float> _jumpAction = null;
        
        private bool _jumpPeakGotten = false;
        private float _sumVelocityY = 0;
        
        private void Awake()
        {
            _stateMachine = GetComponent<PlayerMovementStateMachine>();
            _stateMachine.StateChanged.AddListener(OnStateChanged);
        }

        private void OnStateChanged(IMovementState newState, StatesDataStorage statesDataStorage)
        {
            _statesDataStorage = statesDataStorage;
            _playerConfig = statesDataStorage.Config;
            _characterController = statesDataStorage.Components.PlayerCharacterController;
            _finalMovementBrain = statesDataStorage.Components.PlayerFinalMovementBrain;
            
            if (newState is JumpState)
            {
                _jumpPeakGotten = false;
                StartJump();
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
            if (_sumVelocityY >= _playerConfig.MaxJumpVelocity)
            {
                _jumpPeakGotten = true;
            }
            
            if (_jumpPeakGotten && _characterController.isGrounded)
            {
                _sumVelocityY = 0f;
                _jumpPeakGotten = false;
                _stateMachine.TransitionToState(_statesDataStorage.PlayerMovementStates.Idle);
            }
            else if (!_jumpPeakGotten)
            {
                _sumVelocityY += deltaTime * _playerConfig.MaxJumpVelocity;
                
                _finalMovementBrain.AddMovementToQueue(new QueueMovementComponent((_playerConfig.MaxJumpVelocity - _sumVelocityY) * Vector3.up, "Jump"));   
            }
        }

        private void StartJump()
        {
            if (!_characterController.isGrounded)
            {
                _stateMachine.TransitionToState(_statesDataStorage.PlayerMovementStates.Idle);
            }
        }
        
        private void OnDestroy()
        {
            _stateMachine?.StateChanged.RemoveListener(OnStateChanged);
        }
    }
}

