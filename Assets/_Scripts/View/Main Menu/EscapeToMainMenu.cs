using UnityEngine;

/// <summary>
/// Changes to Main Menu when given the correct input.
/// </summary>
public class EscapeToMainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneHandler.Instance.SetSceneToTransition(0);
        }
    }
}
