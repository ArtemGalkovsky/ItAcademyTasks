using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerController))]
    internal class PlayerHorizontalMovementBrain : MonoBehaviour
    {
        private CharacterController _characterController;
        private PlayerController _playerController;

        private bool _canMoveNow = false;
        
        public Queue<Vector2> MovementQueue { get; }= new Queue<Vector2>();
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerController = GetComponent<PlayerController>();

            _playerController.PlayerStateChanged += OnStateUpdated;
        }

        private void LateUpdate()
        {
            Vector2 finalMovement = Vector3.zero;

            while (MovementQueue.Count > 0)
            {
                Vector2 movement = MovementQueue.Dequeue();
                
                if (_canMoveNow == false)
                {
                    continue;   
                }
                
                finalMovement += movement;
            }
            
            _characterController.Move(finalMovement.y * transform.forward);
            transform.Rotate(finalMovement.x * Vector3.up);
        }

        private void OnStateUpdated(PlayerStates state, object data)
        {
            switch (state)
            {
                case PlayerStates.RespawningStart:
                case PlayerStates.Dead:
                case PlayerStates.Hit:
                    _canMoveNow = false;
                    break;
                default:
                    _canMoveNow = true;
                    break;
            }
        }
    }
}

