using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<string> leftPageTexts;
    [SerializeField] private List<string> rightPageTexts;
    [SerializeField] private TMP_Text leftPage;
    [SerializeField] private TMP_Text rightPage;
    [SerializeField] private TMP_Text nextLeftPage;
    [SerializeField] private TMP_Text nextRightPage;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float playerFollowWaitTime;
    [SerializeField] private float targetFogDensity;
    [SerializeField] private float fogFadeInTime;
    [SerializeField] private Light directionalLight;
    [SerializeField] private float playingLightIntensity;
    [SerializeField] private float introLightIntensity;
    [SerializeField] private GameObject player;
    
    private int currentPage = -1;
    private bool _finishedIntro;

    private void Start()
    {
        cameraController.GotoIntroPosition(0, 1, true);
        animator.Play("closed");
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.005f;
        directionalLight.intensity = introLightIntensity;
        player.SetActive(false);
    }

    private void Update()
    {
        if (!_finishedIntro && Input.GetButtonDown("Interact"))
        {
            NextPage();
        }
    }

    public void OpenFirstPage()
    {
        cameraController.GotoIntroPosition(1, 2f);
        
        string leftText = leftPageTexts[0];
        string rightText = rightPageTexts[0];

        leftPage.text = leftText;
        rightPage.text = rightText;

        nextLeftPage.enabled = false;
        nextRightPage.enabled = false;
        
        animator.Play("openBook");
    }

    public void NextPage()
    {
        currentPage++;
        if (currentPage == 0)
        {
            OpenFirstPage();
        }
        else if(currentPage < leftPageTexts.Count && currentPage < rightPageTexts.Count)
        {
            string leftText = leftPageTexts[currentPage];
            string rightText = rightPageTexts[currentPage];
            
            if (currentPage != 1)
            {
                string oldLeftText = leftPageTexts[currentPage - 1];
                string oldRightText = rightPageTexts[currentPage - 1];
                leftPage.text = oldLeftText;
                rightPage.text = oldRightText;
            }

            nextLeftPage.enabled = true;
            nextRightPage.enabled = true;
            nextLeftPage.text = leftText;
            nextRightPage.text = rightText;
            
            animator.Play("flipPageText", -1, 0f);
        }
        else
        {
            _finishedIntro = true;
            cameraController.GotoIntroPosition(2, 2f);
            animator.Play("flipPageGame");
            GameManager.Instance.BookManager.FlipAll();
            player.SetActive(true);
            StartCoroutine(WaitAndFollowPlayer());
            StartCoroutine(FadeInFogAndDimLight());
        }
    }

    private IEnumerator FadeInFogAndDimLight()
    {
        RenderSettings.fog = true;
        for (float i = 0; i < fogFadeInTime; i += Time.deltaTime)
        {
            float iNorm = i / fogFadeInTime;
            RenderSettings.fogDensity = Mathf.Lerp(0.005f, targetFogDensity, iNorm);
            directionalLight.intensity = Mathf.Lerp(introLightIntensity, playingLightIntensity, iNorm);
            yield return null;
        }
    }

    private IEnumerator WaitAndFollowPlayer()
    {
        yield return new WaitForSeconds(playerFollowWaitTime);
        cameraController.FollowPlayer();
    }
}
