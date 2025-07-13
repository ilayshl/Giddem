using System.Collections;
using UnityEngine;

public class PlayerTelekinesis : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract playerInteract;
    private InteractableObject objectControlled;

    void OnEnable()
    {
        playerInteract.OnInteractAbility += SetTelekinesisObject;
    }

    public void SetTelekinesisObject(InteractableObject objectToControl)
    {
        if (playerManager.state == CharacterState.Telekinesis)
        {
            objectControlled = objectToControl;
        }
    }

    private IEnumerator controlObject()
    {
        while (objectControlled != null)
        {
            MoveObject();
            yield return new WaitForFixedUpdate();
        }
    }

    private void MoveObject()
    {

    }
}
