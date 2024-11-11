using System;
using Background;
using Player;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private BackgroundParallaxMovement _backgroundParallaxMovement;

        private void Awake()
        {
            if (_playerMovement == null)
            {
                Debug.LogError("PlayerMovement is null");
            }

            if (_backgroundParallaxMovement == null)
            {
                Debug.LogError("BackgroundParallaxMovement is null");
            }
            
            _playerMovement.PlayerDirectionChanged.AddListener(OnPlayerChangedDirection);
        }

        private void OnPlayerChangedDirection(float direction)
        {
            _backgroundParallaxMovement.SetMovementDirection(direction);
        }
    }
}

