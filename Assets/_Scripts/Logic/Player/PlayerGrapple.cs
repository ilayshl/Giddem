using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract abilityInteract;

    private GrappleObject grappleTarget;

    private void OnEnable()
    {
        playerManager.OnCharacterStateChanged += GetCharacterState;
        abilityInteract.OnInteractAbility += GetInteractedObject;
    }

    private void OnDisable()
    {
        playerManager.OnCharacterStateChanged -= GetCharacterState;
        abilityInteract.OnInteractAbility -= GetInteractedObject;
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

    private void GetInteractedObject(InteractableObject interactedObect)
    {
        if (interactedObect.TryGetComponent<GrappleObject>(out GrappleObject objectToGrapple))
        {
            if (playerManager.state == CharacterState.Telekinesis)
            {
            grappleTarget = objectToGrapple;
            }
        }
    }

}
