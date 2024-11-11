using System;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    [RequireComponent(typeof(BackgroundGenerator), typeof(BackgroundParallaxMovement))]
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            IEnumerable<BackgroundTileContainer> containers = GetComponent<BackgroundGenerator>().Generate();
            GetComponent<BackgroundParallaxMovement>().Initialize(containers);
        }
    } 
}

