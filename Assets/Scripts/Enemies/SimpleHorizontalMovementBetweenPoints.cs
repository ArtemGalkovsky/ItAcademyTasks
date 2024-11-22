using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))] 
    public class SimpleHorizontalMovementBetweenPoints : MonoBehaviour
    {
        [SerializeField] protected Transform[] _points;
        [SerializeField] protected int _startPointIndex;
        [SerializeField] protected float _movementSpeed = 10f;
        [SerializeField] protected float _minDistanceToPointToSelectNext = 0.01f;

        protected Rigidbody2D Rigidbody;
        
        private int _nextPointIndex;

        protected virtual void Awake()
        {
            _nextPointIndex = _startPointIndex;
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            float xDifference = _points[_nextPointIndex].position.x - transform.position.x;
            
            if (Mathf.Abs(xDifference) < _minDistanceToPointToSelectNext)
            {
                SelectNextPoint();
                return;
            }
            
            float direction = Mathf.Sign(xDifference);
            
            Vector2 velocity = new Vector2(direction * _movementSpeed * Time.fixedDeltaTime, Rigidbody.linearVelocity.y);
            Rigidbody.linearVelocity = velocity;
        }

        protected virtual void SelectNextPoint()
        {
            _nextPointIndex++;
            
            if (_nextPointIndex >= _points.Length)
            {
                _nextPointIndex = 0;
            }
        }
    }
}

