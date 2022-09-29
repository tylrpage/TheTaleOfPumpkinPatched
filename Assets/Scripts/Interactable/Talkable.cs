using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour, IInteractable
{
    [SerializeField] private List<string> dialoguePages;
    
    public void Interact()
    {
        GameManager.Instance.DialogueManager.StartShowDialogue(dialoguePages);
    }
}
