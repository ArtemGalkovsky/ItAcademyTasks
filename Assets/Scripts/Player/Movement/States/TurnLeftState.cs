using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.States
{
    internal class TurnLeftState : DefaultMovementState
    {
        private InputAction _movementInputAction;
        
        public TurnLeftState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents) : base(playerConfig, playerComponents)
        {
        }

        public override void Enter()
        {
            _movementInputAction = Components.PlayerInputComponent.EnabledPlayerActions.Movement.Move;
           
            Components.PlayerAnimator.SetTrigger(Config.TurnLeftTriggerName);
            
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            float rotationAngle = _movementInputAction.ReadValue<Vector2>().x * deltaTime;
            
            Components.PlayerTransform.Rotate(0f, rotationAngle * Config.RotationSpeedCoefficient, 0f);   
        }

        public override void Exit()
        {
            base.Exit();
        }

        protected override void TryChangeState(IMovementState nextState)
        {
            Components.MovementStateMachine.TransitionToState(nextState);
        }
    }
}