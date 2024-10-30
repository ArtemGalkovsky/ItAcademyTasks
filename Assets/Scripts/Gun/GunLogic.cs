using System;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using Scriptables.Ammo;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using PlayerInput = Player.PlayerInput;

namespace Scriptables.Guns
{
    public class GunLogic : MonoBehaviour
    {
        [SerializeField] private DefaultGun _gun;
        [SerializeField] private Transform _ammoSpawnLocation;
        [SerializeField] private DefaultAmmo _initialAmmo;
        
        public static UnityEvent<DefaultGun, DefaultMagazine, MagazinesStates> StateUpdated { get; } = new UnityEvent<DefaultGun, DefaultMagazine, MagazinesStates>();
        public bool CanFireNow => _canFireNow;
        
        private DefaultMagazine _currentMagazine;
        private bool _canFireNow = true;
        
        private void Start()
        {
            PlayerInput.EnabledPlayerInputActions.Fire.Fire.performed += Fire;
            PlayerInput.EnabledPlayerInputActions.Reload.Reload.performed += Reload;

            if (_gun.MagazineTypes.Length <= 0)
            {
                Debug.LogError("Magazines array is empty!");
            }
            
            _gun.SetupGun();
            SwapAmmoTo(_initialAmmo);
        }

        private void Fire(InputAction.CallbackContext context)
        {
            if (!_canFireNow)
            {
                return;
            }

            MagazinesStates state = _gun.Fire(_currentMagazine, _ammoSpawnLocation);
            
            if (state == MagazinesStates.OnFireTime)
            {
                _canFireNow = false;
                StateUpdated?.Invoke(_gun, _currentMagazine, state);
                StartCoroutine(WaitUntilFiringTimeoutEnds());
            }
            
            StateUpdated?.Invoke(_gun, _currentMagazine, state);
        }

        private IEnumerator WaitUntilFiringTimeoutEnds()
        {
            if (_currentMagazine.GetMagazinesState() != MagazinesStates.HasAmmo)
            {
                yield return new WaitForSeconds(_currentMagazine.FireRateSeconds);
                StateUpdated?.Invoke(_gun, _currentMagazine, _currentMagazine.GetMagazinesState());
            }
            
            _canFireNow = true;
        }

        public void ResetMagazines()
        {
            foreach (DefaultMagazine magazine in _gun.MagazineTypes)
            {
                magazine.ResetMagazine();                
            }
            
            StateUpdated?.Invoke(_gun, _currentMagazine, _currentMagazine.GetMagazinesState());
        }

        public void SwapAmmoTo(DefaultAmmo ammo)
        {
            if (!_canFireNow)
            {
                return;
            }

            DefaultMagazine foundMagazine = FindMagazineByAmmoType(ammo);

            if (foundMagazine == null)
            {
                Debug.LogError($"Gun has no magazine {ammo.name}");
                return;
            }
            
            _currentMagazine = foundMagazine;
            
            StateUpdated?.Invoke(_gun, _currentMagazine, _currentMagazine.GetMagazinesState());
        }

        [CanBeNull]
        public DefaultMagazine FindMagazineByAmmoType(DefaultAmmo ammo)
        {
            DefaultMagazine foundMagazine = _gun.MagazineTypes.FirstOrDefault(magazine => magazine.AmmoType.GetType() == ammo.GetType());

            if (foundMagazine == null)
            {
                Debug.LogError($"Gun has no magazine {ammo.name}");
            }
            
            return foundMagazine;
        }
        
        private void Reload(InputAction.CallbackContext context)
        {
            if (!_canFireNow)
            {
                return;
            }
            
            MagazinesStates newState = _currentMagazine.ReloadMagazine();
            StateUpdated?.Invoke(_gun, _currentMagazine, newState);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}

