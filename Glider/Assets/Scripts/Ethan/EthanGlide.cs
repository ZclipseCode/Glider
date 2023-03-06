using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InputManagement;
using UnityEngine.Serialization;
using ReferenceVariables;
using UnityEngine.EventSystems;

public class EthanGlide : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] EthanNewPlayerMovement movement;
    [SerializeField] Camera cam;
    [SerializeField] FloatVariable space;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float playerHeight = 1.01f, momentum;
    [SerializeField] bool grounded, gliding = false;
    float terminalVelocity = 60;
    private void Start() {
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        float jumpInput = space.Value;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);

        if(movement.GetReadyToJump() && jumpInput > 0 && !grounded) {
            gliding = true;
            momentum = 2;
        }
        if (grounded || momentum < 1) {
            gliding = false;
            momentum = 0;
        }

        if (gliding) {
            if (cam.transform.rotation.x > 0) {
                    momentum = Mathf.Lerp(momentum, terminalVelocity, cam.transform.rotation.x / 10);
            }
            else if (cam.transform.rotation.x < 0) { 
                    momentum = Mathf.Lerp(momentum, 0, Mathf.Abs(cam.transform.rotation.x / 10));
            }
            body.velocity = cam.transform.right * 0 + momentum * cam.transform.forward + cam.transform.up * 0;
        }
    }
}
