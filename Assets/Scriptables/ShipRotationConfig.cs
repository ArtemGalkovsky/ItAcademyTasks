using UnityEngine;

[CreateAssetMenu(fileName = "ShipRotationConfig", menuName = "Scriptable Objects/ShipRotationConfig")]
public class ShipRotationConfig : ScriptableObject
{
    [SerializeField] private float _rotationSpeedMeters;
    [SerializeField] private float _mouseMovementHorizontalDeathZone;

    public float RotationSpeedMeters => _rotationSpeedMeters;
    public float MouseMovementHorizontalDeathZone => _mouseMovementHorizontalDeathZone;
}
