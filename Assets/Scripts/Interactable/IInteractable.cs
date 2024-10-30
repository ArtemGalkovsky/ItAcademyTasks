using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    public interface IInteractable
    {
        public UnityEvent<GameObject> Interacted { get; }
        public void Interact(GameObject interactor);
    }
}
