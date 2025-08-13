using System.Collections;
using UnityEngine;

/// <summary>
/// Controls all sounds playing in the game.
/// Use PlaySound, PlayMusic and PlayDialogue.
/// </summary>
public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource musicSource, soundSource, dialogueSource;
    private const float ORIGINAL_VOLUME = 0.5f;

    /// <summary>
    /// Play a specific sound on soundSource.
    /// </summary>
    public void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    /// <summary>
    /// Plays the selected track in the Music source.
    /// If given fade time, also fades the track.
    /// </summary>
    /// <param name="music"></param>
    /// <param name="fadeDuration"></param>
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
            StartCoroutine(SwitchClip(musicSource, music, fadeDuration));
        }
    }

    /// <summary>
    /// Stop current playing track.
    /// If given fade value, fades out the track.
    /// </summary>
    /// <param name="fadeDuration"></param>
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

    /// <summary>
    /// Plays the given clip at the Dialogue source.
    /// </summary>
    /// <param name="dialogue"></param>
    public void PlayDialogue(AudioClip dialogue)
    {
        ReplaceClip(dialogueSource, dialogue);
    }

    /// <summary>
    /// Fade out currently playing music, then fade in new given track.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="music"></param>
    /// <param name="fadeDuration"></param>
    /// <returns></returns>
    private IEnumerator SwitchClip(AudioSource source, AudioClip clip, float fadeDuration)
    {
        if (source.isPlaying) //If there's already music playing, fade it out first
        {
            yield return FadeOutSound(source, fadeDuration);
        }

        source.clip = clip; //Set the new clip
        source.Play(); //Start it
        while (source.volume < ORIGINAL_VOLUME) //Gradually get to the previous volume
        {
            source.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
        source.volume = ORIGINAL_VOLUME; //Reset it to what is was before
    }

    /// <summary>
    /// Fades out the currently playing sound with the float duration value.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fadeDuration"></param>
    /// <returns></returns>
    private IEnumerator FadeOutSound(AudioSource source, float fadeDuration)
    {
        while (source.volume > 0) //Gradually lower the volume
        {
            source.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        ResetSound(source);
    }

    /// <summary>
    /// Instantly replaces the currently played clip at the given AudioSource.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="newClip"></param>
    private void ReplaceClip(AudioSource source, AudioClip newClip)
    {
        if (source.isPlaying)
        {
        source.Stop();
        }
        ResetSound(source);
        source.clip = newClip;
        source.Play();
    }

    /// <summary>
    /// Resets the current clip of the given AudioSource.
    /// </summary>
    /// <param name="source"></param>
    private void ResetSound(AudioSource source)
    {
        source.clip = null;
    }

}
