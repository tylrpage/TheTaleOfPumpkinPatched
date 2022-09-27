using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float lerpSpeed;

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + cameraOffset;

        Vector3 actualPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        
        transform.position = actualPosition;
    }
}
