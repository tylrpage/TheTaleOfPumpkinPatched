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
        yield return new WaitForSeconds(timeToWait);
        
        AudioClip clip = songs[newIndex];
        audioSource.clip = clip;
        audioSource.Play();
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
