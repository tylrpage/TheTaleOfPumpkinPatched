using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float talkingCooldown;

    [NonSerialized] public bool IsCurrentlyTalking;

    private int _pageIndex;
    private List<string> _textPages;

    private void Awake()
    {
        HideDialogue();
    }

    private void Update()
    {
        if (IsCurrentlyTalking && Input.GetButtonDown("Interact"))
        {
            ShowNextPage();
        }
    }

    public void StartShowDialogue(List<string> textPages)
    {
        IsCurrentlyTalking = true;
        dialoguePanel.SetActive(true);
        _textPages = textPages;
        
        // -1 so that ShowNextPage goes to the first page
        _pageIndex = -1;
        ShowNextPage();
    }

    private void ShowNextPage()
    {
        _pageIndex++;
        if (_pageIndex < _textPages.Count)
        {
            string pageText = _textPages[_pageIndex];
            dialogueText.text = pageText;
        }
        else
        {
            HideDialogue();
        }
    }

    private void HideDialogue()
    {
        _textPages = null;
        dialoguePanel.SetActive(false);
        StartCoroutine(AllowTalkingAfterTime());
    }

    private IEnumerator AllowTalkingAfterTime()
    {
        yield return new WaitForSeconds(talkingCooldown);
        IsCurrentlyTalking = false;
    }
}
