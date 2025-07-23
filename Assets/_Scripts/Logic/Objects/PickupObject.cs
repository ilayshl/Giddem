using UnityEngine;

public class PickupObject : InteractableObject
{
    
    public override void OnInteract()
    {
        Debug.Log("interacted");
        Destroy(this.gameObject);
    }

    protected override void ObjectTypeInitiate()
    {
        objectType = InteractableObjectType.Interact;
    }
    
}
