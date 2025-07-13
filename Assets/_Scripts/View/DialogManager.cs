using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject pressEPrompt;
    private InteractableObject currentTarget;
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && currentTarget != null)
        {
            CharacterManager playerCharacter = GetComponent<CharacterManager>();
            if (playerCharacter != null)
            {
                currentTarget.OnInteract(playerCharacter);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Interactable")) return;

        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            currentTarget = interactable;
        }

        isPlayerInRange = true;
        if (pressEPrompt != null) pressEPrompt.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable")) return;

        if (currentTarget != null && other.GetComponent<InteractableObject>() == currentTarget)
        {
            currentTarget = null;
        }

        isPlayerInRange = false;
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
    }

}
