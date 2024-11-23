using System;
using Spawner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal class ObjectsChanceVisualizer : MonoBehaviour
    {
        [SerializeField] private Slider _chanceSlider;
        [SerializeField] private TMP_Text _chanceText;
        [SerializeField] private Spawner.Spawner _spawner;
        [SerializeField] private int _objectIndex = 0;

        public Action<float> ChanceChanged;

        private void Awake()
        {
            if (_chanceSlider == null)
            {
                Debug.LogError("_chanceSlider == null");
            }
            else
            {
                if (_spawner == null)
                {
                    Debug.LogError("Spawner is null");
                }
                else
                {
                    _chanceSlider.value = _spawner.ObjectToSpawn[_objectIndex].SpawnChance;
                }
            }
            
            if (_chanceText == null)
            {
                Debug.LogError("_chanceText == null");
            }
        }

        private void OnEnable()
        {
            _chanceSlider.onValueChanged.AddListener(ChangeValue);
        }

        private void ChangeValue(float newValue)
        {
            _chanceText.text = newValue + "%";
            ChanceChanged?.Invoke(newValue);
        }

        private void OnDisable()
        {
            _chanceSlider.onValueChanged.RemoveListener(ChangeValue);
        }
    }  
}

