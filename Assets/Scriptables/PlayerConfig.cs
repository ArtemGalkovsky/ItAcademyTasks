using UnityEngine;

namespace Player.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {   
        [Header("Movement")]
        public float RotationSpeedCoefficient = 10f;
        public float MovementSpeedCoefficient = 10f;
        public float JumpVelocity = 0.4f;
        public float FallingGravityMultiplier = 0.2f;
        public float GravityY = Physics.gravity.y;

        [Header("Spawn And Death")] 
        public float SpawnTimeSeconds = 2.3f;
        
        [Header("Animator")]
        public string SpawnTriggerName = "Spawn";
        public string DeathTriggerName = "Death";
        public string JumpTriggerName = "Jumping";
        public string TurnLeftTriggerName = "TurnLeft";
        public string TurnRightTriggerName = "TurnRight";
        public string MoveTriggerName = "Move";
        public string IdleTriggerName = "Idle";
        public string[] HitTriggersNames = new string[] {"Hit1", "Hit2", "Hit3", "Hit4", "Hit5"};
        
        [Header("Animations")]
        public string DeathAnimationName = "EllenDeath";
        public string SpawnAnimationName = "EllenSpawn";
        public string JumpAnimationName = "Jumping";
        public string HitAnimationName = "Hit";
        public string MovementAnimationName = "Movement";
        public string TurnLeftAnimationName = "EllenQuickTurnLeft";
        public string TurnRightAnimationName = "EllenQuickTurnRight";
        public string IdleAnimationName = "EllenIdle";
    }
}