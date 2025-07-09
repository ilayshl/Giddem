using UnityEngine;

public class PickupObject : InteractableObject
{
    [SerializeField] private ArmData armData;

    public override void OnInteract()
    {
        Debug.Log("interacted");
        Destroy(this.gameObject);
    }

    
    
}
