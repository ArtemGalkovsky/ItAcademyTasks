using Scriptables.Guns;
using UnityEngine;

namespace Player
{
    public class PlayerGun : MonoBehaviour
    {
        [SerializeField] private GameObject _playersGun;

        public GameObject Gun => _playersGun;
    }
}

