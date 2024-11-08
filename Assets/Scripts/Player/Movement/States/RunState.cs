using UnityEngine;
using UnityEngine.InputSystem;


namespace Player.States
{
    internal class RunState : DefaultMovementState
    {
        private InputAction _movementInputAction;
        
        public RunState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents) : base(playerConfig, playerComponents)
        {
        }
        
        public override void Enter()
        {
            _movementInputAction = Components.PlayerInputComponent.EnabledPlayerActions.Movement.Move;
            
            Components.PlayerAnimator.SetTrigger(Config.MoveTriggerName);
            
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            Vector2 inputMovement = GetMovement(deltaTime); // TODO: rename variable.

            if (inputMovement == Vector2.zero)
            {
                Components.MovementStateMachine.TransitionToState(Components.StorageStates.Idle);
                return;
            }
            
            Move(inputMovement.y);
            Rotate(inputMovement.x * Config.RotationSpeedCoefficient);
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private void Move(float movementInputMovement)
        {
            Vector3 movement = movementInputMovement * Config.MovementSpeedCoefficient * Components.PlayerTransform.forward; 
            
            movement.y = Config.GravityY;
            Components.PlayerCharacterController.Move(movement);
        }

        private void Rotate(float rotationAngleWithSpeed)
        {
            Components.PlayerTransform.Rotate(0f, rotationAngleWithSpeed, 0f);   
        }

        private Vector2 GetMovement(float deltaTime)
        {
            Vector2 movement = _movementInputAction.ReadValue<Vector2>();
            
            return deltaTime * movement;
        }
        
        protected override void TryChangeState(IMovementState nextState)
        {
            Components.MovementStateMachine.TransitionToState(nextState);
        }
    }
}

