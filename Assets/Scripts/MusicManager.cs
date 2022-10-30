using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> songs;
    [SerializeField] private float crossFadeTime;
    [SerializeField] private float barLength;

    private int _currentSongIndex;

    public void StartMusic()
    {
        _currentSongIndex = 0;
        audioSource.Play();
        audioSource.loop = true;

        // Preload next song
        if (songs.Count > 1)
        {
            songs[1].LoadAudioData();
        }
        
        StartCoroutine(QueueSongRoutine(_currentSongIndex));
    }

    public void IncrementMusic()
    {
        _currentSongIndex++;
        StartCoroutine(QueueSongRoutine(_currentSongIndex));
    }

    private IEnumerator QueueSongRoutine(int newIndex)
    {
        float timeToWait;
        if (audioSource.clip == null)
        {
            timeToWait = 0;
        }
        else
        {
            float sourceTime = (float)audioSource.timeSamples / audioSource.clip.frequency;
            timeToWait = barLength - sourceTime;
            if (timeToWait < crossFadeTime)
            {
                timeToWait += barLength;
            }
        }
        
        // Preload the next clip
        if (songs.Count < newIndex + 1)
        {
            songs[newIndex + 1].LoadAudioData();
        }
        
        // Create a new audio source to "pre-load" the next clip
        AudioSource newAudioSource = audioSource.gameObject.AddComponent<AudioSource>();
        AudioClip clip = songs[newIndex];
        newAudioSource.clip = clip;
        newAudioSource.playOnAwake = false;
        // We sync up the time with the current audio source, and play it silently
        newAudioSource.volume = 0;
        newAudioSource.loop = true;
        newAudioSource.Play();

        yield return new WaitForSeconds(timeToWait - crossFadeTime);
        
        newAudioSource.timeSamples = audioSource.timeSamples;
        if (audioSource.clip == null)
        {
            // Instantly transition if the audio source wasnt originally playing anything
            newAudioSource.volume = audioSource.volume;
            audioSource.volume = 0;
        }
        else
        {
            // Cross fade
            float finalVolume = audioSource.volume;
            for (float i = 0; i < crossFadeTime; i+=Time.deltaTime)
            {
                float norm = i / crossFadeTime;
                newAudioSource.volume = norm * finalVolume;
                audioSource.volume = (1 - norm) * finalVolume;
                yield return null;
            }
        }
        
        // When the current clip has ended, switch out the audio sources
        if (audioSource.clip != null)
        {
            audioSource.clip.UnloadAudioData();
        }
        Destroy(audioSource);
        audioSource = newAudioSource;
    }
}
