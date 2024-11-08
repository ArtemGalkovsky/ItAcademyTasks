using UnityEngine;

namespace Player
{
    public class PlayerConfigComponent : MonoBehaviour
    {
        [SerializeField] private Config.PlayerConfig _config;

        public Config.PlayerConfig PlayerCfg => _config;
    }
}

