using System;
using UnityEngine;

namespace Player
{
    internal class CameraRotation : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _rotationVerticalSpeedCoefficient = 0.4f;
        [SerializeField, Min(0f)] private float _maxVerticalAngle = 89.9f;

        private PlayerActions _playerInputActions;
        
        private void Start()
        {
            if (_playerInput == null)
            {
                Debug.LogError("Player Input is null!");
            }
            
            _playerInput.Initialize();
        }

        private void Update()
        {
            RotateCameraVertically(_playerInput.GetRotationDelta().y);   
        }
        
        private void RotateCameraVertically(float rotationDeltaVertical)
        {
            float rawVerticalRotationAngle = -rotationDeltaVertical * _rotationVerticalSpeedCoefficient;
            float rawRotationVerticalPosition = transform.localRotation.eulerAngles.x + rawVerticalRotationAngle;
            
            float convertedAngle = rawRotationVerticalPosition > 180 ? rawRotationVerticalPosition - 360 : rawRotationVerticalPosition;
            float rotationVerticalPosition = Mathf.Clamp(convertedAngle, -_maxVerticalAngle, _maxVerticalAngle);
            
            transform.localRotation = Quaternion.AngleAxis(rotationVerticalPosition, Vector3.right);
        }
    }
}
