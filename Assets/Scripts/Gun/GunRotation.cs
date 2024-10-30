using UnityEngine;

namespace Scriptables.Guns
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField] private Transform _playerHeadCamera;
    
        private void Start()
        {
            if (_playerHeadCamera == null)
            {
                Debug.LogError("Gun: Player Head Camera is null");
                return;
            }
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(_playerHeadCamera.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

}
