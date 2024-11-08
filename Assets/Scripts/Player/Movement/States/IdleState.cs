namespace Player.States
{
    internal class IdleState : DefaultMovementState
    {
        public IdleState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StatesData.Components.PlayerAnimator.SetTrigger(StatesData.Config.IdleTriggerName);
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