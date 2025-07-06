using UnityEngine;

public class ArmPickup : InteractableObject
{
    [SerializeField] private ArmData armData;

    public override void OnInteract()
    {
        Debug.Log("interacted");
        Destroy(this.gameObject);
    }

    
    
}
