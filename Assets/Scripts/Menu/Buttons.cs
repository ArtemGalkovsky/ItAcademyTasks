using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private TMP_Text _visualizer;
    [SerializeField] private List<Button> _buttonsToVisualize;
    [SerializeField] private Button _disable;
    private string _initialVisualizerText;

    private void Awake()
    {
        _initialVisualizerText = _visualizer.text;

        _disable.onClick.AddListener(() => ActionPerformer.PerformActionOnObjects<Button>(_buttonsToVisualize, button => button.gameObject.SetActive(false)));

        ActionPerformer.PerformActionOnObjects<Button>(_buttonsToVisualize, SetSendingTextToVisualizer);
    }
/*
    private void OneCliked()
    {
        _visualizer.text = "One";
    }*/

    private void SetSendingTextToVisualizer(Button button)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        button.onClick.AddListener(() =>
        {
            if (buttonText == null)
            {
                Debug.LogError($"Button {buttonText.name} hasn't TMP_Text children!");
            }

            _visualizer.text = buttonText?.text;
        });
    }

    private void OnDisable()
    {
        ActionPerformer.PerformActionOnObjects<Button>(_buttonsToVisualize, button => button.gameObject.SetActive(true));
        
        _visualizer.text = _initialVisualizerText;
    }

    private void OnDestroy()
    {
        ActionPerformer.PerformActionOnObjects<Button>(_buttonsToVisualize, button => button.onClick.RemoveAllListeners());
    }
}

/*
    private void PerformActionOnButtonsToVisualize(Action<Button> action)
    {
        foreach (Button button in _buttonsToVisualize)
        {
            action(button);
        }
    }
*/