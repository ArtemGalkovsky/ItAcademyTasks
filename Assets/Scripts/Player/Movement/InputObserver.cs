using UnityEngine;
using UnityEngine.Events;
using Player.States;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(States.StatesStorage))]
    internal class InputObserver : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private StatesStorage _statesStorage; 
        private PlayerInputActions _playerInputActions;

        private Vector2 _currentMovement = Vector2.zero;
        private bool _isMoving = false;
        private bool _isRotating = false;
        
        public UnityEvent<IMovementState> ChangeStateTo { get; }= new UnityEvent<States.IMovementState>();
        
        private void Awake()
        {
            _statesStorage = GetComponent<States.StatesStorage>();
            
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.Initialize();

            PlayerInputActions playerInputActions = _playerInput.EnabledPlayerActions;
            _playerInputActions = playerInputActions;
            
            playerInputActions.Death.Die.performed += (_) => ChangeState(_statesStorage.Death);
            playerInputActions.Spawn.Respawn.performed += (_) => ChangeState(_statesStorage.Spawn);
            
            playerInputActions.Jump.Jump.performed += (_) => ChangeState(_statesStorage.Jump);
        }

        private void Update()
        {
            _currentMovement = _playerInputActions.Movement.Move.ReadValue<Vector2>();
            
            Debug.Log(_currentMovement);
            if (_currentMovement.y == 0f && _currentMovement.x != 0f && !_isRotating)
            {
                _isRotating = true;
                
                if (_currentMovement.y > 0f)
                {   
                    ChangeState(_statesStorage.TurnRight);
                } else if (_currentMovement.y < 0f)
                {
                    ChangeState(_statesStorage.TurnLeft);
                }
            }
            else if (_currentMovement != Vector2.zero && !_isMoving)
            {
                _isRotating = false;
                _isMoving = true;
                ChangeState(_statesStorage.Run);
            }
            else if (_currentMovement == Vector2.zero)
            {
                _isRotating = false;
                ChangeState(_statesStorage.Idle);
                _isMoving = false;
            }
            else
            {
                _isRotating = false;
            }
        }

        private void ChangeState(IMovementState newState)
        {
            ChangeStateTo?.Invoke(newState);
        }
    }
}

