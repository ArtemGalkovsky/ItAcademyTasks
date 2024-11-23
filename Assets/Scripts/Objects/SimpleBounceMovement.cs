using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class SimpleBounceMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private string _bouncingWallTag = "BouncingWall";
        
        private Rigidbody2D _rigidbody;
        private Vector2 _movementDirection = Vector2.zero;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _movementDirection = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;   
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _movementSpeed * Time.fixedDeltaTime * _movementDirection;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_bouncingWallTag))
            {
                _movementDirection = Vector2.Reflect(_movementDirection, other.contacts[0].normal).normalized;
            }
        }
    }

}
