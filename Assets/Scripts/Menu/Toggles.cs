using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toggles : MonoBehaviour
{
    [SerializeField] private TMP_Text _visualizer;
    [SerializeField] private List<Toggle> _toggles;
    private string _initialVisualizerText;

    private void Awake()
    {
        _initialVisualizerText = _visualizer.text;

        ActionPerformer.PerformActionOnObjects<Toggle>(_toggles, toggle => SetSendingTextToVisualizer(toggle));
    }

    private void SetSendingTextToVisualizer(Toggle toggle)
    {
        TMP_Text toggleText = toggle.GetComponentInChildren<TMP_Text>();

        toggle.onValueChanged.AddListener(isChecked =>
        {
            if (toggleText == null)
            {
                Debug.LogError($"Button {toggle.name} hasn't TMP_Text children!");
            }

            if (isChecked)
            {
                UncheckOtherToggles(toggle);
                _visualizer.text = toggleText?.text;
            }
        });
    }

    private void UncheckOtherToggles(Toggle currentCheckedToggle)
    {
        ActionPerformer.PerformActionOnObjects<Toggle>(_toggles, toggle =>
        {
            if (toggle != currentCheckedToggle)
            {
                toggle.isOn = false;
            }
        });
    }

    private void OnDisable()
    {
        ActionPerformer.PerformActionOnObjects<Toggle>(_toggles, toggle => toggle.gameObject.SetActive(true));
        ActionPerformer.PerformActionOnObjects<Toggle>(_toggles, toggle => toggle.isOn = false);
        
        _visualizer.text = _initialVisualizerText;
    }

    private void OnDestroy()
    {
        ActionPerformer.PerformActionOnObjects<Toggle>(_toggles, toggle => toggle.onValueChanged.RemoveAllListeners());
    }

}
