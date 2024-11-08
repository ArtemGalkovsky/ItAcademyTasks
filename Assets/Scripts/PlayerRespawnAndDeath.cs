using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerInput))]
<<<<<<< Updated upstream
    public class PlayerRespawnAndDeath : MonoBehaviour
    {
        [SerializeField] private string _animatorDeathParameterName = "Death";
        [SerializeField] private string _animatorSpawnParameterName = "Spawn";
        [SerializeField] private string _animatorCanMoveNowParameterName = "CanMove";
        [SerializeField] private float _noMoveTimeAfterSpawnSeconds = 2.5f; 
        [SerializeField] private Animator _animator;
        
        private PlayerInputActions _playerInputActions;
        private bool _isDead = false;
        
        public UnityEvent Death { get; } = new UnityEvent();
        public UnityEvent Spawn { get; } = new UnityEvent();
        public UnityEvent CanMoveNow { get; } = new UnityEvent();
        
        private void Start()
        {
            _animator.SetBool(_animatorCanMoveNowParameterName, false);
            
            PlayerInput playerInput = GetComponent<PlayerInput>();
            playerInput.Initialize();
            _playerInputActions = playerInput.EnabledPlayerActions;

            _playerInputActions.Death.Die.performed += Die;
            _playerInputActions.Spawn.Respawn.performed += Respawn;
=======
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
>>>>>>> Stashed changes
            
            Respawn();
        }


        private void Die(InputAction.CallbackContext context)
        {
            if (_isDead)
            {
                return;
            }
            
            _isDead = true;
            
<<<<<<< Updated upstream
            StopAllCoroutines();
            
            Death?.Invoke();
            _animator.SetTrigger(_animatorDeathParameterName);
            _animator.SetBool(_animatorCanMoveNowParameterName, false);
=======
            StopCoroutine(_currentRespawnCoroutine);
            
            Death?.Invoke();
>>>>>>> Stashed changes
        }

        private void Respawn(InputAction.CallbackContext context)
        {
            Respawn();
        }

        private void Respawn()
        {
            _isDead = false;
            
            Spawn?.Invoke();
<<<<<<< Updated upstream
            _animator.SetTrigger(_animatorSpawnParameterName);
            _animator.SetBool(_animatorCanMoveNowParameterName, false);
            
            StopCoroutine(Respawn2CanMoveTimer());
            StartCoroutine(Respawn2CanMoveTimer());
=======
            StopCoroutine(_currentRespawnCoroutine);
            _currentRespawnCoroutine = StartCoroutine(Respawn2CanMoveTimer());
>>>>>>> Stashed changes
        }
        
        private IEnumerator Respawn2CanMoveTimer()
        {
            yield return new WaitForSeconds(_noMoveTimeAfterSpawnSeconds);
<<<<<<< Updated upstream
            _animator.SetBool(_animatorCanMoveNowParameterName, true);
            
            CanMoveNow?.Invoke();
=======
            SpawnEnds?.Invoke();
>>>>>>> Stashed changes
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }

}
