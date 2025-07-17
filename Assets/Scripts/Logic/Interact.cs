using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pressEPrompt;
    [SerializeField] private GameObject dialogWindow;

    private bool isPlayerInRange = false;

    private void Start()
    {
        // Make sure UI is hidden at start
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
        if (dialogWindow != null) dialogWindow.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialog();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (pressEPrompt != null) pressEPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (pressEPrompt != null) pressEPrompt.SetActive(false);
        }
    }

    private void TriggerDialog()
    {
        if (pressEPrompt != null) pressEPrompt.SetActive(false);
        if (dialogWindow != null) dialogWindow.SetActive(true);

        // Here you can hook into your own dialog system
        // For example: DialogSystem.Instance.StartDialog(dialogData);
    }
}
