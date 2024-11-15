using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerController))]
    public class PlayerVerticalMovementBrain : MonoBehaviour
    {
        private CharacterController _characterController;
        private PlayerController _playerController;
        
        private bool _canFlyNow = false;
        public Queue<float> MovementQueue { get; }= new Queue<float>();

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerController = GetComponent<PlayerController>();

            _playerController.PlayerStateChanged += OnStateUpdated;
        }

        private void LateUpdate()
        {
            if (MovementQueue.Count <= 0)
            {
                return;
            }

            float finalMovementY = 0f;
            while (MovementQueue.Count > 0)
            {
                float movement = MovementQueue.Dequeue();

                if (movement < 0 && !_canFlyNow)
                {
                    finalMovementY += movement;
                }
            }
            _characterController.Move(finalMovementY * Vector3.up);
        }

        private void OnStateUpdated(PlayerStates state, object data)
        {
            switch (state)
            {
                case PlayerStates.Jumping:
                    _canFlyNow = true;
                    break;
                default:
                    _canFlyNow = false;
                    break;
            }
        }
    }
}

