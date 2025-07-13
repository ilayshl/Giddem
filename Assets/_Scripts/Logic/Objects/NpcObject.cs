using UnityEngine;

public class NpcObject : InteractableObject
{
    [SerializeField] private GameObject dialogWindow;

    private void Start()
    {
        if (dialogWindow != null) dialogWindow.SetActive(false);
    }

    public override void OnInteract(CharacterManager character)
    {
        Debug.Log($"Interacting with NPC: {ObjectName}");

        character.ChangeCharacterState(CharacterState.Dialog);

        if (dialogWindow != null)
        {
            dialogWindow.SetActive(true);
        }
    }

}
