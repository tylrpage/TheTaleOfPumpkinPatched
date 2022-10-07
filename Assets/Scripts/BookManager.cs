using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public Transform LeftPage;
    public Transform RightPage;
    public float FlipRotation;
    public float FlipDelay;
    public float RightVisableDelay;
    [SerializeField] private Animator bookAnimator;

    private List<FlipWithBook> _flippers = new List<FlipWithBook>();

    public void RegisterFlipper(FlipWithBook flipper)
    {
        _flippers.Add(flipper);
    }

    public void FlipAll()
    {
        bookAnimator.Play("firstPage");
        foreach (FlipWithBook flipper in _flippers)
        {
            flipper.StartFlip();
        }
    }
}
