using System;
using UnityEngine;

namespace Player
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationVerticalSpeedCoefficient = 0.4f;
        [SerializeField, Min(0f)] private float _maxVerticalAngle = 89.9f;

        private void Start()
        {
            PlayerInput.Initialize();
        }

        private void Update()
        {
            RotateCameraVertically(PlayerInput.GetRotationDelta().y);   
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
