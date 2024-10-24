using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShipSwapper))]
public class ShipColorer : MonoBehaviour
{
    [SerializeField] private Button[] _colorSelectionButtons;

    private ShipSwapper _shipSwapper;

    private void Start()
    {
        _shipSwapper = GetComponent<ShipSwapper>();

        foreach (Button button in _colorSelectionButtons)
        {
            Image buttonImage = button.GetComponent<Image>();
            button.onClick.AddListener(() => ChangeCurrentShipColor(buttonImage.color));
        }
    }

    private void ChangeCurrentShipColor(Color newShipColor)
    {
        GameObject currentShip = _shipSwapper.CurrentShip;
        MeshRenderer shipMeshRenderer = currentShip.GetComponent<MeshRenderer>();

        foreach (Material material in shipMeshRenderer.materials)
        {
            material.color = newShipColor;
        }
    }

    private void OnDestroy()
    {
        foreach (Button button in _colorSelectionButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
