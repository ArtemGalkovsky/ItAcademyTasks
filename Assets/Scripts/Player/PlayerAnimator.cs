using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerController), typeof(PlayerConfigComponent))]
    internal class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private float _movementTransitionSpeed = 1f;
        
        private Animator _animator;
        private PlayerController _playerController;
        private PlayerConfig _playerConfig;

        private float _currentMovementValue = 1f;
        private float _targetMovementValue = 1f;

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
        }

        private void OnStateUpdated(PlayerStates state, object data)
        {
            _targetMovementValue = 1f;
            
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
                        _targetMovementValue = movementDirection switch
                        {
                            1 => 2,
                            -1 => 0,
                            _ => 1
                        };
                    }
                    else
                    {
                        Debug.LogWarning("Moving state received invalid data.");
                    }
                    break;

                case PlayerStates.Jumping:
                    _animator.SetTrigger(_playerConfig.JumpTriggerName);
                    break;
            }
        }
    }
}
