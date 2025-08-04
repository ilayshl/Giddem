using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    [SerializeField] private AudioClip musicClip;
    public Action<SceneType> OnSceneChanged;
    private int _sceneToTransition;

    void Start()
    {
        SoundManager.Instance.PlayMusic(musicClip, FadeUI.Instance.EnableDuration);
        FadeUI.Instance.Fade();
    }

    public void TransitionScene()
    {
        FadeUI.Instance.OnEnableFinish -= TransitionScene;
        SceneManager.LoadScene(_sceneToTransition);
    }

    /// <summary>
    /// 0 = Main Menu, 1 = New game, 2 = Sandbox
    /// </summary>
    /// <param name="scene"></param>
    public void SetSceneToTransition(int scene)
    {
        _sceneToTransition = scene;
        SoundManager.Instance.StopMusic(FadeUI.Instance.DisableDuration);
        FadeUI.Instance.Fade();
        FadeUI.Instance.OnEnableFinish += TransitionScene;
    }

    

    
}
