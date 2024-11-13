using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource), typeof(PlayerInput), typeof(PlayerJump))]
    internal class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _screamAudio;
        [SerializeField] private AudioClip _breathAudio;
        [SerializeField] private AudioClip _musicAudio;
        [SerializeField] private AudioClip _walkAudio;

        [SerializeField, Range(0f, 100f)] private float _breathChancePercents = 20f;
        [SerializeField, Range(0f, 100f)] private float _screamChancePercents = 5f;
        
        [SerializeField] private float _musicTimeBetweenScreamersSeconds = 3f;

        private AudioSource _audioSource;
        private PlayerInput _playerInput;
        private PlayerActions _playerInputActions;
        private PlayerJump _playerJump;
        private float _currentTimeSinceLastScream = 0f;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerInput = GetComponent<PlayerInput>();
            _playerJump = GetComponent<PlayerJump>();

            _playerInput.Initialize();
            _playerInputActions = _playerInput.EnabledPlayerInputActions;

            CheckChanceValidity();

            PlayBackgroundMusic();
        }

        private void CheckChanceValidity()
        {
            float totalChance = _breathChancePercents + _screamChancePercents;
            if (totalChance > 100f)
            {
                Debug.LogError("Сумма шансов аудиоклипов превышает 100%");
            }
        }

        private void FixedUpdate()
        {
            _currentTimeSinceLastScream += Time.fixedDeltaTime;
            
            if (IsPlayerMoving() && _playerJump.AmIGrounded() && (_audioSource.clip != _walkAudio || !_audioSource.isPlaying))
            {
                PlayClip(_walkAudio);
                return;
            }
            
            if (_currentTimeSinceLastScream > _musicTimeBetweenScreamersSeconds)
            {
                PlayRandomSound();
                _currentTimeSinceLastScream = 0f;
            }
            
            if (!_audioSource.isPlaying && _audioSource.clip != _musicAudio)
            {
                PlayBackgroundMusic();
            }
        }

        private void PlayRandomSound()
        {
            float randomValue = Random.Range(0f, 100f);
            float cumulativeChance = 0f;

            cumulativeChance += _screamChancePercents;
            if (randomValue < cumulativeChance)
            {
                PlayClip(_screamAudio);
                return;
            }

            cumulativeChance += _breathChancePercents;
            if (randomValue < cumulativeChance)
            {
                PlayClip(_breathAudio);
            }
        }

        private bool IsPlayerMoving()  // TODO: find another place to check this
        {
            return _playerInputActions.Movement.Move.ReadValue<Vector2>().y != 0;
        }

        private void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.loop = false; // Звуки проигрываются один раз
            _audioSource.Play();
        }

        private void PlayBackgroundMusic()
        {
            _audioSource.clip = _musicAudio;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
