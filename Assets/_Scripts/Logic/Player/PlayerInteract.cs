using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private CharacterManager playerManager;
    private InteractableObject _interactableObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CharacterState state = playerManager.state;
            if (state == CharacterState.Idle || state == CharacterState.Run)
            {
                if (_interactableObject != null)
                {
                    _interactableObject.OnInteract();
                    if (!_interactableObject.enabled)
                    {
                        _interactableObject = null;
                    }
                }
            }
        }
    }

    /// <summary>
    /// When a new objects enters sight, get rid of old object and highlight the new one
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.TryGetComponent<InteractableObject>(out InteractableObject interactable))
            {
                _interactableObject?.OnRangeExit();
                _interactableObject = interactable;
                interactable.OnRangeEnter();
            }

        }
    }

    /// <summary>
    /// When object leaves sight, if it is the object that is currently higlighted, cancel highlight
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.TryGetComponent<InteractableObject>(out InteractableObject interactable))
            {
                if (interactable == _interactableObject)
                {
                    interactable.OnRangeExit();
                    _interactableObject = null;
                }
            }
        }
    }

}
