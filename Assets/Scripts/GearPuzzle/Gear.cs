using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

public class Gear : MonoBehaviour

{
    [SerializeField] public int gearNumber;
    [SerializeField] private Transform gearPos;
    [SerializeField] private GearPuzzleManager gearPuzzleManager;
    [SerializeField] private float spinSpeed;
    [SerializeField] private bool clockwise;
    
    public Peg currentPeg;
    private bool _shouldSpin;
    
    public void Clicked()
    {
        gearPuzzleManager.activeGear = this;
        
    }

    public void SendHome()
    {
        transform.position = gearPos.position;
        currentPeg = null;
    }
    void Start()
    {
        SendHome();
        
    }

    public void SpinGears()
    {
        _shouldSpin = true;
    }

    private void Update()
    {
        if (_shouldSpin == true)
        {
            float direction = clockwise ? -1 : 1 ;
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime * direction);
            
            

        }
    }
}
