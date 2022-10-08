using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightable : MonoBehaviour
{
    [SerializeField] private Interactable trigger;
    [SerializeField] private Renderer lightRenderer;
    [SerializeField] private float fadeInTime;
    [SerializeField] private Light light;
    [SerializeField] private float finalIntensity;

    private void Awake()
    {
        trigger.Completed += OnCompleted;
        
        var material = lightRenderer.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
        if (light != null)
        {
            light.intensity = 0;
        }
    }

    private void OnDestroy()
    {
        trigger.Completed -= OnCompleted;
    }

    private void OnCompleted()
    {
        StartCoroutine(FadeInLight());
    }

    private IEnumerator FadeInLight()
    {
        for (float i = 0; i < fadeInTime; i += Time.deltaTime)
        {
            float iNorm = i / fadeInTime;
            var material = lightRenderer.material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, iNorm);
            if (light != null)
            {
                light.intensity = Mathf.Lerp(0, finalIntensity, iNorm);
            }
            yield return null;
        }
    }
}
