using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Action<InteractableObject> OnInteractAbility; //To use on the PlayerTelekinesis and PlayerGrapple abilities
    [SerializeField] private CharacterManager playerManager;
    [SerializeField] private InteractableObjectType[] objectTypeToCollide; //Types that the collider will be able to detect.
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
                _highlightedObject.OnInteract();

                switch (_highlightedObject.ObjectType) //For player-related behaviour such as abilities
                {
                    case InteractableObjectType.Telekinesis:
                        playerManager.ChangeCharacterState(CharacterState.Telekinesis);
                        break;
                    case InteractableObjectType.Grapple:
                        playerManager.ChangeCharacterState(CharacterState.Grapple);
                        break;
                }

                if (!_highlightedObject.enabled) //If was destroyed from Interaction
                {
                    _highlightedObject = null; //Reset highlight
                }

                OnInteractAbility?.Invoke(_highlightedObject);
            }
        }
    }

    /// <summary>
    /// When a new objects enters sight, highlight it. If there was already a highlighted object, get rid of the old one.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.TryGetComponent<InteractableObject>(out InteractableObject interactable))
            {
                foreach (var objectType in objectTypeToCollide) //Iterate through the selected interactable types of objects.
                {
                    if (objectType == interactable.ObjectType)
                    {
                        _highlightedObject?.RemoveOutline();
                        _highlightedObject = interactable;
                        interactable.ShowOutline();
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
                    interactable.RemoveOutline();
                    _highlightedObject = null;
                }
            }
        }
    }

}
