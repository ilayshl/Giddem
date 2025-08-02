using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixerGroup audioMixer;
    private AudioSource _audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

        _audioSource = FindFirstObjectByType<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
        
        
    }

    public void FadeSound(AudioMixerGroup audioGroup, bool isPlayingNow, float timeDuration)
    {

    }

}
