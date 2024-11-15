using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerVerticalMovementBrain), typeof(CharacterController)),
    RequireComponent(typeof(PlayerController))]
    internal class PlayerGravity : MonoBehaviour
    {
        [SerializeField] private float _gravityY = Physics.gravity.y;
        [SerializeField] private float _fallingMultiplier = 0.2f;
        
        private PlayerVerticalMovementBrain _verticalMovementBrain;
        private CharacterController _characterController;
        private PlayerController _playerController;
        private bool _applyGravity = true;
        private float _summaryGravityY = 0f;
        
        private void Awake()
        {
            _verticalMovementBrain = GetComponent<PlayerVerticalMovementBrain>();
            _characterController = GetComponent<CharacterController>();
            
            _playerController = GetComponent<PlayerController>();
            _playerController.PlayerStateChanged += OnStateUpdated;
        }

        private void Update()
        {
            if (!_applyGravity)
            {
                return;
            }
            
            if (_characterController.isGrounded)
            {
                _summaryGravityY = -0.1f;
                _verticalMovementBrain.MovementQueue.Enqueue(_summaryGravityY);
                return;
            }
            
            _summaryGravityY += _gravityY * Time.deltaTime * _fallingMultiplier;
            
            _summaryGravityY = Mathf.Clamp(_summaryGravityY, _gravityY, 0f);
            
            _verticalMovementBrain.MovementQueue.Enqueue(_summaryGravityY);
        }

        private void OnStateUpdated(PlayerStates state, object data)
        {
            _summaryGravityY = 0f;
            _applyGravity = state is not PlayerStates.Jumping;
        }
    }
}