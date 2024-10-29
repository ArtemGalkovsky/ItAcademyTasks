using UnityEngine;

public class ShipRotationOnPress : MonoBehaviour
{
    [SerializeField] private ShipRotationConfig _shipRotationConfig;

    private ShipPreviewActions _shipPreviewActions;
    private bool _isRotatingNow = false;

    private void Awake()
    {
        InitializeInputSystem();
    }

    private void InitializeInputSystem()
    {
        _shipPreviewActions = new ShipPreviewActions();

        _shipPreviewActions.Rotate.Enable();

        _shipPreviewActions.Rotate.Rotate.performed += context =>
        {
            _isRotatingNow = true;
        };

        _shipPreviewActions.Rotate.Rotate.canceled += context =>
        {
            _isRotatingNow = false;
        };

        _shipPreviewActions.MousePosition.Enable();
    }

    private void Update()
    {
        if (_isRotatingNow)
        {
            RotateShip();
        }
    }

    private void RotateShip()
    {
        float deltaHorizontal = GetRotationDeltaHorizontal();
        transform.Rotate(deltaHorizontal * _shipRotationConfig.RotationSensitivity * Vector3.up);
    }

    private float GetRotationDeltaHorizontal()
    {
        return _shipPreviewActions.MousePosition.MousePosition.ReadValue<Vector2>().x;
    }
}
