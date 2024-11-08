using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Player.States
{
    internal class IdleState : DefaultMovementState
    {
        public IdleState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents) : base(playerConfig, playerComponents)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Components.PlayerAnimator.SetTrigger(Config.IdleTriggerName);
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
            if (nextState is not IdleState)
            {
                Components.MovementStateMachine.TransitionToState(nextState);   
            }
        }
    }
}