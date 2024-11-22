using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(Slider))]
    public class PlayerHealthVisualizer : MonoBehaviour
    {
        private Slider _healthSlider;
        private PlayerHealth _playerHealth;
        
        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
            _healthSlider.value = 1;
        }

        public void SetPlayerHealthComponent(PlayerHealth playerHealth)
        {
            if (playerHealth == null)
            {
                Debug.LogError("PlayerHealth is null");
            }
            
            _playerHealth = playerHealth;
            _playerHealth.PlayerHealthChanged += ChangeHealthSlider;
        }

        private void ChangeHealthSlider(float health)
        {
            _healthSlider.value = health / _playerHealth.MaxHealth;
        }

        private void OnDestroy()
        {
            if (_playerHealth != null)
            {
                _playerHealth.PlayerHealthChanged -= ChangeHealthSlider;   
            }
        }
    }
}
