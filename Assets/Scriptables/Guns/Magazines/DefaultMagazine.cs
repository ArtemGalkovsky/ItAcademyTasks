using System;
using UnityEngine;

namespace Scriptables.Guns
{
    [Serializable]
    public class DefaultMagazine : ScriptableObject, IMagazine
    {
        [SerializeField] protected Scriptables.Ammo.DefaultAmmo _ammo;
        [SerializeField] protected float _fireRateSeconds = 0.1f;
        [SerializeField] protected int _magazineSize = 10;
        [SerializeField] protected int _magazinesNumber = 10;
        [SerializeField] protected int _usedAmmoPerFire = 1;
        
        protected int MagazinesLeft;
        protected int AmmoInCurrentMagazine;
        
        public Scriptables.Ammo.IDefaultAmmo AmmoType => _ammo;
        public int MagazinesLeftInGun => MagazinesLeft;
        public int AmmoInCurrentMagazineLeftInGun => AmmoInCurrentMagazine;
        public float FireRateSeconds => _fireRateSeconds;
        public int MagazineAmmoSize => _magazineSize;
        public int MagazinesNumberInGun => _magazinesNumber;
        public int UsedAmmoPerFire => _usedAmmoPerFire;
        
        public virtual void ResetMagazine()
        {
            MagazinesLeft = _magazinesNumber - 1;
            AmmoInCurrentMagazine = _magazineSize;
        }

        public virtual MagazinesStates ReloadMagazine()
        {
            MagazinesStates currentMagazinesState = GetMagazinesState();

            if (currentMagazinesState == MagazinesStates.ResetNeeded || MagazinesLeft <= 0)
            {
                return MagazinesStates.ResetNeeded;
            }
            
            MagazinesLeft--;
            AmmoInCurrentMagazine = _magazineSize;
            
            return GetMagazinesState();
        }

        public virtual bool UseAmmoIfCan()
        {
            MagazinesStates currentMagazinesState = GetMagazinesState();
            
            if (currentMagazinesState != MagazinesStates.HasAmmo)
            {
                return false;
            } else if (AmmoInCurrentMagazine - _usedAmmoPerFire < 0)
            {
                return false;
            }
            
            AmmoInCurrentMagazine -= _usedAmmoPerFire;
            return true;
        }

        public virtual MagazinesStates GetMagazinesState()
        {
            if (MagazinesLeft <= 0 && AmmoInCurrentMagazine <= 0)
            {
                return MagazinesStates.ResetNeeded;
            }
            else if (AmmoInCurrentMagazine <= 0)
            {
                return MagazinesStates.ReloadNeeded;
            }

            return MagazinesStates.HasAmmo;
        }
    }
}


