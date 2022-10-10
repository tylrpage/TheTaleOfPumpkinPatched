using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPipe : MonoBehaviour
{
    [SerializeField] private Talkable trigger;
    [SerializeField] private Talkable action;

    private void Awake()
    {
        trigger.Completed += TriggerOnCompleted;
    }

    private void OnDestroy()
    {
        trigger.Completed -= TriggerOnCompleted;
    }

    private void TriggerOnCompleted()
    {
        if (action.CanInteract())
        {
            action.ForceInteract();
            // Only do it once
            Destroy(this);
        }
    }
}
