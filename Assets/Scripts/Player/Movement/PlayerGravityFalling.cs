using System;
using Player.Config;
using UnityEngine;
using Player.States;

namespace Player
{
    [RequireComponent(typeof(StatesDataStorage), typeof(PlayerMovementStateMachine))]
    internal class PlayerGravityFalling : MonoBehaviour
    {
        private PlayerMovementStateMachine _stateMachine;
        private CharacterController _characterController;
        private FinalMovementBrain _finalMovementBrain;
        private PlayerConfig _playerConfig;
        
        private float _gravityY;
        private float _fallingMultiplier;
        private bool _isInitialized = false;
        private float _currentGravityY;
        
        private void Awake()
        {
            _stateMachine = GetComponent<PlayerMovementStateMachine>();
            _stateMachine.StateChanged.AddListener(OnStateChanged);
        }

        private void OnStateChanged(IMovementState newState, StatesDataStorage statesDataStorage)
        {
            _finalMovementBrain = statesDataStorage.Components.PlayerFinalMovementBrain;
            _gravityY = statesDataStorage.Config.GravityY;
            _currentGravityY = _gravityY;
            _fallingMultiplier = statesDataStorage.Config.FallingGravityMultiplier;
            _characterController = statesDataStorage.Components.PlayerCharacterController;
            
            _isInitialized = true;
        }

        private void FixedUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }

            if (_characterController.velocity.y > 0)
            {
                _currentGravityY = 0f;
            }
            else
            {
                _currentGravityY += _gravityY * Time.fixedDeltaTime * _fallingMultiplier;
            }

            
            
            _finalMovementBrain.AddMovementToQueue(new QueueMovementComponent(_currentGravityY * Vector3.up, "Gravity"));
        }
        
        private void OnDestroy()
        {
            _stateMachine?.StateChanged.RemoveListener(OnStateChanged);
        }
    }
}

