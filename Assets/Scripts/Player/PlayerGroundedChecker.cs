using System;
using UnityEngine;

namespace Player
{
    internal class PlayerGroundedChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _canJumpFromLayerMask;
        [SerializeField] private float _raycastRadiusLength = 0.2f;
        [SerializeField] private Vector3 _raycastOffsetFromPlayerCenter = Vector3.down;

        public bool CheckIfGrounded()
        {
            if (Physics2D.OverlapCircle(transform.position + _raycastOffsetFromPlayerCenter, _raycastRadiusLength, _canJumpFromLayerMask))
            {
                return true;
            }

            return false;
        }
    }
}

