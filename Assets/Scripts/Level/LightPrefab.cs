using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightPrefab : MonoBehaviour
{
    [SerializeField] private Vector2 _timeRangeSecondsToDestroy = new Vector2(1f, 7f);
    
    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(Random.Range(_timeRangeSecondsToDestroy.x, _timeRangeSecondsToDestroy.y));

        Destroy(gameObject);
    }
}
