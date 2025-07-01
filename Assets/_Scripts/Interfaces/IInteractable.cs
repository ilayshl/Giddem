using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interactable objects will be here
    /// </summary>
    public interface IInteractable
    {
        public void OnRangeEnter();
        public void OnRangeExit();
        public void OnInteract();
    }
}
