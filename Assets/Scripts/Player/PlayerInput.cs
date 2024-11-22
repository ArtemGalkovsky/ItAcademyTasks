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
            
            PlayerInputActions playerInputActions = new PlayerInputActions();
            playerInputActions.Enable(); 
            
            EnabledPlayerInputActions = playerInputActions;
        }

        private void OnDestroy()
        {
            EnabledPlayerInputActions?.Disable();
        }
    }        
}
    

