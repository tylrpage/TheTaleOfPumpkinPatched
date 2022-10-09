using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private GameObject walkPanel;
    [SerializeField] private Interactor playerInteractor;
    
    private void Start()
    {
        talkPanel.SetActive(false);
        walkPanel.SetActive(false);
    }

    public void StartTalkTutorial()
    {
        talkPanel.SetActive(true);
    }

    public void StartWalkTutorial()
    {
        walkPanel.SetActive(true);
        playerInteractor.enabled = false;
    }

    private void Update()
    {
        if (talkPanel.activeSelf && Input.GetButtonDown("Interact"))
        {
            talkPanel.SetActive(false);
        }
        if (walkPanel.activeSelf && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            walkPanel.SetActive(false);
            playerInteractor.enabled = true;
        }
    }
}
