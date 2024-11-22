using System.Collections;
using UnityEngine;

namespace Level
{
    public class VerticalWallMovement : SmoothMovement
    {
        [SerializeField] private Vector2 _movementYRange = new Vector2(-3f, 3f);
        
        public void Move(float targetOffsetY)
        {
            targetOffsetY = Mathf.Clamp(targetOffsetY, _movementYRange.x, _movementYRange.y);

            StopAllCoroutines();
            StartCoroutine(StartMovingTo(targetOffsetY));
        }
    }
}
