using System;

namespace Scriptables.Guns
{
    public interface IMagazine
    {
        public Scriptables.Ammo.IDefaultAmmo AmmoType { get; }
        public int MagazinesLeftInGun { get; }
        public int AmmoInCurrentMagazineLeftInGun { get; }
        public float FireRateSeconds { get; }
        public int MagazineAmmoSize { get; }
        public int MagazinesNumberInGun { get; }
        public int UsedAmmoPerFire { get; }

        public void ResetMagazine();
        public MagazinesStates ReloadMagazine();
        public bool UseAmmoIfCan();
        public MagazinesStates GetMagazinesState();
        
    }
}

