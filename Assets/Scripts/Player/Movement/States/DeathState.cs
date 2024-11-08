using UnityEngine;

namespace Player.States
{
    internal class DeathState : DefaultMovementState
    {
        public DeathState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StatesData.Components.PlayerAnimator.SetTrigger(StatesData.Config.DeathTriggerName);
        }

        public override void Update(float deltaTime)
        {
        }

        protected override void TryChangeState(IMovementState nextState)
        {
            AnimatorStateInfo animatorStateInfo = StatesData.Components.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
            if (nextState is not DeathState && nextState is SpawnState && animatorStateInfo.IsName(StatesData.Config.DeathAnimationName) && animatorStateInfo.normalizedTime > 1f)
            {
                StatesData.Components.MovementStateMachine.TransitionToState(nextState);
            }
        }
    }
}

