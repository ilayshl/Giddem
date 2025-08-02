using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource musicSource, soundSource, dialogueSource;

    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip music, float fadeDuration = 0)
    {
        if (fadeDuration <= 0)
        {
            musicSource.Stop();
            musicSource.clip = music;
            musicSource.Play();
        }
        else
        {
            StartCoroutine(SwitchMusic(musicSource, music, fadeDuration));
        }
    }

    public IEnumerator SwitchMusic(AudioSource source, AudioClip music, float fadeDuration)
    {
        float volume = source.volume;
        if (source.isPlaying)
        {
            yield return FadeSound(source, fadeDuration);
        }

        source.clip = music;
        source.Play();
        while (source.volume < volume)
        {
            source.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    private IEnumerator FadeSound(AudioSource source, float fadeDuration)
    {
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        ResetClip(source);
    }

    private void ResetClip(AudioSource source)
    {
        source.clip = null;
    }

}
