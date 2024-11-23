using UI;
using UnityEngine;

namespace Objects
{
    public class ChanceChanger : MonoBehaviour
    {
        [SerializeField] private ObjectsChanceVisualizer _chanceVisualizer;
        [SerializeField] private Spawner.Spawner _spawner;
        [SerializeField] private int _objectIndex = 0;
        
        private void Awake()
        {
            if (_chanceVisualizer == null)
            {
                Debug.LogError("Chance Visualizer is null");
            }

            if (_spawner == null)
            {
                Debug.LogError("Spawner is null");
            }
        }

        private void OnEnable()
        {
            _chanceVisualizer.ChanceChanged += ChangeValueOnSpawner;
        }

        private void ChangeValueOnSpawner(float newChance)
        {
            _spawner.ObjectToSpawn[_objectIndex].SetSpawnChance(newChance);   
        }

        private void OnDisable()
        {
            _chanceVisualizer.ChanceChanged += ChangeValueOnSpawner;
        }
    }

}
