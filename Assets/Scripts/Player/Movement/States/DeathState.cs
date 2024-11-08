using UnityEngine;

namespace Player.States
{
    internal class DeathState : DefaultMovementState
    {
        public DeathState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents) : base(playerConfig, playerComponents)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Components.PlayerAnimator.SetTrigger(Config.DeathTriggerName);
        }

        public override void Update(float deltaTime)
        {
        }

        public override void Exit()
        {
            base.Exit();
        }

        protected override void TryChangeState(IMovementState nextState)
        {
            AnimatorStateInfo animatorStateInfo = Components.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
            if (nextState is not DeathState && nextState is SpawnState && animatorStateInfo.IsName(Config.DeathAnimationName) && animatorStateInfo.normalizedTime > 1f)
            {
                Components.MovementStateMachine.TransitionToState(nextState);
            }
        }
    }
}

