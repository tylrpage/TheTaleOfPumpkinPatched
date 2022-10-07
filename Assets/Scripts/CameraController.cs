using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Transform> introPositions;
    [SerializeField] private Quaternion followPlayerRotation;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private bool _followingPlayer;
    private float _initialLerpSpeed;

    private void Awake()
    {
        _initialLerpSpeed = lerpSpeed;
    }

    public void GotoIntroPosition(int index, float? speed, bool instant = false)
    {
        _followingPlayer = false;
        Transform targetTransform = introPositions[index];
        _targetPosition = targetTransform.position;
        _targetRotation = targetTransform.rotation;

        if (speed != null)
        {
            lerpSpeed = (float)speed;
        }

        if (instant)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }
    }

    public void FollowPlayer()
    {
        _followingPlayer = true;
    }

    private void LateUpdate()
    {
        if (_followingPlayer)
        {
            _targetPosition = player.position + cameraOffset;
            _targetRotation = followPlayerRotation;
            lerpSpeed = _initialLerpSpeed;
        }

        float lerpSpeedInd = 1 - Mathf.Pow(lerpSpeed, Time.deltaTime);
        float lerpRotInd = 1 - Mathf.Pow(rotationSpeed, Time.deltaTime);
        
        Vector3 actualPosition = Vector3.Lerp(transform.position, _targetPosition, lerpSpeedInd);
        Quaternion actualRotation = Quaternion.Lerp(transform.rotation, _targetRotation, lerpRotInd);
        
        transform.position = actualPosition;
        transform.rotation = actualRotation;
    }
}
