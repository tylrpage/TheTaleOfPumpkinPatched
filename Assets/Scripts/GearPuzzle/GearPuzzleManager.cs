using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPuzzleManager : MonoBehaviour
{
    public Gear activeGear;
    public event Action Completed;

    [SerializeField] private List<Peg> Pegs;
    [SerializeField] private List<Gear> Gears;
    [SerializeField] private GameObject VictoryPanel;

    private bool _done;

    private void Start()
    {
        VictoryPanel.SetActive(false);
    }

    public bool AllCorrect()
    {
        foreach (var peg in Pegs)
        {
            if (!peg.Correct())
            {
                return false;
            }
        }

        foreach (var gear in Gears)
        {
            gear.SpinGears();
        }
        
        VictoryPanel.SetActive(true);
        _done = true;
        return true;
    }

    private void Update()
    {
        if (_done && Input.GetButtonDown("Interact"))
        {
            _done = false;
            Completed?.Invoke();
        }
    }
}
