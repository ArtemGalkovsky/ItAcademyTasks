using System;
using UnityEngine;
using UnityEngine.Events;
using Player;

namespace Level
{
    [Serializable]
    public class PlatePressed : UnityEvent {}
    
    public class PlayerOnlyPlate : SmoothMovement
    {
        [SerializeField] private PlatePressed _onPlatePressed = new PlatePressed();
        [SerializeField] private bool _isOneUse = false;
        [SerializeField] private float _localYMovementOnPress = -0.3f;

        private bool _wasUsed = false;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerInput>(out _))
            {
                return;
            }
            
            if (_wasUsed && _isOneUse)
            {
                return;
            }
                
            _wasUsed = true;
            _onPlatePressed?.Invoke();
            
            StopAllCoroutines();
            StartCoroutine(StartMovingTo(_localYMovementOnPress));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerInput>(out _))
            {
                return;
            }

            if (_isOneUse && _wasUsed)
            {
                return;
            }
            
            StopAllCoroutines();
            StartCoroutine(StartMovingTo(0f));
        }
    }
}
