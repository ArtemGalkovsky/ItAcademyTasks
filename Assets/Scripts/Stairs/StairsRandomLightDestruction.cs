using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stairs
{
    internal class StairsRandomLightDestruction : MonoBehaviour
    {
        [SerializeField] private Light _light;
        [SerializeField] private Vector2 _randomCooldownRangeSeconds = new Vector2(10f, 60f);

        private StairsGenerator _stairsGenerator;
        
        private void Awake()
        {
            if (_light == null)
            {
                Debug.LogError("Light is null");
            }

            StartCoroutine(WaitBreakLight());
            
            _stairsGenerator = FindAnyObjectByType<StairsGenerator>();

            if (_stairsGenerator == null)
            {
                Debug.LogError("StairsGenerator is null");
            }
            
            _stairsGenerator.StairsReconstructed += ReEnableLight;
        }

        private IEnumerator WaitBreakLight()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_randomCooldownRangeSeconds.x, _randomCooldownRangeSeconds.y));
                
                _light.enabled = false;
            }
        }

        private void ReEnableLight()
        {
            _light.enabled = true;
        }
        
        private void OnDestroy()
        {
            StopAllCoroutines();
            _stairsGenerator.StairsReconstructed -= ReEnableLight;
        }
    }
 
}
