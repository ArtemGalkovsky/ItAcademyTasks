using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected UnityEvent<GameObject> _interacted;

        UnityEvent<GameObject> IInteractable.Interacted => _interacted;

        public virtual void Interact(GameObject interactor)
        {
            _interacted?.Invoke(interactor);
        }
    }

}
