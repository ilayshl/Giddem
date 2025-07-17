using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private CharacterManager playerManager;
    [SerializeField] private InteractableObjectType[] objectTypeToCollide;
    [SerializeField] private KeyCode inputKey = KeyCode.E;

    // Reference the GameObject that holds the interact prompt UI text
    [SerializeField] private GameObject interactPrompt;

    private InteractableObject _highlightedObject;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(inputKey))
        {
            CharacterState state = playerManager.state;

            if (_highlightedObject != null && (state == CharacterState.Idle || state == CharacterState.Run))
            {
                _highlightedObject.OnInteract(playerManager);

                // Change state if needed
                switch (_highlightedObject.ObjectType)
                {
                    case InteractableObjectType.Grapple:
                        playerManager.ChangeCharacterState(CharacterState.Grapple);
                        break;
                    case InteractableObjectType.Telekinesis:
                        playerManager.ChangeCharacterState(CharacterState.Telekinesis);
                        break;
                }

                if (!_highlightedObject.enabled)
                {
                    _highlightedObject = null;
                }

                if (interactPrompt != null)
                    interactPrompt.SetActive(false);
            }
            else if (state == CharacterState.Dialog)
            {
                if (_highlightedObject is NpcObject npc)
                {
                    npc.ContinueDialog(playerManager);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject interactable))
        {
            foreach (var objectType in objectTypeToCollide)
            {
                if (objectType == interactable.ObjectType)
                {
                    _highlightedObject = interactable;

                    if (interactPrompt != null)
                        interactPrompt.SetActive(true);

                    return;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject interactable))
        {
            if (interactable == _highlightedObject)
            {
                _highlightedObject = null;

                if (interactPrompt != null)
                    interactPrompt.SetActive(false);
            }
        }
    }
}
