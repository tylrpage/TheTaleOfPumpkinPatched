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

    private int currentPage = -1;

    private void Start()
    {
        animator.Play("closed");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            NextPage();
        }
    }

    public void OpenFirstPage()
    {
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
        else
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
    }
}
