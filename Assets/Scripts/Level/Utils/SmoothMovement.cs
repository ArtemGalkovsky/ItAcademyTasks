using System;
using System.Collections;
using UnityEngine;

namespace Level
{
    public abstract class SmoothMovement : MonoBehaviour
    {
        [SerializeField] protected float _movementSpeed = 0.3f;
        
        protected float TimeSinceTargetChanged = 0f;
        protected Vector3 InitialPosition;

        protected virtual void Awake()
        {
            InitialPosition = transform.position;
        }

        protected IEnumerator StartMovingTo(float targetYOffset)
        {
            while (TimeSinceTargetChanged < 1.0f)
            {
                yield return new WaitForEndOfFrame();

                TimeSinceTargetChanged += Time.deltaTime * _movementSpeed;
                TimeSinceTargetChanged = Mathf.Clamp01(TimeSinceTargetChanged);

                float newY = Mathf.MoveTowards(transform.position.y, InitialPosition.y + targetYOffset,
                    TimeSinceTargetChanged);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}

