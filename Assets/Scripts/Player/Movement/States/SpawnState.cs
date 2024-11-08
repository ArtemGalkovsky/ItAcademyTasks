using System.Collections;
using UnityEngine;

namespace Player.States
{
    internal class SpawnState : DefaultMovementState
    {
        private float _timeSinceSpawnStarts = 0f;
        
        public SpawnState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents) : base(playerConfig, playerComponents)
        {
        }

        public override void Enter()
        {
            Components.PlayerAnimator.SetTrigger(Config.SpawnTriggerName);
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            _timeSinceSpawnStarts += deltaTime;
            
            if (_timeSinceSpawnStarts >= Config.SpawnTimeSeconds)
            {
                MovementStateMachine.TransitionToState(Components.StorageStates.Idle);   
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        protected override void TryChangeState(IMovementState nextState)
        {
        }
    }
}