using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractor : Interactable
{
    public bool FixingTractor { get; private set; }
    
    [SerializeField] private GearPuzzleManager gearPuzzle;
    [SerializeField] private Transform canvas;

    private GearPuzzleManager _gearPuzzleManager;
    
    public override void Interact()
    {
        _gearPuzzleManager = Instantiate(gearPuzzle, canvas);
        _gearPuzzleManager.Completed += GearPuzzleManagerOnCompleted;
        FixingTractor = true;
    }

    private void GearPuzzleManagerOnCompleted()
    {
        _gearPuzzleManager.Completed -= GearPuzzleManagerOnCompleted;
        Destroy(_gearPuzzleManager.gameObject);
        Complete();
        FixingTractor = false;
    }
}
