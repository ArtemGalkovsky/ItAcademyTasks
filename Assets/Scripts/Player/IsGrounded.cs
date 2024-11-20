using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class IsGrounded : MonoBehaviour
    {
        [SerializeField] private float _raycastDistanceOnGround = 0.2f;
        [SerializeField] private float _raycastDistanceOnSlope = 2f;
        [SerializeField] private Vector3 _raycastOffsetFromPlayerCenter = Vector3.down * 0.5f;

        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public bool IsPlayerGrounded()
        {
            Vector3 rayDirection = Vector3.down;
            float rayDistance = _raycastDistanceOnGround;

            if (_playerMovement.IsOnSlope())
            {
                Vector3 slopeNormal = _playerMovement.SlopeHit.normal;
                rayDirection = Vector3.ProjectOnPlane(Vector3.up, slopeNormal).normalized;
                rayDirection.y = -rayDirection.y;
                
                rayDistance = _raycastDistanceOnSlope;
            }
            
            Debug.DrawRay(transform.position + _raycastOffsetFromPlayerCenter, rayDirection * rayDistance, Color.red);
            return Physics.Raycast(transform.position + _raycastOffsetFromPlayerCenter, rayDirection, out _, rayDistance);
        }
    }
}
