using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private float xRandomRange;
    [SerializeField] private float zRandomRange;
    [SerializeField, Min(0.1f)] private float delayBetweenTeleportationsSeconds = 1f;

    private void Start()
    {
        StartCoroutine(TeleportToRandomLocation());
    }

    IEnumerator TeleportToRandomLocation()
    {
        while (true)
        {
            transform.position = GetNewRandomPosition();

            yield return new WaitForSeconds(delayBetweenTeleportationsSeconds);
        }
    }

    private Vector3 GetNewRandomPosition()
    {
        float xBias = Random.Range(-xRandomRange, xRandomRange);
        float zBias = Random.Range(-zRandomRange, zRandomRange);

        return new Vector3(initialPosition.x + xBias, 0, initialPosition.z + zBias);
    }
}
