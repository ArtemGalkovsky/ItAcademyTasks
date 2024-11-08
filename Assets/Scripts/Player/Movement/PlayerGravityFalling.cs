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
        private FinalMovementBrain _finalMovementBrain;
        private PlayerConfig _playerConfig;

        private bool _isNowFalling = true;
        private float _gravityY;
        private float _fallingMultiplier;
        
        private void Awake()
        {
            _stateMachine = GetComponent<PlayerMovementStateMachine>();
            _stateMachine.StateChanged.AddListener(OnStateChanged);
        }

        private void OnStateChanged(IMovementState newState, StatesDataStorage statesDataStorage)
        {
            _finalMovementBrain = statesDataStorage.Components.PlayerFinalMovementBrain;
            _gravityY = statesDataStorage.Config.GravityY;
            _fallingMultiplier = statesDataStorage.Config.FallingGravityMultiplier;
            
            if (newState is JumpState)
            {
                _isNowFalling = false;
            }
            else
            {
                _isNowFalling = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_isNowFalling)
            {
                return;
            }
   
            _finalMovementBrain.AddMovementToQueue(_fallingMultiplier * _gravityY * Vector3.up);
        }
        
        private void OnDestroy()
        {
            _stateMachine?.StateChanged.RemoveListener(OnStateChanged);
        }
    }
}

