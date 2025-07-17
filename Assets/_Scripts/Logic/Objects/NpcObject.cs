using UnityEngine;
using TMPro;

public class NpcObject : InteractableObject
{
    [SerializeField] private GameObject npcDialogCanvas; // Reference to NpcDialogCanvas
    [SerializeField] private TextMeshProUGUI dialogText; // Reference to Dialog TextBox/Text component

    [TextArea]
    [SerializeField] private string[] dialogLines;

    private int currentLine = 0;

    public override void OnInteract(CharacterManager player)
    {
        currentLine = 0;

        if (npcDialogCanvas != null)
            npcDialogCanvas.SetActive(true);

        if (dialogText != null && dialogLines.Length > 0)
            dialogText.text = dialogLines[currentLine];

        player.ChangeCharacterState(CharacterState.Dialog);
    }

    public void ContinueDialog(CharacterManager player)
    {
        currentLine++;

        if (currentLine < dialogLines.Length)
        {
            if (dialogText != null)
                dialogText.text = dialogLines[currentLine];
        }
        else
        {
            ExitDialog(player);
        }
    }

    public void ExitDialog(CharacterManager player)
    {
        currentLine = 0;

        if (npcDialogCanvas != null)
            npcDialogCanvas.SetActive(false);

        player.ChangeCharacterState(CharacterState.Idle);
    }
}
