using UnityEngine;

namespace Player
{
    public class PlayerCameraVerticalRotation : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _verticalRotationSensitivity = 1f;
        [SerializeField] private float _maxAngle = 90f;
        [SerializeField] private float _minAngle = -90f;
        
        private float _verticalRotation = 0f;
        
        private void Awake()
        {
            _playerInput.Initialize();
            
            _verticalRotation = transform.localEulerAngles.x;
            if (_verticalRotation > 180f) _verticalRotation -= 360f;
        }

        private void Update()
        {
            float deltaY = _playerInput.GetMousePositionDelta().y;
            _verticalRotation = Mathf.Clamp(_verticalRotation + -deltaY * _verticalRotationSensitivity, _minAngle, _maxAngle);
            
            transform.SetLocalPositionAndRotation(transform.localPosition, Quaternion.Euler(_verticalRotation, 0f, 0f));
        }
    }
}

