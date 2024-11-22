using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
    internal class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        
        private PlayerInputActions _playerInputActions;
        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            PlayerInput input = GetComponent<PlayerInput>();
            
            input.Initialize();
            _playerInputActions = input.EnabledPlayerInputActions;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float inputMovement = _playerInputActions.Movement.Move.ReadValue<float>();
            float finalMovement = inputMovement * Time.fixedDeltaTime * _speed;
            
            _rigidbody2D.linearVelocity = new Vector2(finalMovement, _rigidbody2D.linearVelocity.y);
        }
    }
}

