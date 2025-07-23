using UnityEngine;

public class GrappleObject : InteractableObject//, IGrappleable
{
    public override void OnInteract()
    {

    }
    
    protected override void ObjectTypeInitiate()
    {
        objectType = InteractableObjectType.Grapple;
    }

}
