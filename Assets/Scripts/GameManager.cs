using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public DialogueManager DialogueManager;
    public FlagManager FlagManager;
    public BookManager BookManager;
    public IntroManager IntroManager;
    public OutroManager OutroManager;
    public TutorialManager TutorialManager;
    public MusicManager MusicManager;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
