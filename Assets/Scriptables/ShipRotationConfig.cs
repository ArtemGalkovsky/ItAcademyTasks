using UnityEngine;

[CreateAssetMenu(fileName = "ShipRotationConfig", menuName = "Scriptable Objects/ShipRotationConfig")]
public class ShipRotationConfig : ScriptableObject
{
    [SerializeField] private float _rotationSensitivity = 0.1f;

    public float RotationSensitivity => _rotationSensitivity;
}
