using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoverTutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float delay;
    [SerializeField] private float fadeTime;

    private void Start()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        text.alpha = 0;
        yield return new WaitForSeconds(fadeTime);
        for (float i = 0; i < fadeTime; i += Time.deltaTime)
        {
            float iNorm = i / fadeTime;
            text.alpha = Mathf.Lerp(0, 1, iNorm);
            yield return null;
        }
    }
}
