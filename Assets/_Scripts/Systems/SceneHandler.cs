using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public Action<SceneType> OnSceneChanged;

    public void TransitionScene(SceneType scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    
}
