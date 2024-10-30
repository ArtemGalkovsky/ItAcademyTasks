using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerActions EnabledPlayerInputActions;

        private void Awake()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (EnabledPlayerInputActions == null)
            {
                PlayerActions playerActions = new PlayerActions();  
                playerActions.Enable();
                
                EnabledPlayerInputActions = playerActions;
            }
        }
        
        public static Vector2 GetRotationDelta()
        {
            return EnabledPlayerInputActions.Movement.Look.ReadValue<Vector2>();
        }

        private void OnDestroy()
        {
            EnabledPlayerInputActions?.Disable();
        }
    }
}



