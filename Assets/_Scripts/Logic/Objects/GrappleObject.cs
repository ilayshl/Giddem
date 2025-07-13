using UnityEngine;

public class GrappleObject : InteractableObject
{
    public override void OnInteract(CharacterManager character)
    {
        Debug.Log("Grapple to: " + ObjectName);
        Destroy(this.gameObject);
    }

}
