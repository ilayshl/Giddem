using System.Collections;
using UnityEngine;

public class PlayerTelekinesis : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    [SerializeField] PlayerInteract abilityInteract;
    private TelekinesisObject objectControlled;

    private void OnEnable()
    {
        playerManager.OnCharacterStateChanged += InitiateTelekinesis;
        abilityInteract.OnInteractAbility += GetInteratedObject;
    }

    private void OnDisable()
    {
        playerManager.OnCharacterStateChanged -= InitiateTelekinesis;
        abilityInteract.OnInteractAbility -= GetInteratedObject;
    }

    private void InitiateTelekinesis(CharacterState state)
    {
        if (state == CharacterState.Telekinesis)
        {
            
        }
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
