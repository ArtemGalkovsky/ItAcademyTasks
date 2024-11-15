using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerVerticalMovementBrain), typeof(CharacterController), typeof(PlayerController))]
    internal class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float _jumpMotion = 10f;
        
        private PlayerVerticalMovementBrain _verticalMovementBrain;
        private PlayerController _playerController;
        private bool _isJumping = false;
        private float _summaryJumpMotion = 0f;
        private bool _isPeakGotten = false;
        
        private void Awake()
        {
            _verticalMovementBrain = GetComponent<PlayerVerticalMovementBrain>();
            _playerController = GetComponent<PlayerController>();

            _playerController.PlayerStateChanged += OnStateChanged;
        }

        private void Update()
        {
            if (_isJumping)
            {
                UpdateJumpMotion();
            }
        }

        private void UpdateJumpMotion()
        {
            if (_isPeakGotten)
            {
                return;
            }
            
            _summaryJumpMotion += _jumpMotion * Time.deltaTime;
            
            _summaryJumpMotion = Mathf.Clamp(_summaryJumpMotion, 0f, _jumpMotion);

            if (_summaryJumpMotion >= _jumpMotion)
            {
                _isPeakGotten = true;
            }
            
            _verticalMovementBrain.MovementQueue.Enqueue(_jumpMotion - _summaryJumpMotion);
        }

        private void OnStateChanged(PlayerStates state, object data)
        {
            if (state is not PlayerStates.Jumping)
            {
                _summaryJumpMotion = 0f;
                _isJumping = false;
                _isPeakGotten = false;
                return;
            }

            _isJumping = true;
            UpdateJumpMotion();
        }
    }
}

