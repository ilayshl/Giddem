using UnityEngine;

public class PickupObject : InteractableObject
{
    public override void OnInteract(CharacterManager character)
    {
        Debug.Log("Picked up: " + ObjectName);
        Destroy(this.gameObject);
    }
}
