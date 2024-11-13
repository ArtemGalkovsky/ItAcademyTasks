using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StairsGenerator
{
    internal class StairsGenerator : MonoBehaviour
    {
        [SerializeField] private StairsBox[] _stairsBoxes;
        [SerializeField] private GameObject _player;
        
        private StairsBox[] _currentStairsPositionsArray;
        private float _stairsBoxHeight;
        
        public StairsBox[] Boxes => _stairsBoxes;
        
        private void Awake()
        {
            if (_stairsBoxes.Length <= 0)
            {
                Debug.LogError("No stairs boxes found!");
            }
            
            _stairsBoxHeight = _stairsBoxes[0].GetComponent<Collider>().bounds.size.y;
            
            _player.SetActive(false);
            
            SetupStairsPositions();
            ChangeStairsPositions(_stairsBoxes[_stairsBoxes.Length / 2]);
            
            foreach (StairsBox stairs in _stairsBoxes)
            {
                stairs.PlayerMovedToMe.AddListener(OnPlayerEnterAnotherBox);
            }
            
            _player.SetActive(true);
        }

        private void SetupStairsPositions()
        {
            _currentStairsPositionsArray = new StairsBox[_stairsBoxes.Length];

            for (int boxIndex = 0; boxIndex < _stairsBoxes.Length; boxIndex++)
            {
                _currentStairsPositionsArray[boxIndex] = _stairsBoxes[boxIndex];
            }
        }

        private void ChangeStairsPositions(StairsBox startStairsBox)
        {
            List<StairsBox> otherStairBoxes = _currentStairsPositionsArray.ToList();
            
            if (!otherStairBoxes.Remove(startStairsBox))
            {
                Debug.LogError("No start stairs box found!");
            }

            int boxesGoesUpCount = Mathf.CeilToInt(otherStairBoxes.Count() / 2f);
            float startBoxY = startStairsBox.transform.position.y;
            float currentY = startBoxY;
            
            for (int boxIndex = 0; boxIndex < boxesGoesUpCount; boxIndex++)
            {
                currentY += _stairsBoxHeight;
                otherStairBoxes[boxIndex].transform.position = Vector3.up * currentY;
            }
            
            currentY = startBoxY;
            for (int boxIndex = boxesGoesUpCount; boxIndex < otherStairBoxes.Count(); boxIndex++)
            {
                currentY -= _stairsBoxHeight;
                otherStairBoxes[boxIndex].transform.position = Vector3.up * currentY;
            }
        }
        
        private void OnPlayerEnterAnotherBox(StairsBox stairsBox)
        {
            ChangeStairsPositions(stairsBox);   
        }

        private void OnDestroy()
        {
            foreach (StairsBox stairs in _stairsBoxes)
            {
                stairs?.PlayerMovedToMe.RemoveAllListeners();
            }
        }
    }
}

