using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    private float _fadeInTime;

    public void StartMusic(float fadeInTime)
    {
        audioSource.Play();
        audioSource.loop = true;
        _fadeInTime = fadeInTime;
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        for (float i = 0; i < _fadeInTime; i += Time.deltaTime)
        {
            float iNorm = i / _fadeInTime;
            audioSource.volume = iNorm;
            yield return null;
        }
    }
}
