using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource musicSource, soundSource, dialogueSource;
    private const float MUSIC_VOLUME = 0.5f;
    
    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip music, float fadeDuration = 0)
    {
        if (fadeDuration <= 0) //If no fade duration was given, play the new clip immediately
        {
            musicSource.Stop();
            musicSource.clip = music;
            musicSource.Play();
        }
        else
        {
            StartCoroutine(SwitchSound(musicSource, music, fadeDuration));
        }
    }

    public void StopMusic(float fadeDuration)
    {
        if (musicSource.isPlaying)
        {
            if (fadeDuration <= 0)
            {
                ResetSound(musicSource);
            }
            else
            {
                StartCoroutine(FadeOutSound(musicSource, fadeDuration));
            }
        }
    }

    public IEnumerator SwitchSound(AudioSource source, AudioClip music, float fadeDuration)
    {
        float originalVolume = MUSIC_VOLUME; //Gets the current volume for later
        if (source.isPlaying) //If there's already music playing, fade it out first
        {
            yield return FadeOutSound(source, fadeDuration);
        }

        source.clip = music; //Set the new clip
        source.Play(); //Start it
        while (source.volume < originalVolume) //Gradually get to the previous volume
        {
            source.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
            source.volume = originalVolume; //Reset it to what is was before
    }

    private IEnumerator FadeOutSound(AudioSource source, float fadeDuration)
    {
        while (source.volume > 0) //Gradually lower the volume
        {
            source.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        ResetSound(source);
    }

    private void ResetSound(AudioSource source)
    {
        source.clip = null;
    }

}
