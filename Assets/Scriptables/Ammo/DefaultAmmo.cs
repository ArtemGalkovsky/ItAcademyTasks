using UnityEngine;

namespace Scriptables.Ammo
{
    [CreateAssetMenu(fileName = "DefaultAmmo", menuName = "AmmoScriptable/DefaultAmmo")]
    public class DefaultAmmo : ScriptableObject, IDefaultAmmo
    {
        [SerializeField] protected float _firingForce = 100f;
        [SerializeField] protected GameObject _ammoPrefab;
        [SerializeField] protected string _ammoName;

        public GameObject AmmoPrefab => _ammoPrefab;
        public float FiringForce => _firingForce;

        public override string ToString()
        {
            return _ammoName;
        }
    }
}