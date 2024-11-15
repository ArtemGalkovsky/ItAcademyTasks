using UnityEngine;

namespace Player
{
    internal class PlayerConfigComponent : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        public PlayerConfig PlayerConfig => _playerConfig;

        private void Awake()
        {
            if (_playerConfig == null)
            {
                Debug.LogError("PlayerConfig is null");
            }
        }
    }
}

