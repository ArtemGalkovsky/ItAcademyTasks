using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 maxScale;
    [SerializeField, Min(0.1f)] private float scalingSpeed;
    [SerializeField, Min(0.1f)] private float scaleBetweenVectorsToResetScale = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * scalingSpeed);

        if (Vector3.Distance(transform.localScale, maxScale) < scaleBetweenVectorsToResetScale)
            transform.localScale = Vector3.one;
    }
}
