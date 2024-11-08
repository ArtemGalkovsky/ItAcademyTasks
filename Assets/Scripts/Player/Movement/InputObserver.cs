using System;
using UnityEngine;
using UnityEngine.Events;
using Player.States;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(StatesDataStorage), typeof(PlayerMovementStateMachine))]
    internal class InputObserver : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private StatesDataStorage _statesDataStorage; 
        private PlayerInputActions _playerInputActions;
        private PlayerMovementStateMachine _playerMovementStateMachine;

        private Vector2 _currentMovement = Vector2.zero;
        private bool _isInitialized = false;
        private bool _isJumpingNow = false;

        public UnityEvent<IMovementState> ChangeStateTo { get; } = new UnityEvent<IMovementState>();

        private void Start()
        {
            _playerMovementStateMachine = GetComponent<PlayerMovementStateMachine>();
            _playerMovementStateMachine.StateChanged.AddListener(OnStateMachineStateChanged);
        }

        private void Initialize(StatesDataStorage statesDataStorage)
        {
            _isInitialized = true;
            
            _statesDataStorage = statesDataStorage;
            
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.Initialize();

            PlayerInputActions playerInputActions = _playerInput.EnabledPlayerActions;
            _playerInputActions = playerInputActions;

            playerInputActions.Death.Die.performed += (_) => ChangeState(_statesDataStorage.PlayerMovementStates.Death);
            playerInputActions.Spawn.Respawn.performed += (_) => ChangeState(_statesDataStorage.PlayerMovementStates.Spawn);
            playerInputActions.Jump.Jump.performed += (_) => ChangeState(_statesDataStorage.PlayerMovementStates.Jump);
            playerInputActions.Hit.Hit.performed += (_) => ChangeState(_statesDataStorage.PlayerMovementStates.Hit);
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            _currentMovement = _playerInputActions.Movement.Move.ReadValue<Vector2>();

            if (_currentMovement == Vector2.zero)
            {
                ChangeState(_statesDataStorage.PlayerMovementStates.Idle);
            }
            else if (CheckIfRotatingOnly())
            {
                ChangeState(_currentMovement.x > 0 ? _statesDataStorage.PlayerMovementStates.TurnRight : _statesDataStorage.PlayerMovementStates.TurnLeft);
            }
            else if (_currentMovement != Vector2.zero)
            {
                ChangeState(_statesDataStorage.PlayerMovementStates.Run);
            }
        }

        private bool CheckIfRotatingOnly()
        {
            return _currentMovement.y == 0f && _currentMovement.x != 0f;
        }

        private void OnStateMachineStateChanged(IMovementState newState, StatesDataStorage statesDataStorage)
        {
            if (!_isInitialized)
            {
                Initialize(statesDataStorage);   
            }
        }

        private void ChangeState(IMovementState newState)
        {
            if (newState != _playerMovementStateMachine.CurrentState)
            {
                ChangeStateTo?.Invoke(newState);   
            }
        }
    }
}
