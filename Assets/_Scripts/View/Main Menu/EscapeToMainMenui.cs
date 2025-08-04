using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToMainMenui : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneHandler.Instance.SetSceneToTransition(0);
        }
    }
}
