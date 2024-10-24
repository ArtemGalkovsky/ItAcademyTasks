using UnityEngine;

public class ShipRotationOnPress : MonoBehaviour
{
    [SerializeField] private ShipRotationConfig _shipRotationConfig;

    private ShipPreviewActions _shipPreviewActions;
    private Vector2 _currentMousePosition;
    private Vector2 _previousMousePosition;
    private bool _isRotatingNow = false;
    private bool _isFirstRotation = true;

    private void Awake()
    {
        InitializeInputSystem();
        _isFirstRotation = true;
    }

    private void InitializeInputSystem()
    {
        _shipPreviewActions = new ShipPreviewActions();

        _shipPreviewActions.Rotate.Enable();

        _shipPreviewActions.Rotate.Rotate.performed += context =>
        {
            _previousMousePosition = GetCurrentMousePosition();
            _isRotatingNow = true;
        };

        _shipPreviewActions.Rotate.Rotate.canceled += context =>
        {
            _isFirstRotation = true;
            _isRotatingNow = false;
        };

        _shipPreviewActions.MousePosition.Enable();
    }

    private void Update()
    {
        _currentMousePosition = GetCurrentMousePosition();

        if (_isRotatingNow)
        {
            if (!_isFirstRotation)  // To prevent first click freaky rotation.
            {
                RotateShip();
            }

            _isFirstRotation = false;
        }

        _previousMousePosition = _currentMousePosition;
    }

    private void RotateShip() {
    
        float mouseHorizontalMoveFromPreviousPosition = _currentMousePosition.x - _previousMousePosition.x;
        bool isMouseMovedOutDeathZone = Mathf.Abs(mouseHorizontalMoveFromPreviousPosition) > _shipRotationConfig.MouseMovementHorizontalDeathZone;

        if (isMouseMovedOutDeathZone)
        {
            transform.Rotate(new Vector3(0, -mouseHorizontalMoveFromPreviousPosition * Time.deltaTime * _shipRotationConfig.RotationSpeedMeters, 0));
        }
    }

    private Vector2 GetCurrentMousePosition()
    {
        return _shipPreviewActions.MousePosition.MousePosition.ReadValue<Vector2>();
    }
}
