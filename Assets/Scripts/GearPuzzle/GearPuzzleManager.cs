using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPuzzleManager : MonoBehaviour
{
    public Gear activeGear;

    [SerializeField] private List<Peg> Pegs;
    [SerializeField] private List<Gear> Gears;
    [SerializeField] private GameObject VictoryPanel;

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
        return true;
    }


}
