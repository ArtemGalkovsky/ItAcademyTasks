using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float speed;
    [SerializeField] private Vector3 rotationVector;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * speed * Time.deltaTime);
    }
}
