using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"{name}: Hi ghostie!");
    }
}
