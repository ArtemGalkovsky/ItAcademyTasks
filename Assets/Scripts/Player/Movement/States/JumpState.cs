using UnityEngine;

namespace Player.States
{
    internal class JumpState : DefaultMovementState
    {
        private StatesDataStorage _statesDataStorage;
        private Animator _animator;
        
        public JumpState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
            _statesDataStorage = statesDataStorage;
            _animator = statesDataStorage.Components.PlayerAnimator;
        }

        public override void Enter()
        {
            _animator.SetTrigger(_statesDataStorage.Config.JumpTriggerName);
            
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            
        }
        
        protected override void TryChangeState(IMovementState nextState)
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_statesDataStorage.Config.JumpAnimationName))
            {
                StatesData.Components.MovementStateMachine.TransitionToState(nextState);
            }
        }
    }
}

