using UnityEngine;

namespace Background
{
    public class MovementBackgroundSpriteProperties : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _speed = 1f;
        public float BackgroundSpeed => _speed;
    }
}

