using UnityEngine;

public class TelekinesisObject : InteractableObject//, ITelekinesisable
{
    public override void OnInteract()
    {

    }

    protected override void ObjectTypeInitiate()
    {
        objectType = InteractableObjectType.Telekinesis;
    }
}
