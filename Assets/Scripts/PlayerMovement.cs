using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController cc;
    
    private bool _facingRight;
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.DialogueManager.IsCurrentlyTalking)
        {
            return;
        }
        
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Cap magnitude to 1
        if (inputDirection.magnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }
        cc.Move(inputDirection * Time.deltaTime * speed);
    }
}
