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
    [SerializeField] private float typingSpeed = 0.04f;

    // Used to tell other systems if a dialogue is currently being shown
    [NonSerialized] public bool IsCurrentlyTalking;

    // Used to have a cooldown between dialogues ending and new dialogues starting
    private bool _canStartDialogue;
    private bool _canContinueDialogue;
    private int _pageIndex;
    private List<string> _textPages;
    private Action _completeCallback;

    private void Awake()
    {
        HideDialogue();
    }

    private void Update()
    {
        if (IsCurrentlyTalking && Input.GetButtonDown("Interact") && _canContinueDialogue)
        {
            ShowNextPage();
        }
    }
    
    public void StartShowDialogue(List<string> textPages, Action completeCallback = null)
    {
        if (!_canStartDialogue)
        {
            return;
        }

        _canStartDialogue = false;
        _completeCallback = completeCallback;
        StartCoroutine(SetTalkingNextFrame());
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
            StartCoroutine(TypeDialogue(pageText));
        }
        else
        {
            HideDialogue();
            _completeCallback?.Invoke();
            _completeCallback = null;
        }
    }

    private void HideDialogue()
    {
        IsCurrentlyTalking = false;
        _textPages = null;
        dialoguePanel.SetActive(false);
        StartCoroutine(AllowTalkingAfterTime());
    }

    private IEnumerator AllowTalkingAfterTime()
    {
        yield return new WaitForSeconds(talkingCooldown);
        _canStartDialogue = true;
    }

    private IEnumerator SetTalkingNextFrame()
    {
        yield return null;
        IsCurrentlyTalking = true;
    }
    
    private IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        _canContinueDialogue = false;
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;

            if (letter != ' ') 
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        _canContinueDialogue = true;
    }
}
