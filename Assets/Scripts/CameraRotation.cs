using System;
using UnityEngine;

namespace Player
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationVerticalSpeedCoefficient = 10f;
        [SerializeField, Min(0f)] private float _maxVerticalAngle = 89.9f;

        private void Start()
        {
            Input.Initialize();
        }

        private void Update()
        {
            Vector2 rotation = Input.EnabledPlayerInput.Rotation.RotationJoyStick.ReadValue<Vector2>();
            RotateCameraVertically(rotation.y);
        }

        private void RotateCameraVertically(float verticalRotation)
        {
            float rawVerticalRotationAngle = Time.deltaTime * -verticalRotation * _rotationVerticalSpeedCoefficient;
            float rawRotationVerticalPosition = transform.localRotation.eulerAngles.x + rawVerticalRotationAngle;
            
            float convertedAngle = rawRotationVerticalPosition > 180 ? rawRotationVerticalPosition - 360 : rawRotationVerticalPosition;
            float rotationVerticalPosition = Mathf.Clamp(convertedAngle, -_maxVerticalAngle, _maxVerticalAngle);
            
            transform.localRotation = Quaternion.AngleAxis(rotationVerticalPosition, Vector3.right);
        }
    }  
}

