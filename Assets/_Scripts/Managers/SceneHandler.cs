using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    [SerializeField] private AudioClip musicClip;
    public Action<SceneType> OnSceneChanged;

    void Start()
    {
        SoundManager.Instance.PlayMusic(musicClip, 2);
        Debug.Log("[SceneHandler] Played sound.");
    }

    public void TransitionScene(SceneType scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    
}
