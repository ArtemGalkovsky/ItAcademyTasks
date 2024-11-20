using System;
using UnityEngine;

namespace Stairs
{
    public class StairsGenerator : MonoBehaviour
    {
        [SerializeField] private StairsBox[] _stairsContainers;
        [SerializeField] private StairsBox _initialMiddleStairsBox;
        [SerializeField] private float _stairsContainerHeight = 23f;

        public Action StairsReconstructed;
        
        private void Awake()
        {
            if (_stairsContainers.Length <= 0)
            {
                Debug.LogError("StairsContainers is empty!");
            }
            else
            {
                ReconstructStairs(_initialMiddleStairsBox);
                
                foreach (StairsBox stairsBox in _stairsContainers)
                {
                    stairsBox.PlayerEnterStairsBox += ReconstructStairs;
                }
            }
        }

        private void ReconstructStairs(StairsBox middleStairsBox)
        {
            StairsReconstructed?.Invoke();
            
            int stairsGoesDown = (_stairsContainers.Length - 1) / 2;

            int currentStairsBoxIndex = 0;
            float initialStairsBoxY = middleStairsBox.transform.position.y;
            float currentStairsBoxY = initialStairsBoxY;
            foreach (StairsBox stairsBox in _stairsContainers)
            {
                if (stairsBox.Equals(middleStairsBox))
                {
                    continue; 
                }
                
                if (currentStairsBoxIndex < stairsGoesDown)
                {
                    currentStairsBoxY -= _stairsContainerHeight;
                }
                else if (currentStairsBoxIndex == stairsGoesDown)
                {
                    currentStairsBoxY = initialStairsBoxY + _stairsContainerHeight;
                }
                else
                {
                    currentStairsBoxY += _stairsContainerHeight;
                }
                    
                stairsBox.transform.position = new Vector3(stairsBox.transform.position.x, currentStairsBoxY, stairsBox.transform.position.z);
                currentStairsBoxIndex++;
            }
        }

        private void OnDestroy()
        {
            foreach (StairsBox stairsBox in _stairsContainers)
            {
                stairsBox.PlayerEnterStairsBox -= ReconstructStairs;
            }
        }
    }
}
