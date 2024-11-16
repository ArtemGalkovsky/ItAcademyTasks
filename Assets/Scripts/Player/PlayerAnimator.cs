using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerController), typeof(PlayerConfigComponent))]
    internal class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private float _movementTransitionSpeed = 1f;
        [SerializeField] private float _rotationTransitionSpeed = 1f;
        [SerializeField] private int[] _hitIndexes = { 1, 2, 3, 4 };
        
        private Animator _animator;
        private PlayerController _playerController;
        private PlayerConfig _playerConfig;

        private float _currentMovementValue = 0f;
        private float _currentRotationValue = 0f;
        private float _targetMovementValue = 0f;
        private float _targetRotationValue = 0f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerConfig = GetComponent<PlayerConfigComponent>().PlayerConfig;
            _playerController = GetComponent<PlayerController>();

            _playerController.PlayerStateChanged += OnStateUpdated;
        }

        private void Update()
        {
            _currentMovementValue = Mathf.MoveTowards(
                _currentMovementValue,
                _targetMovementValue,
                _movementTransitionSpeed * Time.deltaTime
            );
            
            _animator.SetFloat(_playerConfig.MovementStateFloatName, _currentMovementValue);
            
            _currentRotationValue = Mathf.MoveTowards(
                _currentRotationValue,
                _targetRotationValue,
                _rotationTransitionSpeed * Time.deltaTime
            );
            _animator.SetFloat(_playerConfig.RotationFloatName, _currentRotationValue);
        }

        private void OnStateUpdated(PlayerStates state, object data)
        {
            _targetMovementValue = 0f;
            _targetRotationValue = 0f;
            _animator.SetInteger(_playerConfig.HitIndexIntName, -1);
            _animator.SetFloat(_playerConfig.RotationFloatName, 0);
            _animator.SetBool(_playerConfig.RotationBoolName, false);
            
            switch (state)
            {
                case PlayerStates.Dead:
                    _animator.SetTrigger(_playerConfig.DeathTriggerName);
                    break;

                case PlayerStates.RespawningStart:
                    _animator.SetTrigger(_playerConfig.SpawnTriggerName);
                    break;

                case PlayerStates.Moving:
                    if (data is int movementDirection)
                    {
                        _targetMovementValue = movementDirection;
                    }
                    else
                    {
                        Debug.LogWarning("Moving state received invalid data.");
                    }
                    break;
                case PlayerStates.RotationOnSpot:
                    if (data is int rotationDirection)
                    {
                        _targetRotationValue = rotationDirection;
                    }
                    else
                    {
                        Debug.LogWarning("Rotation state received invalid data.");
                    }
                    break;
                case PlayerStates.Jumping:
                    _animator.SetTrigger(_playerConfig.JumpTriggerName);
                    break;
                case PlayerStates.Hit:
                    if (_hitIndexes.Length <= 0)
                    {
                        Debug.LogError("Hit indexes are emptry.");
                        break;
                    }
                    
                    _animator.SetInteger(_playerConfig.HitIndexIntName, _hitIndexes[Random.Range(0, _hitIndexes.Length)]);
                    _animator.SetTrigger(_playerConfig.HitTriggerName);
                    break;
            }
        }
    }
}
