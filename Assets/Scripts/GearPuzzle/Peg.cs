using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    [SerializeField] private int desiredGearNumber;
    [SerializeField] private GearPuzzleManager gearPuzzleManager;

    public Gear currentGear;

    public void Clicked()
    {
        if (currentGear != null)
        {
            if (gearPuzzleManager.activeGear == null)
            {
                currentGear.Clicked();
            }
            //swaps gears
            else if (gearPuzzleManager.activeGear.currentPeg != null)
            {
                Gear prevGear = currentGear;
                gearPuzzleManager.activeGear.currentPeg.AssignGear(prevGear);
                AssignGear(gearPuzzleManager.activeGear);
                gearPuzzleManager.activeGear = null;
            }
            else
            {
                Gear prevGear = currentGear;
                prevGear.SendHome();
                AssignGear(gearPuzzleManager.activeGear);
                gearPuzzleManager.activeGear = null;
            }
        }
        else if (gearPuzzleManager.activeGear != null)
        {
            if (gearPuzzleManager.activeGear.currentPeg != null)
            {
                gearPuzzleManager.activeGear.currentPeg.currentGear = null;
            }
            
            AssignGear(gearPuzzleManager.activeGear);
            gearPuzzleManager.activeGear = null;
        }
        gearPuzzleManager.AllCorrect();
    }

    private void AssignGear(Gear gear)
    {
        currentGear = gear;
        gear.transform.position = transform.position;
        gear.currentPeg = this;
    }

    public bool Correct()
    {
        if (currentGear == null)
        {
            return false;
        }
        else if (currentGear.gearNumber == desiredGearNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
    void Update()
    {
        
    }
}
