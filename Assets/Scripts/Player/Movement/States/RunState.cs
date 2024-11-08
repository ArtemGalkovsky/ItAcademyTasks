using UnityEngine;
using UnityEngine.InputSystem;


namespace Player.States
{
    internal class RunState : DefaultMovementState
    {
        public RunState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
        }
        
        public override void Enter()
        {
            StatesData.Components.PlayerAnimator.SetTrigger(StatesData.Config.MoveTriggerName);
            
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
        }
        
        protected override void TryChangeState(IMovementState nextState)
        {
            StatesData.Components.MovementStateMachine.TransitionToState(nextState);
        }
    }
}

