using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
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
}
