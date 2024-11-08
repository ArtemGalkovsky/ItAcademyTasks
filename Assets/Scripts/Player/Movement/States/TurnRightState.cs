using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    internal class TurnRightState: DefaultMovementState
    {
        public TurnRightState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
        }

        public override void Enter()
        {
            StatesData.Components.PlayerAnimator.SetTrigger(StatesData.Config.TurnRightTriggerName);
            
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