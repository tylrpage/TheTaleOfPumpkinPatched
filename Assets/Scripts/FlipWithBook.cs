using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWithBook : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Vector3 _offsetFromPagePivot;
    private Quaternion _initialRotation;
    private float _flipDownRotation = 80;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _offsetFromPagePivot = _initialPosition - GameManager.Instance.BookManager.LeftPage.position;

        
        StartCoroutine(FlipUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (_initialPosition.x < GameManager.Instance.BookManager.RightPage.position.x)
        {
            // Rotate according to left page
            Quaternion rot = Quaternion.LookRotation(_initialRotation * Vector3.forward, -GameManager.Instance.BookManager.LeftPage.up);
            Quaternion flipDown = Quaternion.AngleAxis(_flipDownRotation, Vector3.right);
            transform.rotation = rot * flipDown;
            //transform.rotation *= Quaternion.AngleAxis(90, transform.right);
            
            Quaternion adjustment = Quaternion.Inverse(Quaternion.AngleAxis(180, Vector3.back));
            transform.position = GameManager.Instance.BookManager.LeftPage.position + adjustment *
                (Quaternion.Inverse(GameManager.Instance.BookManager.LeftPage.localRotation) * _offsetFromPagePivot);
        }
        else
        {
            transform.rotation = _initialRotation * Quaternion.AngleAxis(_flipDownRotation, Vector3.right);
        }
    }

    private IEnumerator FlipUp()
    {
        yield return new WaitForSeconds(3);

        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            _flipDownRotation = Mathf.Lerp(80, 0, t);
            yield return null;
        }
    }
}