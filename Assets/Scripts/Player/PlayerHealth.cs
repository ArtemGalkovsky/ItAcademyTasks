using System;
using Game;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _initialHealth = 100f;
        
        private GameEndController _gameEndController;
        private float _currentHealth;

        public Action<float> PlayerHealthChanged;
        public float MaxHealth { get; private set; }
        
        private void Awake()
        {
            _currentHealth = _initialHealth;
            MaxHealth = _initialHealth;
        }

        public void SetGameEndController(GameEndController gameEndController)
        {
            _gameEndController = gameEndController;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            PlayerHealthChanged?.Invoke(_currentHealth);
            
            CheckIfDead();
        }

        private void CheckIfDead()
        {
            if (_gameEndController == null)
            {
                Debug.Log("Game end controller is null");
                return;
            }
            
            if (_currentHealth <= 0)
            {
                _gameEndController.EndGame();   
            }
        }
    }
}

