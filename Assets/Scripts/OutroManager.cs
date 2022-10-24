using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform partner;
    [SerializeField] private Renderer cheeks;
    [SerializeField] private Talkable partnerTalkable;
    [SerializeField] private Vector3 partnerOffset;
    [SerializeField] private float partnerWalkTime;
    [SerializeField] private float blushTime;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float fadeTime;
    [SerializeField] private List<Renderer> ghostRenderers;
    [SerializeField] private float endCanvasFadeInTime;
    [SerializeField] private Graphic endScreenGraphic;
    [SerializeField] private Graphic endTextGraphic;

    private void Awake()
    {
        partnerTalkable.Completed += PartnerTalkableOnCompleted;
    }

    private void OnDestroy()
    {
        partnerTalkable.Completed -= PartnerTalkableOnCompleted;
    }

    private void PartnerTalkableOnCompleted()
    {
        cameraController.GotoIntroPosition(3, 10f);
        StartCoroutine(WalkDownAndFade());
        StartCoroutine(FadeInEnd());
    }

    public void PlayOutro()
    {
        StartCoroutine(OutroRoutine());
        GameManager.Instance.MusicManager.IncrementMusic();
    }

    private IEnumerator OutroRoutine()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Interactor>().enabled = false;
        partner.gameObject.SetActive(true);
        yield return StartCoroutine(MovePartner());
        yield return StartCoroutine(BlushCheeks());
        partnerTalkable.Interact();

        yield return null;
    }

    private IEnumerator MovePartner()
    {
        Vector3 partnerInitialPosition = player.position + partnerOffset + Vector3.back * 4;
        for (float i = 0; i < partnerWalkTime; i += Time.deltaTime)
        {
            float iNorm = i / partnerWalkTime;
            partner.position = Vector3.Lerp(partnerInitialPosition, player.position + partnerOffset, iNorm);
            yield return null;
        }
    }
    
    private IEnumerator BlushCheeks()
    {
        for (float i = 0; i < blushTime; i += Time.deltaTime)
        {
            float iNorm = i / blushTime;
            var material = cheeks.material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, iNorm);
            yield return null;
        }
    }

    private IEnumerator WalkDownAndFade()
    {
        for (float i = 0; i < fadeTime; i += Time.deltaTime)
        {
            player.position += Vector3.back * Time.deltaTime;
            partner.position += Vector3.back * Time.deltaTime;
            
            float iNorm = 1 - i / fadeTime;
            var material = cheeks.material;
            foreach (var renderer in ghostRenderers)
            {
                renderer.material.color = new Color(material.color.r, material.color.g, material.color.b, iNorm);
            }
            yield return null;
        }
    }
    
    private IEnumerator FadeInEnd()
    {
        for (float i = 0; i < endCanvasFadeInTime * 2; i += Time.deltaTime)
        {
            float iNorm = i / endCanvasFadeInTime;
            endScreenGraphic.color = new Color(endScreenGraphic.color.r, endScreenGraphic.color.g, endScreenGraphic.color.b, iNorm);

            float iEndText = iNorm - 0.5f;
            endTextGraphic.color = new Color(endTextGraphic.color.r, endTextGraphic.color.g, endTextGraphic.color.b, iEndText);
            yield return null;
        }
    }
}
