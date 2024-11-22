using System;
using Player;
using UnityEngine;

namespace Enemies
{
    internal class CrabShell : MonoBehaviour
    {
        [SerializeField] private string _redBallTag = "RedBall";
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _hitCooldownSeconds = 1f;

        private float _lastHitTime = 0f;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_redBallTag))
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth) && Time.time - _lastHitTime > _hitCooldownSeconds)
            {
                playerHealth.TakeDamage(_damage);
                _lastHitTime = Time.time;
            }
        }
    }
    
}

