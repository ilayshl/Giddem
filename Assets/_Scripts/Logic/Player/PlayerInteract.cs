using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private CharacterManager playerManager;
    [SerializeField] private InteractableObjectType[] objectTypeToCollide;
    [SerializeField] private KeyCode inputKey;
    private InteractableObject _highlightedObject;

    private void Update()
    {
        GetInput();
    }

    /// <summary>
    /// Checks if the required inputKey is pressed, then compares CharacterState and interacts
    /// </summary>
    private void GetInput()
    {
        if (_highlightedObject == null) return; //If nothing is highlighted
        if (Input.GetKeyDown(inputKey))
        {
            CharacterState state = playerManager.state;
            if (state == CharacterState.Idle || state == CharacterState.Run)
            {
                _highlightedObject.OnInteract(playerManager);

                var objectType = _highlightedObject.ObjectType;
                if (objectType == InteractableObjectType.Grapple)
                {
                    playerManager.ChangeCharacterState(CharacterState.Grapple);
                }
                else if (objectType == InteractableObjectType.Telekinesis)
                {
                    playerManager.ChangeCharacterState(CharacterState.Telekinesis);
                }
                
                    if (!_highlightedObject.enabled) //If was destroyed from Interaction
                {
                    _highlightedObject = null; //Reset highlight
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
                        _highlightedObject = interactable;
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
                if (interactable == _highlightedObject)
                {
                    _highlightedObject = null;
                }
            }
        }
    }

}
