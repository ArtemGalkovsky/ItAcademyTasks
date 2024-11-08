using UnityEngine;
using UnityEngine.Events;

namespace Player.States
{
    [RequireComponent(typeof(StatesStorage), typeof(CharacterController))]
    internal class PlayerMovementStateMachine : MonoBehaviour
    {
        public IMovementState CurrentState { get; private set; }
        
        private void Start()
        {
            StatesStorage states = GetComponent<StatesStorage>();
            states.Initialize();
            
            CurrentState = states.Spawn;
            CurrentState.Enter();
        }

        public void TransitionToState(IMovementState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        private void FixedUpdate()
        {
            CurrentState?.Update(Time.fixedDeltaTime);
        }
    }
}

