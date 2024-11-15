using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerVerticalMovementBrain), typeof(CharacterController))]
    internal class PlayerGravity : MonoBehaviour
    {
        [SerializeField] private float _gravityY = Physics.gravity.y;
        
        private PlayerVerticalMovementBrain _verticalMovementBrain;
        private CharacterController _characterController;
        private float _summaryGravityY = 0f;
        
        private void Awake()
        {
            _verticalMovementBrain = GetComponent<PlayerVerticalMovementBrain>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_characterController.isGrounded)
            {
                _summaryGravityY = -0.1f;
                return;
            }
            
            _summaryGravityY += _gravityY * Time.deltaTime;
            
            _summaryGravityY = Mathf.Clamp(_summaryGravityY, _gravityY, 0f);
            
            _verticalMovementBrain.MovementQueue.Enqueue(_summaryGravityY);
        }
    }
}