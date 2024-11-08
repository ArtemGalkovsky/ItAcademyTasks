using UnityEngine;
using UnityEngine.Events;

namespace Player.States
{
    [RequireComponent(typeof(StatesDataStorage), typeof(CharacterController))]
    internal class PlayerMovementStateMachine : MonoBehaviour
    {
        public IMovementState CurrentState { get; private set; }
        public UnityEvent<IMovementState, StatesDataStorage> StateChanged { get; } = new UnityEvent<IMovementState, StatesDataStorage>();
        
        private StatesDataStorage _statesDataStorage;
        
        private void Start()
        {
            StatesDataStorage statesData = GetComponent<StatesDataStorage>();
            _statesDataStorage = statesData;
            statesData.Initialize();

            TransitionToState(statesData.PlayerMovementStates.Spawn);
        }

        public void TransitionToState(IMovementState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
            
            StateChanged?.Invoke(CurrentState, _statesDataStorage);
        }

        private void FixedUpdate()
        {
            CurrentState?.Update(Time.fixedDeltaTime);
        }
    }
}

