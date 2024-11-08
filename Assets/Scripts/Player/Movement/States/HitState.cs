using Player.Config;
using UnityEngine;

namespace Player.States
{
    internal class HitState : DefaultMovementState
    {
        private string[] _hitTriggersNames;
        private Animator _playerAnimator;
        private PlayerConfig _playerConfig;
        private StatesDataStorage _statesDataStorage;
        
        public HitState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
            _hitTriggersNames = statesDataStorage.Config.HitTriggersNames;
            _playerAnimator = statesDataStorage.Components.PlayerAnimator;
            _statesDataStorage = statesDataStorage;
            _playerConfig = statesDataStorage.Config;
        }

        public override void Enter()
        {
            base.Enter();
            
            if (_hitTriggersNames.Length <= 0)
            {
                Debug.LogError("Hit triggers not set");
            }
            else
            {
                StatesData.Components.PlayerAnimator.SetTrigger(_hitTriggersNames[Random.Range(0, _hitTriggersNames.Length)]);
            }
        }

        public override void Update(float deltaTime)
        {
            if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(_playerConfig.HitAnimationName) && _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                StatesData.Components.MovementStateMachine.TransitionToState(_statesDataStorage.PlayerMovementStates.Idle); 
            }
        }

        protected override void TryChangeState(IMovementState nextState)
        {
            if (_playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                StatesData.Components.MovementStateMachine.TransitionToState(nextState);      
            }
        }
    }
}