using UnityEngine;

namespace Player.States
{
    internal abstract class DefaultMovementState : IMovementState
    {
        protected Config.PlayerConfig Config;
        protected PlayerInputActions PlayerActions;
        protected PlayerMovementStateMachine MovementStateMachine;
        protected PlayerComponents Components;
        
        protected DefaultMovementState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents)
        {
            Components = playerComponents;
            MovementStateMachine = playerComponents.MovementStateMachine;
            Config = playerConfig;

            PlayerInput playerInput = Components.PlayerInputComponent;
            playerInput.Initialize();
            PlayerActions = playerInput.EnabledPlayerActions; ;
        }

        public virtual void Enter()
        {
            Components.PlayerInputObserver.ChangeStateTo.AddListener(TryChangeState);
        }
        public abstract void Update(float deltaTime);

        public virtual void Exit()
        {
            Components.PlayerInputObserver.ChangeStateTo.RemoveListener(TryChangeState);
        }
        
        protected abstract void TryChangeState(IMovementState nextState);
    }
}