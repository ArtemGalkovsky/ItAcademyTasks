using UnityEngine;

namespace Player.States
{
    internal abstract class DefaultMovementState : IMovementState
    {
        protected PlayerInputActions PlayerActions;
        protected StatesDataStorage StatesData;
        
        protected DefaultMovementState(StatesDataStorage statesDataStorage)
        {
            StatesData = statesDataStorage;
            
            PlayerInput playerInput = statesDataStorage.Components.PlayerInputComponent;
            playerInput.Initialize();
            PlayerActions = playerInput.EnabledPlayerActions;
        }

        public virtual void Enter()
        {
            StatesData.Components.PlayerInputObserver.ChangeStateTo.AddListener(TryChangeState);
        }
        public abstract void Update(float deltaTime);

        public virtual void Exit()
        {
            StatesData.Components.PlayerInputObserver.ChangeStateTo.RemoveListener(TryChangeState);
        }
        
        protected abstract void TryChangeState(IMovementState nextState);
    }
}