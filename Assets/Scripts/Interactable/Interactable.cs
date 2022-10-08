using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public int priority;
    [SerializeField] private List<string> requiredFlags;
    [SerializeField] private string completeFlag;

    public bool CanInteract()
    {
        return requiredFlags?.All(x => GameManager.Instance.FlagManager.HasFlag(x)) ?? true;
    }

    public abstract void Interact();

    public void Complete()
    {
        if (!string.IsNullOrEmpty(completeFlag))
        {
            GameManager.Instance.FlagManager.AddFlag(completeFlag);
        }
    }
}
