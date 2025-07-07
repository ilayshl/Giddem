using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InteractableObjectType[] objectTypeToCollide;
    [SerializeField] private KeyCode inputKey;
    [SerializeField] private CharacterManager playerManager;
    private InteractableObject _interactableObject;

    private void Update()
    {
        GetInput();
    }

    /// <summary>
    /// Checks if the required inputKey is pressed, then compares CharacterState and interacts
    /// </summary>
    private void GetInput()
    {
        if (Input.GetKeyDown(inputKey))
        {
            CharacterState state = playerManager.state;
            if (state == CharacterState.Idle || state == CharacterState.Run)
            {
                if (_interactableObject != null) //If something is highlighted
                {
                    _interactableObject.OnInteract();
                    if (!_interactableObject.enabled) //If was destroyed from Interaction
                    {
                        _interactableObject = null; //Reset highlight
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
                foreach (var objectType in objectTypeToCollide)
                {
                    if (objectType == interactable.ObjectType)
                    {
                        _interactableObject?.OnRangeExit();
                        _interactableObject = interactable;
                        interactable.OnRangeEnter();
                    }
                }
            }

        }
    }

    /// <summary>
    /// When object leaves sight, if it is the object that is currently higlighted, cancel highlight
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerExit(Collider other)
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
