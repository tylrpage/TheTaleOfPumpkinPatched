using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController cc;
    [SerializeField] private Transform visual;
    
    private bool _facingRight;
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.DialogueManager.IsCurrentlyTalking)
        {
            return;
        }
        
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cc.Move(inputDirection * Time.deltaTime * speed);

        // if (inputDirection != Vector3.zero)
        // {
        //     _facingRight = inputDirection.x > 0;
        // }
        //
        // Vector3 currentScale = visual.localScale;
        // visual.localScale = new Vector3(_facingRight ? -1 : 1, currentScale.y, currentScale.z);
    }
}
