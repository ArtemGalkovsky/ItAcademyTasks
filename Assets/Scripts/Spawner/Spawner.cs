using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Spawner
{
    [Serializable]
    internal struct ObjectSpawnConfig // TODO: Rename
    {
        [SerializeField] private float _spawnChance;
        [SerializeField] private GameObject _object;
        
        public float SpawnChance => _spawnChance;
        public GameObject Object => _object;
        
        public ObjectSpawnConfig(float spawnChance, GameObject gameObject)
        {
            _spawnChance = Mathf.Clamp(spawnChance, 0f, 100f);
            _object = gameObject;
        }

        public void SetSpawnChance(float spawnChance)
        {
            if (spawnChance < 0 || spawnChance > 100)
            {
                Debug.LogWarning("Invalid spawn chance was passed to SetSpawnChance of " + _object.name);    
            }
            
            _spawnChance = spawnChance;
        }
    }
    
    internal class Spawner : MonoBehaviour
    {
        [SerializeField] private ObjectSpawnConfig[] _objectsToSpawn;
        [SerializeField] private Transform _leftBottomEdgeOfRoomTransform;
        [SerializeField] private Transform _rightTopEdgeOfRoomTransform;
        
        public ObjectSpawnConfig[] ObjectToSpawn => _objectsToSpawn;
        
        private void Awake()
        {
            if (_objectsToSpawn.Length <= 0)
            {
                Debug.LogError("Spawner: No objects to spawn");
            }

            if (_leftBottomEdgeOfRoomTransform == null)
            {
                Debug.LogError("Left bottom edge of the room transform is null. It will be set to Vector2.zero");
                _leftBottomEdgeOfRoomTransform.position = Vector2.zero;
            }
            
            if (_rightTopEdgeOfRoomTransform == null)
            {
                Debug.LogError("Right top edge of the room transform is null. It will be set to Vector2.zero");
                _rightTopEdgeOfRoomTransform.position = Vector2.zero;
            }

            if (_rightTopEdgeOfRoomTransform.position.x < _leftBottomEdgeOfRoomTransform.position.x || _leftBottomEdgeOfRoomTransform.position.y > _rightTopEdgeOfRoomTransform.position.y)
            {
                Debug.LogError("Incorrect positions!");
            }
        }
        
        public void SpawnRandomObject()
        {
            GameObject objectToSpawn = GetRandomObject();

            if (objectToSpawn == null)
            {
                Debug.LogError("No objects would be spawned???");
                return;
            }
            
            Instantiate(objectToSpawn, GetRandomPositionToSpawn(), Quaternion.identity);
        }

        private Vector2 GetRandomPositionToSpawn()
        {
            float x = Random.Range(_leftBottomEdgeOfRoomTransform.position.x, _rightTopEdgeOfRoomTransform.position.x);
            float y = Random.Range(_leftBottomEdgeOfRoomTransform.position.y, _rightTopEdgeOfRoomTransform.position.y);

            return new Vector2(x, y);
        }
        
        private GameObject GetRandomObject()
        {
            float totalPercentage = 0f;
            foreach (var objectToSpawn in _objectsToSpawn)
            {
                totalPercentage += objectToSpawn.SpawnChance;
            }

            if (totalPercentage > 100f)
            {
                Debug.LogError("Spawner has total spawn object chance percentage over 100%");
            }
            
            float randomValue = Random.Range(0f, 100f);
            
            float currentSpawnChance = 0f;
            foreach (var objectToSpawn in _objectsToSpawn)
            {
                currentSpawnChance += objectToSpawn.SpawnChance;
                
                if (randomValue <= currentSpawnChance)
                {
                    return objectToSpawn.Object;
                }
            }

            return null;
        }
    }
}

