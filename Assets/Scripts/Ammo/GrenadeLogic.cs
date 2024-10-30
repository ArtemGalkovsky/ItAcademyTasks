using Scriptables.Ammo;
using UnityEngine;

namespace Scriptables.Ammo
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrenadeLogic : AmmoDefaultLogic
    {
        [SerializeField] private GrenadeScriptable _grenadeScriptable;

        protected override void OnCollisionEnter(Collision other)
        {
            Vector3 explosionPosition = transform.position;
            
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, _grenadeScriptable.ExplosionRadius);
            
            foreach (Collider collider in colliders)
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(_grenadeScriptable.ExplosionForce, explosionPosition, _grenadeScriptable.ExplosionRadius, _grenadeScriptable.UpwardsModifier, ForceMode.Impulse);
                }
            }
            
            base.OnCollisionEnter(other);
        }
    }

}

