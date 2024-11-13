using UnityEngine;

namespace Player
{
    internal class PlayerInput : MonoBehaviour
    {
        private PlayerActions _enabledPlayerInputActions;
        internal PlayerActions EnabledPlayerInputActions => _enabledPlayerInputActions;
        
        private void Awake()
        {
            Initialize();
        }

        internal void Initialize()
        {
            if (_enabledPlayerInputActions == null)
            {
                PlayerActions playerActions = new PlayerActions();  
                playerActions.Enable();
                
                _enabledPlayerInputActions = playerActions;
            }
        }
        
        internal Vector2 GetRotationDelta()
        {
            return _enabledPlayerInputActions.Movement.Look.ReadValue<Vector2>();
        }

        private void OnDestroy()
        {
            _enabledPlayerInputActions?.Disable();
        }
    }
}



