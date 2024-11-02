using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerInput))]
    public class PlayerHit : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string[] _animatorHitAnimationNames;
        [SerializeField] private string _animatorMovementAnimationName = "Movement";
        
        private PlayerInputActions _playerInputActions;
    
        private void Awake()
        {
            PlayerInput playerInput = GetComponent<PlayerInput>();
            playerInput.Initialize();
            _playerInputActions = playerInput.EnabledPlayerActions;

            _playerInputActions.Hit.Hit.performed += Hit;

            if (_animatorHitAnimationNames.Length <= 0)
            {
                Debug.LogError("Animator Hit Animation Names has no animation names!");
            }
        }

        private void Hit(InputAction.CallbackContext context)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_animatorMovementAnimationName))
            {
                int randomIndex = Random.Range(1, _animatorHitAnimationNames.Length);
                _animator.SetTrigger(_animatorHitAnimationNames[randomIndex]);       
            }
        }
    }
}

