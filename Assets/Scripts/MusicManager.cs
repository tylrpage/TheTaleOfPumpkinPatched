using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> songs;
    [SerializeField] private float fadeInTime;

    private int _currentSongIndex;

    public void StartMusic()
    {
        _currentSongIndex = 0;
        audioSource.Play();
        audioSource.loop = true;
        //StartCoroutine(Routine());
        
        StartCoroutine(QueueSongRoutine(_currentSongIndex));
    }

    public void IncrementMusic()
    {
        _currentSongIndex++;
        StartCoroutine(QueueSongRoutine(_currentSongIndex));
    }

    private IEnumerator QueueSongRoutine(int newIndex)
    {
        float clipLength = audioSource.clip != null ? audioSource.clip.length : 0;
        float timeToWait = clipLength - audioSource.time;
        if (timeToWait < 0.1f)
        {
            timeToWait += clipLength;
        }
        
        // Create a new audio source to "pre-load" the next clip
        AudioSource newAudioSource = audioSource.gameObject.AddComponent<AudioSource>();
        AudioClip clip = songs[newIndex];
        newAudioSource.clip = clip;
        // We sync up the time with the current audio source, and play it silently
        newAudioSource.volume = 0;
        newAudioSource.loop = true;
        newAudioSource.time = audioSource.time;
        newAudioSource.Play();
        
        yield return new WaitForSeconds(timeToWait);
        
        // When the current clip has ended, switch out the audio sources
        newAudioSource.volume = audioSource.volume;
        Destroy(audioSource);
        audioSource = newAudioSource;
    }

    private IEnumerator Routine()
    {
        for (float i = 0; i < fadeInTime; i += Time.deltaTime)
        {
            float iNorm = i / fadeInTime;
            audioSource.volume = iNorm;
            yield return null;
        }
    }
}
