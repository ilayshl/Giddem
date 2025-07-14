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
                if (pressEPrompt != null) pressEPrompt.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInRange = true;
        pressEPrompt.SetActive(true);
        if (!other.CompareTag("Interactable")) return;

        InteractableObject interactable = other.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            currentTarget = interactable;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable")) return;

        if (currentTarget != null && other.GetComponent<InteractableObject>() == currentTarget)
        {
            currentTarget = null;
        }

        isPlayerInRange = false;
    }

}
