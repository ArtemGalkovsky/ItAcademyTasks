using System;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    internal class BackgroundParallaxMovement : MonoBehaviour
    {
        [SerializeField] private float _backgroundSpeedMultiplier = 10f;

        private IEnumerable<BackgroundTileContainer> _backgroundTileContainers;
        private float _movementDirection = 1f;

        private bool _isInitialized = false;

        public void Initialize(IEnumerable<BackgroundTileContainer> backgroundTileContainers)
        {
            _backgroundTileContainers = backgroundTileContainers;
            
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            foreach (var container in _backgroundTileContainers)
            {
                foreach (var backgroundTile in container.BackgroundPropertiesComponents)
                {
                    float movementX = -backgroundTile.BackgroundSpeed * _backgroundSpeedMultiplier * _movementDirection * Time.deltaTime;
                    
                    if (backgroundTile.transform.position.x > container.MaxXValue || backgroundTile.transform.position.x < container.MinXValue)
                    {
                        backgroundTile.transform.position = new Vector2(container.StartXValue, backgroundTile.transform.position.y); 
                    }
                    else
                    {
                        backgroundTile.transform.Translate(movementX * Vector3.right);   
                    }
                }
            }
        }
        
        public void SetMovementDirection(float direction)
        {
            if (direction > 0f)
            {
                _movementDirection = 1f;
            } else if (direction < 0f)
            {
                _movementDirection = -1f;
            }
            else
            {
                _movementDirection = 0f;
            }
        }
    }
}
