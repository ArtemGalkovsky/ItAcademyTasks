using UnityEngine;

namespace Player
{
    internal class PlayerInput : MonoBehaviour
    {  
        public PlayerInputActions EnabledPlayerInputActions { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (EnabledPlayerInputActions != null)
            {
                return;
            }
            
            var playerActions = new PlayerInputActions();
            playerActions.Enable();
            
            EnabledPlayerInputActions = playerActions;
        }

        public Vector2 GetMousePositionDelta()
        {
            return EnabledPlayerInputActions.Rotation.Rotate.ReadValue<Vector2>();
        }

        private void OnDestroy()
        {
            EnabledPlayerInputActions?.Disable();
        }
    }
}
