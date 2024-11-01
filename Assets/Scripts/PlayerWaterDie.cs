using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWaterDie : MonoBehaviour
{
    [SerializeField] private LayerMask _waterLayer;
    [SerializeField] private TMP_Text _dieText;
    [SerializeField] private float _dieTextVisibleTimeSeconds = 0.5f;
    [SerializeField] private float _cameraDisabledTimeWhenDied = 1f;
    [SerializeField] private Camera _camera;
    
    private float _initialCameraFieldOfView;
    public static readonly UnityEvent DieEvent = new UnityEvent();
    
    private void Awake()
    {
        if (_dieText == null)
        {
            Debug.LogError("Die Text is null!");
        }
        else
        {
            _dieText.enabled = false;   
        }

        if (_camera == null)
        {
            _camera = Camera.main;
        }

        if (_camera == null)
        {
            Debug.LogError("Camera and Camera.main are null!");
        }
        
        _initialCameraFieldOfView = _camera.fieldOfView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_waterLayer.value == (_waterLayer.value & (1 << other.gameObject.layer)))
        {
            StopCoroutine(Respawn());
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        DieEvent?.Invoke();

        _camera.fieldOfView = 0.01f;
        
        yield return new WaitForSeconds(_cameraDisabledTimeWhenDied);
        
        _camera.fieldOfView = _initialCameraFieldOfView;
        _dieText.enabled = true;
        
        yield return new WaitForSeconds(_dieTextVisibleTimeSeconds);
        
        _dieText.enabled = false;
    }
}
