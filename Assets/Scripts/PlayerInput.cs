using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputActions EnabledPlayerActions {get; private set;}

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (EnabledPlayerActions != null)
            {
                return;
            }
            
            PlayerInputActions playerActions = new PlayerInputActions();   
            playerActions.Enable();
        
            EnabledPlayerActions = playerActions;
        }

        private void OnDestroy()
        {
            EnabledPlayerActions?.Disable();
        }
    }

}
