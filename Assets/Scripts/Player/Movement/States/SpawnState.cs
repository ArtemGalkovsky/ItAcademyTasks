using System.Collections;
using UnityEngine;

namespace Player.States
{
    internal class SpawnState : DefaultMovementState
    {
        private float _timeSinceSpawnStarts = 0f;
        
        public SpawnState(StatesDataStorage statesDataStorage) : base(statesDataStorage)
        {
        }

        public override void Enter()
        {
            StatesData.Components.PlayerAnimator.SetTrigger(StatesData.Config.SpawnTriggerName);
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            _timeSinceSpawnStarts += deltaTime;
            
            if (_timeSinceSpawnStarts >= StatesData.Config.SpawnTimeSeconds)
            {
                StatesData.Components.MovementStateMachine.TransitionToState(StatesData.PlayerMovementStates.Idle);   
            }
        }

        protected override void TryChangeState(IMovementState nextState)
        {
        }
    }
}