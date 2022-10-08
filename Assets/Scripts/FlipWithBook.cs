using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWithBook : MonoBehaviour
{
    [SerializeField] private bool disableFlip;
    
    private Vector3 _initialPosition;
    private Vector3 _offsetFromPagePivot;
    private Quaternion _initialRotation;
    private BookManager _bookManager;
    private bool _isLeft;

    private void Start()
    {
        _bookManager = GameManager.Instance.BookManager;
        _bookManager.RegisterFlipper(this);
        
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _offsetFromPagePivot = _initialPosition - _bookManager.LeftPage.position;
        
        _isLeft = _initialPosition.x < GameManager.Instance.BookManager.RightPage.position.x;
        
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
    }

    public void StartFlip()
    {
        if (!disableFlip)
        {
            StartCoroutine(FlipUp());
        }

        StartCoroutine(WaitAndShow());
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion flipDown = disableFlip ? Quaternion.identity : Quaternion.AngleAxis(_bookManager.FlipRotation, Vector3.right);
        
        if (_isLeft)
        {
            // Rotate according to left page
            Quaternion rot = Quaternion.LookRotation(_initialRotation * Vector3.forward, -_bookManager.LeftPage.up);
            transform.rotation = rot * flipDown;
            
            // Position
            Quaternion adjustment = Quaternion.Inverse(Quaternion.AngleAxis(180, Vector3.back));
            transform.position = GameManager.Instance.BookManager.LeftPage.position + adjustment *
                (Quaternion.Inverse(GameManager.Instance.BookManager.LeftPage.localRotation) * _offsetFromPagePivot);
        }
        else
        {
            if (!disableFlip)
            {
                transform.rotation = _initialRotation * flipDown;
            }
        }
    }

    private IEnumerator WaitAndShow()
    {
        yield return new WaitForSeconds(_bookManager.RightVisableDelay);
            
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;
        }
    }

    private IEnumerator FlipUp()
    {
        yield return new WaitForSeconds(_bookManager.FlipDelay);

        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            _bookManager.FlipRotation = Mathf.Lerp(80, 0, t);
            yield return null;
        }
    }
}