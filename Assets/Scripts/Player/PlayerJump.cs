using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]    
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 3f;
        [SerializeField] private float _groundCheckRadius = 0.3f;
        [SerializeField] private float _groundCheckOffset = 1f;
        [SerializeField] private LayerMask _groundLayerMask;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            PlayerInput.EnabledPlayerInputActions.Movement.Jump.performed += Jump;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (AmIGrounded())
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        private bool AmIGrounded()
        {
            Vector3 sphereCastOrigin = transform.position + Vector3.down * _groundCheckOffset;
            
            return Physics.CheckSphere(sphereCastOrigin, _groundCheckRadius, _groundLayerMask, QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 sphereCastOrigin = transform.position + Vector3.down * _groundCheckOffset;
            Gizmos.DrawWireSphere(sphereCastOrigin, _groundCheckRadius);
        }
    }
}

