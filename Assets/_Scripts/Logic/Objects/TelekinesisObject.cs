using UnityEngine;

public class TelekinesisObject : InteractableObject//, ITelekinesisable
{
    public override void OnInteract(CharacterManager character)
    {
        Debug.Log("Controled: " + ObjectName);
        Destroy(this.gameObject);
    }
}
