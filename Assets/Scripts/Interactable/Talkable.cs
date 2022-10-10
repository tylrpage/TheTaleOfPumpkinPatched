using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : Interactable
{
    [SerializeField] private List<string> dialoguePages;
    
    public override void Interact()
    {
        GameManager.Instance.DialogueManager.StartShowDialogue(dialoguePages, Complete);
    }

    public void ForceInteract()
    {
        GameManager.Instance.DialogueManager.StartShowDialogue(dialoguePages, Complete, true);
    }
}
