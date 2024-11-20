using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Audio
{
    internal class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _playerMovementSource;
        [SerializeField] private AudioSource _breathSource;
        [SerializeField] private AudioSource _screamSource;
        
        [Header("Chances")]
        [SerializeField] private float _breathChance = 10f;
        [SerializeField] private float _screamChance = 3f;
        
        [Header("Other")]
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private Vector3 _screamMaxOffsetFromPlayer = Vector3.one * 10f;
        [SerializeField] private Vector3 _playerMovementStepOffset = Vector3.down * 0.5f;
        [SerializeField] private Vector2 _randomSoundCooldownRangeSeconds = new Vector2(1f, 15f);
        
        private IsGrounded _playerIsGroundedComponent;
        
        private void Awake()
        {
            StartCoroutine(WaitPlayRandomSound());

            if (_player == null)
            {
                Debug.LogError("Player is null");
            }
            
            _playerIsGroundedComponent = _player.GetComponent<IsGrounded>();

            if (_playerIsGroundedComponent == null)
            {
                Debug.LogError("No IsGrounded component found on player!");
            }
        }

        private IEnumerator WaitPlayRandomSound()
        {
            while (true)
            {
                float cooldown = Random.Range(_randomSoundCooldownRangeSeconds.x, _randomSoundCooldownRangeSeconds.y + 1);
            
                yield return new WaitForSeconds(cooldown);
                PlayRandomSound();   
            }
        }

        private void Update()
        {
            _musicSource.transform.position = _player.transform.position;
            
            if (_player.IsMoving() && !_playerMovementSource.isPlaying && _playerIsGroundedComponent.IsPlayerGrounded())
            {
                _playerMovementSource.transform.position = _player.transform.position + _playerMovementStepOffset;
                _playerMovementSource.Play();
            }
        }

        private void PlayRandomSound()
        {
            if (_breathChance <= 0 && _screamChance <= 0)
                return;
            
            float randomValue = Random.Range(1, 101);
            
            if (_breathChance > 0 && randomValue <= _breathChance && !_breathSource.isPlaying)
            {
                _breathSource.transform.position = _player.transform.position;
                _breathSource.Play();

                return;
            }
            
            if (_screamChance > 0 && randomValue <= _breathChance + _screamChance && !_screamSource.isPlaying)
            {
                _screamSource.transform.position = _player.transform.position + GetRandomPosition();
                _screamSource.Play();
            }  
        }

        private Vector3 GetRandomPosition()
        {
            float randomX = Random.Range(-_screamMaxOffsetFromPlayer.x, _screamMaxOffsetFromPlayer.x);
            float randomY = Random.Range(-_screamMaxOffsetFromPlayer.y, _screamMaxOffsetFromPlayer.y);
            float randomZ = Random.Range(-_screamMaxOffsetFromPlayer.z, _screamMaxOffsetFromPlayer.z);
            
            return new Vector3(randomX, randomY, randomZ);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}

