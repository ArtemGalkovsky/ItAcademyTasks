using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
    internal class PlayerController : MonoBehaviour
    {
        public PlayerStates PlayerCurrentState { get; private set; }
        
        private PlayerInput _playerInput;
        private CharacterController _characterController;
        
        public delegate void StateHandler(PlayerStates state, object data);
        public event StateHandler PlayerStateChanged;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.Initialize();

            _playerInput.EnabledPlayerInputActions.Spawn.Spawn.performed += Spawn;
            _playerInput.EnabledPlayerInputActions.Death.Die.performed += _ => PlayerStateChanged?.Invoke(PlayerStates.Dead, null);
            _playerInput.EnabledPlayerInputActions.Jump.Jump.performed += JumpIfCan;
            _playerInput.EnabledPlayerInputActions.Hit.Hit.performed += Hit;
            SpawnPlayer();

            PlayerStateChanged += StateUpdated;
        }

        private void StateUpdated(PlayerStates state, object data)
        {
            PlayerCurrentState = state;
        }

        private void Update()
        {
            Vector2 movement = _playerInput.EnabledPlayerInputActions.Movement.Move.ReadValue<Vector2>();
            if (movement.sqrMagnitude > 0)
            {
                if (PlayerCurrentState is PlayerStates.Idling or PlayerStates.RespawningEnd)
                {
                    PlayerStateChanged?.Invoke(PlayerStates.Moving, movement.y > 0 ? 1 : -1);
                }
            } else if (PlayerCurrentState == PlayerStates.Moving)
            {
                PlayerStateChanged?.Invoke(PlayerStates.Idling, null);
            }
        }

        private void Hit(InputAction.CallbackContext context)
        {
            if (PlayerCurrentState is PlayerStates.Moving or PlayerStates.Idling or PlayerStates.RespawningEnd)
            {
                PlayerStateChanged?.Invoke(PlayerStates.Hit, null);
            }
        }

        private void JumpIfCan(InputAction.CallbackContext context)
        {
            if (_characterController.isGrounded && PlayerCurrentState is PlayerStates.Moving or PlayerStates.RespawningEnd or PlayerStates.Idling)
            {
                PlayerStateChanged?.Invoke(PlayerStates.Jumping, null);
            }
        }
        
        private void Spawn(InputAction.CallbackContext context)
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (PlayerCurrentState is PlayerStates.Dead)
            {
                PlayerStateChanged?.Invoke(PlayerStates.RespawningStart, null);
            }
        }

        private void OnSpawnEnds()
        {
            PlayerStateChanged?.Invoke(PlayerStates.RespawningEnd, null);
        }

        private void OnJumpEnds()
        {
            PlayerStateChanged?.Invoke(PlayerStates.Idling, null);
        }

        private void OnHitEnds()
        {
            PlayerStateChanged?.Invoke(PlayerStates.Idling, null);
        }
    }
}
