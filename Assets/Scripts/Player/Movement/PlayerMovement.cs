using System;
using UnityEngine;
using Player.States;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(StatesDataStorage), typeof(PlayerMovementStateMachine))]
    internal class PlayerMovement : MonoBehaviour
    {
        private StatesDataStorage _statesDataStorage;
        private PlayerMovementStateMachine _stateMachine;
        private PlayerInputActions _movementInputAction;
        private FinalMovementBrain _finalMovementBrain;
        private Action<Vector2> _movementAction = null;
        
        private Transform _playerTransform;
        private float _rotationSpeedCoefficient;
        private float _movementSpeedCoefficient;
        
        private void Awake()
        {
            _statesDataStorage = GetComponent<StatesDataStorage>();
            _stateMachine = GetComponent<PlayerMovementStateMachine>();
            _stateMachine.StateChanged.AddListener(OnStateChanged);
        }

        private void OnStateChanged(IMovementState newState, StatesDataStorage statesDataStorage)
        {
            _finalMovementBrain = statesDataStorage.Components.PlayerFinalMovementBrain;
            _movementInputAction = statesDataStorage.Components.PlayerInputComponent.EnabledPlayerActions;
            _playerTransform = statesDataStorage.Components.PlayerTransform;
            _rotationSpeedCoefficient = statesDataStorage.Config.RotationSpeedCoefficient;
            _movementSpeedCoefficient = statesDataStorage.Config.MovementSpeedCoefficient;
            
            if (newState is RunState)
            {
                _movementAction = MoveCharacterController;
            } else if (newState is TurnLeftState || newState is TurnRightState)
            {
                _movementAction = (inputMovement) => Rotate(inputMovement.x);
            }
            else
            {
                _movementAction = null;
            }
        }

        private void FixedUpdate()
        {
            if (_movementAction == null)
            {
                return;
            }
            
            Vector2 inputMovement = GetMovement(Time.fixedDeltaTime);
            
            if (inputMovement == Vector2.zero)
            {
                _statesDataStorage.Components.MovementStateMachine.TransitionToState(_statesDataStorage.PlayerMovementStates.Idle);
                return;
            }
            
            _movementAction?.Invoke(inputMovement);    
        }

        private void MoveCharacterController(Vector2 inputMovement)
        {
            _finalMovementBrain.AddMovementToQueue(inputMovement.y * _movementSpeedCoefficient * _playerTransform.forward);
            Rotate(inputMovement.x);
        }

        private void Rotate(float inputRotationMovement)
        {
            _finalMovementBrain.AddRotationToQueue(Quaternion.Euler(0f, inputRotationMovement * _rotationSpeedCoefficient, 0f));
        }
        
        private Vector2 GetMovement(float deltaTime)
        {
            Vector2 movement = _movementInputAction.Movement.Move.ReadValue<Vector2>();
            
            return deltaTime * movement;
        }
        
        private void OnDestroy()
        {
            _stateMachine?.StateChanged.RemoveListener(OnStateChanged);
        }
    }
}

