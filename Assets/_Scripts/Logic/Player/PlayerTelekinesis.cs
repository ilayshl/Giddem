using System.Collections;
using UnityEngine;

public class PlayerTelekinesis : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract abilityInteract;
    private TelekinesisObject objectControlled;

    private void OnEnable()
    {
        playerManager.OnCharacterStateChanged += GetCharacterState;
        abilityInteract.OnInteractAbility += GetInteratedObject;
    }

    private void OnDisable()
    {
        playerManager.OnCharacterStateChanged -= GetCharacterState;
        abilityInteract.OnInteractAbility -= GetInteratedObject;
    }

    private void GetCharacterState(CharacterState state)
    {
        /* if (state == CharacterState.Telekinesis)
        {
            CameraMovement.Instance.ChangeCameraTarget(objectControlled.transform);
        }
        else if (CameraMovement.Instance.Target != objectControlled.transform)
        {
            CameraMovement.Instance.ChangeCameraTarget(playerManager.transform);
        } 
        Setting the camera's target to the object, then changing it back to the player*/
    }

    private void GetInteratedObject(InteractableObject interactedObect)
    {
        if (interactedObect.TryGetComponent<TelekinesisObject>(out TelekinesisObject objectToControl))
        {
            if (playerManager.state == CharacterState.Telekinesis)
            {
            objectControlled = objectToControl;
            }
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
