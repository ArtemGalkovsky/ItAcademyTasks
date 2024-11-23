using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spawner
{
    [RequireComponent(typeof(Spawner))]
    internal class SpawnerInput : MonoBehaviour
    {
        public SpawnerInputActions EnabledSpawnerInputActions { get; private set; }
        private Spawner _spawner;
        
        private void Awake()
        {
            Initialize();
            _spawner = GetComponent<Spawner>();
        }

        public void Initialize()
        {
            if (EnabledSpawnerInputActions != null)
            {
                return;
            }
            
            var actions = new SpawnerInputActions();
            actions.Enable();

            EnabledSpawnerInputActions = actions;
            
            SetBindings();
        }

        private void SetBindings()
        {
            EnabledSpawnerInputActions.Spawner.Spawn.performed += SpawnRandomObject;
        }

        private void SpawnRandomObject(InputAction.CallbackContext _)
        {
            _spawner.SpawnRandomObject();    
        }
        
        private void OnDestroy()
        {
            EnabledSpawnerInputActions?.Disable();
        }
    }

}
