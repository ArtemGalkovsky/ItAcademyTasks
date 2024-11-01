using UnityEngine;

namespace Player
{
    public class Input : MonoBehaviour
    {
        public static PlayerInput EnabledPlayerInput;
    
        void Awake()
        {

        }

        public static void Initialize()
        {
            if (EnabledPlayerInput == null)
            {
                PlayerInput input = new PlayerInput();
                input.Enable();
            
                EnabledPlayerInput = input;    
            }
        }

        private void OnDestroy()
        {
            EnabledPlayerInput?.Disable();
        }

        public static Vector2 GetMouseDelta()
        {
            return EnabledPlayerInput.Rotation.Rotation.ReadValue<Vector2>();
        }
    }
}

