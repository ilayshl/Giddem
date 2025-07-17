using UnityEngine;

public class TelekinesisObject : InteractableObject//, ITelekinesisable
{
    public override void OnInteract()
    {
        Debug.Log("Telekinesis Ability");
    }
}
