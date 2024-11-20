using System;
using Player;
using UnityEngine;

namespace Stairs
{
    public class StairsBox : MonoBehaviour
    {
        public event Action<StairsBox> PlayerEnterStairsBox;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMovement>(out _))
            {
                PlayerEnterStairsBox?.Invoke(this);
            }
        }
    }
}
