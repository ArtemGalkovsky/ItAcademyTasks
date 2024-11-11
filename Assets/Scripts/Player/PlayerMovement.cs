using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementDirection = 1f;
        public UnityEvent<float> PlayerDirectionChanged { get; } = new UnityEvent<float>();
        private PlayerActions _playerActions;

        private void Awake()
        {
            _playerActions = new PlayerActions();
            
            _playerActions.Enable();
            _playerActions.Movement.ChangeDirection.performed += (_) =>
            {
                _movementDirection *= -1;
                PlayerDirectionChanged?.Invoke(_movementDirection);
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            };
        }

    private void OnDestroy()
        {
            _playerActions?.Disable();
        }
    }
}

