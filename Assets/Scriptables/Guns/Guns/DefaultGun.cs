using System;
using System.Linq;
using UnityEngine;

namespace Scriptables.Guns
{
    public class DefaultGun : ScriptableObject, IDefaultGun
    {
        [SerializeField] private DefaultMagazine[] _magazineTypes;

        protected float _previousFireTime;

        public DefaultMagazine[] MagazineTypes => _magazineTypes;
        public float PreviousFireTime => _previousFireTime;

        public virtual void SetupGun()
        {
            _previousFireTime = Time.time;
            
            foreach (DefaultMagazine magazine in _magazineTypes)
            {
                magazine.ResetMagazine();
            }
        }

        public override string ToString()
        {
            return "DefaultGun";
        }
        
        public virtual MagazinesStates Fire(DefaultMagazine magazine, Transform gunTransform)
        {
            if (!CheckIfMagazineTypeInCurrentGunMagazineTypes(magazine))
            {
                Debug.LogError($"Incorrect magazine type was sent to Fire() method of {this.name}");
                return MagazinesStates.GunDoesNotHaveThisMagazine;
            }

            return FireIfCan(magazine, gunTransform);
        }

        protected virtual MagazinesStates FireIfCan(DefaultMagazine magazine, Transform gunTransform)
        {
            MagazinesStates state = magazine.GetMagazinesState();
            
            if (Time.time - _previousFireTime >= magazine.FireRateSeconds)
            {
                _previousFireTime = Time.time;
                bool ammoUsed = magazine.UseAmmoIfCan();
                
                if (ammoUsed)
                {
                    InstantiateAmmo(magazine, gunTransform);
                }
            }
            else if (state == MagazinesStates.HasAmmo)
            {
                return MagazinesStates.OnFireTime;
            }
            
            return magazine.GetMagazinesState();
        }

        protected virtual void InstantiateAmmo(DefaultMagazine magazine, Transform gunTransform)
        {
            GameObject ammoPrefab = magazine.AmmoType.AmmoPrefab;
            
            GameObject instantiatedAmmo = Instantiate(ammoPrefab);
            instantiatedAmmo.transform.SetPositionAndRotation(gunTransform.position, gunTransform.rotation * ammoPrefab.transform.rotation);
            instantiatedAmmo.GetComponent<Rigidbody>().AddForce(gunTransform.forward * magazine.AmmoType.FiringForce);
        }

        protected virtual bool CheckIfMagazineTypeInCurrentGunMagazineTypes(IMagazine magazine)
        {
            Type magazineType = magazine.GetType();

            if (_magazineTypes.Any(selectedMagazine => selectedMagazine.GetType() == magazineType))
            {
                return true;
            }

            return false;
        }
    }
}


