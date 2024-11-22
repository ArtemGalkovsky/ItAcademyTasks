using System;
using Player;
using UnityEngine;

namespace Game
{
    internal class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameEndController _gameEndController;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private GameObject _level;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private PlayerHealthVisualizer _playerHealthVisualizer;
        
        private void Awake()
        {
            PlayerHealth playerHealth = null;
            
            if (_player == null)
            {
                Debug.Log("Player is null");
            }
            else
            {
                playerHealth = Instantiate(_player, _playerSpawnPoint.position, Quaternion.identity);

                if (_gameEndController == null)
                {
                    Debug.LogError("Game end controller is null");
                }
                else
                {
                    playerHealth.SetGameEndController(_gameEndController);   
                }
            }

            if (_level == null)
            {
                Debug.Log("Level is null");
            }
            else
            {
                Instantiate(_level, Vector3.zero, Quaternion.identity);
            }

            if (_playerHealthVisualizer == null)
            {
                Debug.Log("PlayerHealthVisualizer is null");
            }
            else
            {
                _playerHealthVisualizer.SetPlayerHealthComponent(playerHealth);    
            }
        }
    }
}

