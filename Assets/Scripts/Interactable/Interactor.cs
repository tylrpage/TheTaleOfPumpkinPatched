using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private const int MaxColliders = 10;
    private static readonly int WhiteEnabled = Shader.PropertyToID("_WhiteEnabled");
    
    [SerializeField] private float interactRange;

    private Collider[] _results = new Collider[MaxColliders];
    private Collider _recentInteractableCollider;
    private static readonly int WhitePhaseOffset = Shader.PropertyToID("_WhitePhaseOffset");

    // Update is called once per frame
    void Update()
    {
        // Update nearest interactable
        int interactableLayerMask = (1 << LayerMask.NameToLayer("Interactable"));
        Collider nearestCollider = FindNearestCollider(interactableLayerMask);
        IInteractable nearestInteractable = nearestCollider != null ? nearestCollider.GetComponent<IInteractable>() : null;

        // old interactable is no longer interactable, disable flashing
        if (_recentInteractableCollider != null && _recentInteractableCollider != nearestCollider)
        {
            _recentInteractableCollider.GetComponent<Renderer>().material.SetFloat(WhiteEnabled, 0);
        }
        
        if (nearestInteractable != null)
        {
            // Nearest interactable has changed
            if (_recentInteractableCollider != nearestCollider)
            {
                // Enable flashing on the new object
                Material material = nearestCollider.GetComponent<Renderer>().material;
                material.SetFloat(WhiteEnabled, 1);
                material.SetFloat(WhitePhaseOffset, Time.timeSinceLevelLoad);
            }
            
            // Interact if interact key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                nearestInteractable.Interact();
            }
        }
        
        _recentInteractableCollider = nearestCollider;
    }

    private Collider FindNearestCollider(LayerMask layerMask)
    {
        var size = Physics.OverlapSphereNonAlloc(transform.position, interactRange, _results, layerMask);

        // Find nearest collider
        Collider nearestCollider = null;
        float nearestDistance = float.MaxValue;
        for (int i = 0; i < size; i++)
        {
            Collider currentCollider = _results[i];
            float relativeDistance = (currentCollider.transform.position - transform.position).sqrMagnitude;
            if (relativeDistance < nearestDistance)
            {
                nearestDistance = relativeDistance;
                nearestCollider = currentCollider;
            }
        }

        return nearestCollider;
    }
}
