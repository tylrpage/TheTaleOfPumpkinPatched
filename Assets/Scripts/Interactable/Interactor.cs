using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private const int MaxColliders = 10;
    private static readonly int WhiteEnabled = Shader.PropertyToID("_WhiteEnabled");
    
    [SerializeField] private float interactRange;
    [SerializeField] private Tractor tractor;

    private Collider[] _results = new Collider[MaxColliders];
    private Interactable _recentInteractable;
    private static readonly int WhitePhaseOffset = Shader.PropertyToID("_WhitePhaseOffset");

    // Update is called once per frame
    void Update()
    {
        // Update nearest interactable
        int interactableLayerMask = (1 << LayerMask.NameToLayer("Interactable"));
        Interactable nearestInteractable = FindNearestInteractable(interactableLayerMask);

        // old interactable is no longer interactable, disable flashing
        if (_recentInteractable != null && _recentInteractable != nearestInteractable)
        {
            _recentInteractable.GetComponentInChildren<Renderer>().material.SetFloat(WhiteEnabled, 0);
        }
        
        if (nearestInteractable != null)
        {
            // Nearest interactable has changed
            if (_recentInteractable != nearestInteractable)
            {
                // Enable flashing on the new object
                Material material = nearestInteractable.GetComponentInChildren<Renderer>().material;
                material.SetFloat(WhiteEnabled, 1);
                material.SetFloat(WhitePhaseOffset, Time.timeSinceLevelLoad);
            }
            
            // Interact if interact key is pressed
            if (!GameManager.Instance.DialogueManager.IsCurrentlyTalking && !tractor.FixingTractor && Input.GetButtonDown("Interact"))
            {
                nearestInteractable.Interact();
            }
        }
        
        _recentInteractable = nearestInteractable;
    }

    private Interactable FindNearestInteractable(LayerMask layerMask)
    {
        var size = Physics.OverlapSphereNonAlloc(transform.position, interactRange, _results, layerMask);

        // Sort by distance
        Array.Sort(_results, Comparison);
        for (int i = 0; i < size; i++)
        {
            Collider currentCollider = _results[i];
            if (currentCollider == null)
            {
                continue;
            }
            
            Interactable[] interactables = currentCollider.GetComponents<Interactable>();
            Array.Sort(interactables, (x, y) => x.Priority - y.Priority);
            foreach (var interactable in interactables)
            {
                if (interactable.CanInteract())
                {
                    return interactable;
                }
            }
        }

        return null;
    }

    private int Comparison(Collider x, Collider y)
    {
        if (x == null && y == null)
        {
            return 0;
        }
        else if (x == null)
        {
            return 1;
        }
        else if (y == null)
        {
            return -1;
        }
        
        Vector3 position = transform.position;
        
        float xRelativeDistance = (x.transform.position - position).sqrMagnitude;
        float yRelativeDistance = (y.transform.position - position).sqrMagnitude;

        if (xRelativeDistance < yRelativeDistance)
        {
            return -1;
        }
        else if (xRelativeDistance > yRelativeDistance)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
