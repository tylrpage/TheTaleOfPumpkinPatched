using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWithBook : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Vector3 _offsetFromPagePivot;

    private void Start()
    {
        _initialPosition = transform.position;
        _offsetFromPagePivot = _initialPosition - GameManager.Instance.BookManager.LeftPage.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_initialPosition.x < GameManager.Instance.BookManager.RightPage.position.x)
        {
            // Rotate according to left page
            transform.rotation =
                Quaternion.LookRotation(transform.forward, -GameManager.Instance.BookManager.LeftPage.up);
            Quaternion rot = Quaternion.Inverse(Quaternion.AngleAxis(180, Vector3.back));

            transform.position = GameManager.Instance.BookManager.LeftPage.position + rot *
                (Quaternion.Inverse(GameManager.Instance.BookManager.LeftPage.localRotation) * _offsetFromPagePivot);
        }
    }
}