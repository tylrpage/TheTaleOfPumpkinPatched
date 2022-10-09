using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinKingFollow : MonoBehaviour
{
    [SerializeField] private Interactable startTrigger;
    [SerializeField] private Interactable endTrigger;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Vector3 playerOffset;
    [SerializeField] private Transform player;
    [SerializeField] private float travelToPlayerTime;
    [SerializeField] private Transform kingTransform;
    [SerializeField] private GameObject kingVisual;

    private bool _isFollowing;
    private Vector3 _initialPosition;

    private void Awake()
    {
        startTrigger.Completed += StartFollow;
        endTrigger.Completed += EndFollow;
        _initialPosition = kingTransform.position;
    }

    private void LateUpdate()
    {
        if (_isFollowing)
        {
            kingTransform.position = player.position + playerOffset;
        }
    }

    private void EndFollow()
    {
        // Only do it once
        endTrigger.Completed -= EndFollow;
        StartCoroutine(HopOffPlayer());
    }

    private void StartFollow()
    {
        StartCoroutine(HopOnPlayer());
    }

    private IEnumerator HopOnPlayer()
    {
        for (float i = 0; i < travelToPlayerTime; i += Time.deltaTime)
        {
            float iNorm = i / travelToPlayerTime;
            kingTransform.position = Vector3.Lerp(_initialPosition, player.position + playerOffset, iNorm);
            yield return null;
        }
        _isFollowing = true;
    }

    private IEnumerator HopOffPlayer()
    {
        _isFollowing = false;
        Vector3 playersOriginalPosition = player.position + playerOffset;
        for (float i = 0; i < travelToPlayerTime; i += Time.deltaTime)
        {
            float iNorm = i / travelToPlayerTime;
            kingTransform.position = Vector3.Lerp(playersOriginalPosition, endPosition.position, iNorm);
            yield return null;
        }
        kingVisual.SetActive(false);
    }
}
