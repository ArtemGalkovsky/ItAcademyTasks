using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerDie : MonoBehaviour
    {
        [SerializeField] private float _playerMaxLinearVelocityBeforeDeath = -50f;
        
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_rigidbody.linearVelocity.y < _playerMaxLinearVelocityBeforeDeath)
            {
                Debug.Log("GG");
                Destroy(gameObject);
            }
        }
    }
}

