using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InputManagement;
using UnityEngine.Serialization;
using ReferenceVariables;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] BidirectionalFloat wasd;
    Rigidbody body;

    // additional movement
    [SerializeField] FloatVariable space;
    [SerializeField] float moveSpeed, groundDrag, jumpForce, jumpCooldown;
    bool readyToJump = true;
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;
    [SerializeField] Transform orientation;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        body.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MovePlayer();
        SpeedControl();

        body.drag = groundDrag;

        float jumpInput = space.Value;
        if (jumpInput > 0 && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void MovePlayer()
    {
        float xInput = wasd.Right.Value - wasd.Left.Value;
        float yInput = wasd.Up.Value - wasd.Down.Value;
        Vector3 moveDirection = orientation.forward * yInput + orientation.right * xInput;
        body.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(body.velocity.x, 0f, body.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            body.velocity = new Vector3(limitedVel.x, body.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
