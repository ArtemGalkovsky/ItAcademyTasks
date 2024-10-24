using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class View
{
    [SerializeField] private Button _viewButton;
    [SerializeField] private Vector3 _cameraPosition;
    [SerializeField] private Vector3 _cameraRotation;

    public Button ViewButton => _viewButton;
    public Vector3 CameraPosition => _cameraPosition;
    public Vector3 CameraRotation => _cameraRotation;

    public View(Button viewButton, Vector3 cameraPosition, Vector3 cameraRotation)
    {
        _viewButton = viewButton;
        _cameraPosition = cameraPosition;
        _cameraRotation = cameraRotation;
    }
}

public class CameraShipView : MonoBehaviour
{
    [SerializeField] private View[] _views;
    [SerializeField] private int _initialViewIndex = 0;

    private void Awake()
    {
        View initialView = _views[_initialViewIndex];

        MoveCamera(initialView.CameraPosition, initialView.CameraRotation);

        foreach (View view in _views)
        {
            view.ViewButton.onClick.AddListener(() => MoveCamera(view.CameraPosition, view.CameraRotation));
        }
    }

    private void MoveCamera(Vector3 position, Vector3 rotation)
    {
        transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
    }

    private void OnDestroy()
    {
        foreach (View view in _views)
        {
            view.ViewButton.onClick.RemoveAllListeners();
        }
    }
}
