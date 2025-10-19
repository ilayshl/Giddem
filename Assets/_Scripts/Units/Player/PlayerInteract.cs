using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to interact with specific, pre-determined InteractableObject types.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    public Action<InteractableObject> OnInteractAbility; //To use on the PlayerTelekinesis and PlayerGrapple abilities
    [SerializeField] private CharacterManager playerManager;
    [SerializeField] private InteractableObjectType[] objectTypeToCollide; //Types that the collider will be able to detect.
    [SerializeField] private KeyCode inputKey;
    [SerializeField] private List<InteractableObject> _interactablesInRange;

    private void Update()
    {
        GetInput();
    }

    /// <summary>
    /// Checks if the required inputKey is pressed, then compares CharacterState and interacts
    /// </summary>
    private void GetInput()
    {
        if (_interactablesInRange.Count == 0) return; //If nothing is highlighted
        if (Input.GetKeyDown(inputKey))
        {
            CharacterState state = playerManager.state;
            if (state == CharacterState.Idle || state == CharacterState.Run)
            {
                _interactablesInRange[0].OnInteract();

                switch (_interactablesInRange[0].ObjectType) //For player-related behaviour such as abilities
                {
                    case InteractableObjectType.Telekinesis:
                        playerManager.ChangeCharacterState(CharacterState.Telekinesis);
                        break;
                    case InteractableObjectType.Grapple:
                        playerManager.ChangeCharacterState(CharacterState.Grapple);
                        break;
                }

                if (!_interactablesInRange[0].enabled) //If was destroyed from Interaction
                {
                    _interactablesInRange.Remove(_interactablesInRange[0]); //Reset highlight
                }

                OnInteractAbility?.Invoke(_interactablesInRange[0]);
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
                        _interactablesInRange.Add(interactable);
                        CheckListOrder(interactable);
                    }
                }
            }

        }
    }

    /// <summary>
    /// Checks if the given interactable is the first in the list to highlight it
    /// </summary>
    /// <param name="interactable"></param>
    private void CheckListOrder(InteractableObject interactable)
    {
        if (_interactablesInRange[0] == interactable)
        {
            interactable.ShowOutline();
        }
    }


    /// <summary>
    /// When object leaves sight, if it is the object that is currently higlighted, cancel highlight
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerExit(Collider other)
    {
        if (_interactablesInRange.Count == 0) return; //If there's no interactable in range, no need to check collision
        
        if (other.TryGetComponent<InteractableObject>(out InteractableObject interactable))
        {
            if (_interactablesInRange.Contains(interactable))
            {
                if (_interactablesInRange[0] == interactable) //If the interactable is highlighted
                {
                    interactable.RemoveOutline();
                    _interactablesInRange.Remove(interactable);
                    if (_interactablesInRange.Count > 0) //If there's another interactable in range currently
                    {
                    _interactablesInRange[0].ShowOutline();
                    }
                }
                else
                {
                    _interactablesInRange.Remove(interactable);
                }
            }
        }
    }

}
