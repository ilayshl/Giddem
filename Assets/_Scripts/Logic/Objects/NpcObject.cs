using UnityEngine;

public class NpcObject : InteractableObject
{
    public override void OnInteract()
    {
        Debug.Log("NPC Dialogue");
    }
}
