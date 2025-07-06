using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    private InteractableObject interactableObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CharacterState state = playerManager.state;
            if (state == CharacterState.Idle || state == CharacterState.Run)
            {
                if (interactableObject != null)
                {
                    interactableObject.OnInteract();
                    if (!interactableObject.enabled)
                    {
                        interactableObject = null;
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
                interactableObject?.OnRangeExit();
                interactableObject = interactable;
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
                if (interactable == interactableObject)
                {
                    interactable.OnRangeExit();
                    interactableObject = null;
                }
            }
        }
    }

}
