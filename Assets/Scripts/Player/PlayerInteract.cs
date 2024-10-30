using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float _interactDistance = 3f;
        [SerializeField] private CameraRotation _headCamera;
        [SerializeField] private TMP_Text _interactText;
        private bool _isInteracting;
        private void Start()
        {
            SetHeadCamera(); 
            
            Player.PlayerInput.EnabledPlayerInputActions.Interact.Interact.performed += (context) => _isInteracting = true;
            Player.PlayerInput.EnabledPlayerInputActions.Interact.Interact.canceled += (context) => _isInteracting = false;
            
            _interactText.enabled = false;
        }

        private void SetHeadCamera()
        {
            if (_headCamera == null)
            {
                Camera cameraMain = Camera.main;

                if (cameraMain == null)
                {
                    Debug.LogError("No camera found!");
                    return;
                }
                
                _headCamera = cameraMain.GetComponent<CameraRotation>();

                if (_headCamera == null)
                {
                    Debug.LogError("No camera found with CameraRotation component!");
                }
            }
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            Interactable.IInteractable interactable;

            if (Physics.Raycast(transform.position, transform.forward + _headCamera.transform.forward, out hit, _interactDistance) && hit.collider.TryGetComponent(out interactable))
            {
                _interactText.enabled = true;
                
                if (_isInteracting)
                {
                    interactable.Interact(gameObject);
                    _isInteracting = false;
                }
            }
            else
            {
                _interactText.enabled = false;
            }
        }
    }
}
