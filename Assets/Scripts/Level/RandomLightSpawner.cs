using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StairsGenerator
{
    [RequireComponent(typeof(StairsGenerator))]
    internal class RandomLightSpawner : MonoBehaviour
    {
        [SerializeField] private LightPrefab _lightPrefab;
        [SerializeField] private Vector2 _rangeSecondsToSummonLight = new Vector2(15, 30);

        private Collider _currentStairsBoxCollider;
        private void Awake()
        {
            foreach (StairsBox box in GetComponent<StairsGenerator>().Boxes)
            {
                box.PlayerMovedToMe.AddListener(stairsBox =>
                {
                    _currentStairsBoxCollider = stairsBox.GetComponent<Collider>();
                });
            }
            
            StartCoroutine(SpawnRandomLightAwaiter());
        }

        private IEnumerator SpawnRandomLightAwaiter()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_rangeSecondsToSummonLight.x, _rangeSecondsToSummonLight.y));

                SpawnRandomLight();
            }
        }

        private void SpawnRandomLight()
        {
            Bounds bounds = _currentStairsBoxCollider.bounds;
            
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
                
            Instantiate(_lightPrefab, randomPosition, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
        }

        private void OnDestroy()
        {
            foreach (StairsBox box in GetComponent<StairsGenerator>().Boxes)
            {
                box.PlayerMovedToMe.RemoveAllListeners();
            }
        }
    }
}