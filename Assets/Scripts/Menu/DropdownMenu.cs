using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _visualizer;
    [SerializeField] private TMP_Dropdown _dropdown;

    private string initialVisualizerText;

    private void Awake()
    {
        initialVisualizerText = _visualizer.text;

        _dropdown.onValueChanged.AddListener(value =>
        {
            _visualizer.text = _dropdown.options[value].text;
        });
    }

    private void OnDisable()
    {
        _visualizer.text = initialVisualizerText;
    }

    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveAllListeners();
    }
}
