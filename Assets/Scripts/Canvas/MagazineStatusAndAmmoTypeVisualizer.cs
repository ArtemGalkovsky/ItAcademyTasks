using System;
using System.Linq;
using Scriptables.Guns;
using TMPro;
using UnityEngine;

namespace Canvas
{
    [Serializable]
    public struct MagazineStateText
    {
        [SerializeField] private MagazinesStates _magazineState;
        [SerializeField] private string _statusText;

        public MagazinesStates MagazineState => _magazineState;
        public string StatusText => _statusText;

        public MagazineStateText(MagazinesStates magazineState, string statusText)
        {
            _magazineState = magazineState;
            _statusText = statusText;
        }
    }
    
    public class MagazineStatusAndAmmoTypeVisualizer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ammoAmountText;
        [SerializeField] private TMP_Text _ammoTypeText;
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private TMP_Text _gunNameText;
        [SerializeField] private MagazineStateText[] _magazineStatesTexts;
        
        private void Awake()
        {
            GunLogic.StateUpdated.AddListener(StateUpdated_OnStateUpdated);
        }

        private void StateUpdated_OnStateUpdated(DefaultGun gun, DefaultMagazine magazine, MagazinesStates state)
        {
            _gunNameText.text = gun.ToString();
            _ammoAmountText.text = $"A:{magazine.AmmoInCurrentMagazineLeftInGun} | M:{magazine.MagazinesLeftInGun}";
            _ammoTypeText.text = magazine.AmmoType.ToString().ToUpper();

            MagazineStateText stateText = GetStateObject(state);
            _statusText.text = stateText.StatusText;
        }

        private MagazineStateText GetStateObject(MagazinesStates state)
        {
            return _magazineStatesTexts.FirstOrDefault(stateText => stateText.MagazineState == state);
        }

        private void OnDestroy()
        {
            GunLogic.StateUpdated?.AddListener(StateUpdated_OnStateUpdated);
            StopAllCoroutines();
        }
    }

}
