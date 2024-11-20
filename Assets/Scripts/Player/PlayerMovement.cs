using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 150f;
        [SerializeField] private float _rotationSensitivity = 1f;
        [SerializeField] private float _maxSlopeAngle = 15f;
        [SerializeField] private float _playerSlopeCheckRaycastLength = 1.3f;
        
        private PlayerInput _playerInput;
        private Rigidbody _rigidbody;
        private RaycastHit _slopeHit;

        private float _mouseXDeltaSummary = 0f;
        private Vector3 _previousPosition;
        private bool _isMoving = false;
        
        public RaycastHit SlopeHit => _slopeHit;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.Initialize();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _mouseXDeltaSummary += _playerInput.GetMousePositionDelta().x;
        }

        private void FixedUpdate()
        {
            bool isOnSlope = IsOnSlope();

            if (isOnSlope)
            {
                _rigidbody.useGravity = false;
            }
            else if (!_rigidbody.useGravity)
            {
                _rigidbody.useGravity = true;
            }
            
            Move(isOnSlope);
            Rotate();
        }

        private void Move(bool isOnSlope)
        {
            Vector2 movement = _playerInput.EnabledPlayerInputActions.Movement.Move.ReadValue<Vector2>().normalized;
            Vector3 moveDirection = movement.x * transform.right + movement.y * transform.forward;
            Vector3 velocity;
            
            if (isOnSlope)
            {
                moveDirection = GetSlopeMovementDirection(moveDirection);
                velocity = _movementSpeed * Time.fixedDeltaTime * moveDirection;
            }
            else
            {
                velocity = _movementSpeed * Time.fixedDeltaTime * moveDirection;
                velocity.y = _rigidbody.linearVelocity.y;
            }

            _isMoving = (transform.position - _previousPosition).sqrMagnitude > 0.01f;
            
            _rigidbody.linearVelocity = velocity;
            _previousPosition = transform.position;
        }

        private void Rotate()
        {
            _rigidbody.angularVelocity = _mouseXDeltaSummary * _rotationSensitivity * Vector3.up;
            // _rigidbody.rotation = Quaternion.Euler(0f, _rigidbody.rotation.eulerAngles.y + _mouseXDeltaSummary * _rotationSensitivity, 0f);
            _mouseXDeltaSummary = 0f;
        }

        public bool IsOnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerSlopeCheckRaycastLength))
            {
                float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                return angle < _maxSlopeAngle && angle != 0f;
            }

            return false;
        }

        private Vector3 GetSlopeMovementDirection(Vector3 movementDirection)
        {
            return Vector3.ProjectOnPlane(movementDirection, _slopeHit.normal).normalized;
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.DrawRay(transform.position, Vector3.down * _playerSlopeCheckRaycastLength);
        // }

        public bool IsMoving()
        {
            return _isMoving;
        }
    }
}

