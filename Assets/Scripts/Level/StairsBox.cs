using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace StairsGenerator
{
    [RequireComponent(typeof(Collider))]
    internal class StairsBox : MonoBehaviour
    {
        internal UnityEvent<StairsBox> PlayerMovedToMe { get; } = new UnityEvent<StairsBox>();

        private void OnTriggerEnter(Collider other)
        {
            PlayerMovedToMe?.Invoke(this);
        }
    }
}

