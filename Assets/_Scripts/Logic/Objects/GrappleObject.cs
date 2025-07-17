using UnityEngine;

public class GrappleObject : InteractableObject//, IGrappleable
{
    public override void OnInteract(CharacterManager character)
    {
        Debug.Log("Grapple to: " + ObjectName);
        Destroy(this.gameObject);
    }

}
