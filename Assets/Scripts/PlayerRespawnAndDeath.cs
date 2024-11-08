using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerInput))]
    internal class PlayerRespawnAndDeath : MonoBehaviour
    {
        [SerializeField] private float _noMoveTimeAfterSpawnSeconds = 2.5f; 
        [SerializeField] private Animator _animator;

        private PlayerInput _playerInput;
        private bool _isDead = false;
        private Coroutine _currentRespawnCoroutine = null;
        
        internal UnityEvent Death { get; } = new UnityEvent();
        internal UnityEvent Spawn { get; } = new UnityEvent();
        internal UnityEvent SpawnEnds { get; } = new UnityEvent();
        
        
        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            
            _playerInput.EnabledPlayerActions.Death.Die.performed += Die;
            _playerInput.EnabledPlayerActions.Spawn.Respawn.performed += Respawn;
            
            Respawn();
        }


        private void Die(InputAction.CallbackContext context)
        {
            if (_isDead)
            {
                return;
            }
            
            _isDead = true;
            
            StopCoroutine(_currentRespawnCoroutine);
            
            Death?.Invoke();
        }

        private void Respawn(InputAction.CallbackContext context)
        {
            Respawn();
        }

        private void Respawn()
        {
            _isDead = false;
            
            Spawn?.Invoke();
            StopCoroutine(_currentRespawnCoroutine);
            _currentRespawnCoroutine = StartCoroutine(Respawn2CanMoveTimer());
        }
        
        private IEnumerator Respawn2CanMoveTimer()
        {
            yield return new WaitForSeconds(_noMoveTimeAfterSpawnSeconds);
            SpawnEnds?.Invoke();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }

}
