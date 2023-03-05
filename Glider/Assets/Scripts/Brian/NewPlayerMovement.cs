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
    [SerializeField] BidirectionalFloat wasd;
    Rigidbody body;

    // additional movement
    [SerializeField] FloatVariable space;
    [SerializeField] float moveSpeed, jumpForce, jumpCooldown;
    bool readyToJump = true;
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;
    [SerializeField] Transform orientation;
    //bool isJumping;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        body.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
        
        //MovePlayer();
        float xInput = wasd.Right.Value - wasd.Left.Value;
        float yInput = wasd.Up.Value - wasd.Down.Value;
        //body.velocity = xInput * transform.right * moveSpeed + yInput * transform.forward * moveSpeed;
        body.velocity = new Vector3(xInput * moveSpeed, body.velocity.y, yInput * moveSpeed);

        //SpeedControl();

        //body.drag = groundDrag;

        float jumpInput = space.Value;
        if (jumpInput > 0 && readyToJump && grounded)
        {
            readyToJump = false;
            //isJumping = true;
            body.velocity += Vector3.up * jumpForce;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        //if (isJumping)
        //{
        //    //body.velocity = xInput * transform.right * moveSpeed + yInput * transform.forward * moveSpeed + transform.up * jumpForce;
        //    //body.velocity = new Vector3(body.velocity.x, body.velocity.y + jumpForce, body.velocity.z);
        //    body.velocity += Vector3.up * jumpForce;
        //    Invoke(nameof(ResetJump), jumpCooldown);
        //}
        //if (jumpInput > 0 && readyToJump && grounded)
        //{
        //    readyToJump = false;
        //    Jump();
        //    Invoke(nameof(ResetJump), jumpCooldown);
        //}
    }

    void MovePlayer()
    {
        float xInput = wasd.Right.Value - wasd.Left.Value;
        float yInput = wasd.Up.Value - wasd.Down.Value;
        //Vector3 moveDirection = orientation.forward * yInput + orientation.right * xInput;
        //body.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        body.velocity = xInput * transform.right * moveSpeed + yInput * transform.forward * moveSpeed + Vector3.up * 0;
    }

    //void SpeedControl()
    //{
    //    Vector3 flatVel = new Vector3(body.velocity.x, 0f, body.velocity.z);

    //    //if (flatVel.magnitude > moveSpeed)
    //    //{
    //    //    Vector3 limitedVel = flatVel.normalized * moveSpeed;
    //    //    body.velocity = new Vector3(limitedVel.x, body.velocity.y, limitedVel.z);
    //    //}
    //}

    void Jump()
    {
        //body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //transform.up vs Vector3.up: transform is relative to GameObject's transform while Vector3 is world space
    }

    void ResetJump()
    {
        readyToJump = true;
        //isJumping = false;
    }
}
