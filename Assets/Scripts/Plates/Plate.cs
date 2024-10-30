using System;
using Scriptables.Ammo;
using Scriptables.Guns;
using UnityEngine;

namespace Plates
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] private DefaultAmmo _ammoToSeOnPlayer;

        private void OnCollisionEnter(Collision other)
        {
            Player.PlayerGun playerGun = null;

            if (other.gameObject.TryGetComponent<Player.PlayerGun>(out playerGun))
            {
                playerGun.Gun.GetComponent<GunLogic>().SwapAmmoTo(_ammoToSeOnPlayer);
            }
        }
    }
}

