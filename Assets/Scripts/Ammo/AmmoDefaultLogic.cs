using System;
using System.Collections;
using UnityEngine;

namespace Scriptables.Ammo
{
    public class AmmoDefaultLogic : MonoBehaviour
    {
        [SerializeField]
        protected float _objectDeletionTimeAfterCollision = 10f;
    
        protected virtual void OnCollisionEnter(Collision other)
        {
            StopCoroutine(DeleteAfterSeconds());
        
            StartCoroutine(DeleteAfterSeconds());
        }

        IEnumerator DeleteAfterSeconds()
        {
            yield return new WaitForSeconds(_objectDeletionTimeAfterCollision);
        
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}

