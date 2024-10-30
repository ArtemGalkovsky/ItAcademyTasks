using UnityEngine;

namespace Interactable
{
    public class AmmoBox : Interactable
    {
        public override void Interact(GameObject interactor)
        {
            Player.PlayerGun playerGun = interactor.GetComponent<Player.PlayerGun>();

            if (playerGun == null)
            {
                Debug.LogError("Player doesn't have PlayerGunComponent!");
                return;
            }

            Scriptables.Guns.GunLogic gunLogic = playerGun.Gun.GetComponent<Scriptables.Guns.GunLogic>();

            if (gunLogic == null)
            {
                Debug.LogError("Gun doesn't have GunLogic component!");
                return;
            }

            gunLogic.ResetMagazines();
        }
    }
}

