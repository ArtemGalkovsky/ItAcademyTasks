using System;
using Player;
using UnityEngine;

namespace Enemies
{
    internal class CrabLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _crabShell;

        private void Awake()
        {
            if (_crabShell == null)
            {
                Debug.LogError("CrabLogic needs a crab shell!!!");
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out _) && _crabShell == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
