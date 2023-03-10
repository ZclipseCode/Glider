using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InputManagement;
using UnityEngine.Serialization;
using ReferenceVariables;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class NewPlayerMovement : MonoBehaviour
{
    [Header("Lucas Inputs")]
    [SerializeField] BidirectionalFloat wasd;
    [SerializeField] FloatVariable space;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float jumpCooldown = 1;
    [SerializeField] float playerHeight = 1.01f;
    [SerializeField] LayerMask whatIsGround;
    Rigidbody body;
    bool readyToJump = true;
    bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
        
        float xInput = wasd.Right.Value - wasd.Left.Value;
        float yInput = wasd.Up.Value - wasd.Down.Value;
        body.velocity = xInput * transform.right * moveSpeed + yInput * transform.forward * moveSpeed + transform.up * body.velocity.y;

        float jumpInput = space.Value;
        if (jumpInput > 0 && readyToJump && grounded)
        {
            readyToJump = false;
            body.velocity += Vector3.up * jumpForce;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public bool GetReadyToJump() {
        return readyToJump;
    }
}
