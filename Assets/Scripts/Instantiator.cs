using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToInstantiate;
    private GameObject currentInstantiatedObject = null;

    private void Awake()
    {
        if (objectsToInstantiate.Count == 0)
        {
            Debug.LogError("No objects to instantiate!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentInstantiatedObject != null)
                Destroy(currentInstantiatedObject);

            currentInstantiatedObject = Instantiate(
                    objectsToInstantiate[Random.Range(0, objectsToInstantiate.Count)]
                );
        }
    }
}
