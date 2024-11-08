using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    internal class TurnLeftState : DefaultMovementState
    {
        public TurnLeftState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
        }

        public override void Enter()
        {
            StatesData.Components.PlayerAnimator.SetTrigger(StatesData.Config.TurnLeftTriggerName);
            
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