using UnityEngine;

namespace Scriptables.Ammo
{
    [CreateAssetMenu(fileName = "GrenadeScriptable", menuName = "AmmoScriptable/GranadeScriptable")]
    public class GrenadeScriptable : DefaultAmmo
    {
        [SerializeField] protected float _explosionForce = 30f;
        [SerializeField] protected float _explosionRadius = 2f;
        [SerializeField] protected float _upwardsModifier = 3f;
        
        public float ExplosionForce => _explosionForce;
        public float ExplosionRadius => _explosionRadius;
        public float UpwardsModifier => _upwardsModifier;
    }
}