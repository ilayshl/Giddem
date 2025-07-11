using System.Collections;
using UnityEngine;

public class PlayerTelekinesis : MonoBehaviour
{
    [SerializeField] CharacterManager playerManager;
    private TelekinesisObject objectControlled;

    public void SetTelekinesisObject(TelekinesisObject objectToControl)
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
