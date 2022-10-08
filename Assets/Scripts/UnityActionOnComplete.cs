using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityActionOnComplete : MonoBehaviour
{
    [SerializeField] private Interactable trigger;
    public UnityEvent action;

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
        action.Invoke();
    }
}
