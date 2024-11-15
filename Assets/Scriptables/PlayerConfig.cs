using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    internal class PlayerConfig : ScriptableObject
    {
        [Header("Animator")]
        public string JumpTriggerName = "Jump";
        public string SpawnTriggerName = "Spawn"; 
        public string DeathTriggerName = "Death"; 
        public string HitIndexIntName = "HitIndex";
        public string HitTriggerName = "Hit";
        public string MovementStateFloatName = "MovementState";
    }
}

