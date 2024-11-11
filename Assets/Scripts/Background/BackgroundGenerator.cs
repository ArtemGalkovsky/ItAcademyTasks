using System;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public struct BackgroundTileContainer
    {
        public IEnumerable<MovementBackgroundSpriteProperties> BackgroundPropertiesComponents { get; private set; }
        public BackgroundTilePrefab CurrentBackgroundTilePrefab  { get; }
        public float MinXValue  { get; }
        public float MaxXValue  { get; }
        public float StartXValue  { get; }

        public BackgroundTileContainer(BackgroundTilePrefab backgroundTilePrefab, float minXValue, float xOffset)
        {
            CurrentBackgroundTilePrefab = backgroundTilePrefab;
            BackgroundPropertiesComponents = new List<MovementBackgroundSpriteProperties>();
            MinXValue = minXValue;
            MaxXValue = minXValue + xOffset + xOffset;
            StartXValue = minXValue + xOffset;
            
            AddBackgroundSpritesProperties(backgroundTilePrefab);
        }

        private void AddBackgroundSpritesProperties(BackgroundTilePrefab backgroundTilePrefab)
        {
            List<MovementBackgroundSpriteProperties> componentsList = new List<MovementBackgroundSpriteProperties>();
            foreach (var properties in backgroundTilePrefab.GetComponentsInChildren<MovementBackgroundSpriteProperties>())
            {
                componentsList.Add(properties);
            }
            
            BackgroundPropertiesComponents = componentsList;
        }
    }
    
    public class BackgroundGenerator : MonoBehaviour
    {
        [SerializeField, Tooltip("If is null - will be used Camera.main object")] private Camera _mainCamera;
        [SerializeField, Tooltip("If is null - will be used current object")] private Transform _backgroundParent;
        [SerializeField] private BackgroundTilePrefab _backgroundTilePrefab;
        
        [SerializeField] private float _newBackgroundPrefabYOffset = 0f;
        [SerializeField, 
         Tooltip("Extra background prefabs used to prevent player from seeing teleporting backgrounds")] 
        private int _extraBackgroundPrefabsCount = 4;
        
        public IEnumerable<BackgroundTileContainer> Generate()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            
            if (_backgroundParent == null)
            {
                _backgroundParent = transform;
            }

            if (_backgroundTilePrefab == null)
            {
                Debug.Log("Background tile prefab not found!");
            }

            float backgroundImageWidth = GetFirstBackgroundImageWidth(_backgroundTilePrefab);
            int backgroundsCount = CalculateNumberOfBackgroundTilesToFillScreen(backgroundImageWidth) + _extraBackgroundPrefabsCount;
            float xOffset = CalculateFirstBackgroundXOffset(backgroundImageWidth, backgroundsCount);
            
            BackgroundTileContainer[] movementBackgrounds = new BackgroundTileContainer[backgroundsCount];
            for (ushort index = 0; index < backgroundsCount; index++)
            {
                Vector3 newBackgroundPosition = new Vector3(xOffset, _newBackgroundPrefabYOffset, 0f);
                BackgroundTilePrefab newBackgroundPrefab = Instantiate(_backgroundTilePrefab, newBackgroundPosition, Quaternion.identity, _backgroundParent);
        
                movementBackgrounds[index] = new BackgroundTileContainer(newBackgroundPrefab, xOffset - backgroundImageWidth, backgroundImageWidth);
                
                xOffset += backgroundImageWidth;
            }

            return movementBackgrounds;
        }

        private float GetFirstBackgroundImageWidth(BackgroundTilePrefab backgroundTilePrefab)
        {
            SpriteRenderer spriteRenderer = backgroundTilePrefab.GetComponentInChildren<SpriteRenderer>();
            
            if (spriteRenderer == null)
            {
                Debug.LogError("Background Tile Prefab does not have children with sprite renderer!");
            }
            
            return spriteRenderer.sprite.textureRect.width / spriteRenderer.sprite.pixelsPerUnit;   
        }
        
        private int CalculateNumberOfBackgroundTilesToFillScreen(float tileSize)
        {
            float screenWorldWidth = 2f * _mainCamera.orthographicSize * _mainCamera.aspect;  // 2f because orthographicSize returns half of the height
            return Mathf.CeilToInt(screenWorldWidth / tileSize);
        }

        private float CalculateFirstBackgroundXOffset(float tileSize, int backgroundsCount)
        {
            float totalWidth = tileSize * backgroundsCount;
            float firstTileXOffset = -totalWidth / 2f + tileSize / 2f;

            return firstTileXOffset;
        }

    }
}
