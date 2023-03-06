using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InputManagement;
using UnityEngine.Serialization;
using ReferenceVariables;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class BrianGlide : MonoBehaviour
{
    [Header("Lucas Input")]
    [SerializeField] FloatVariable space;

    [Header("Gliding")]
    [SerializeField] Camera cam;
    [SerializeField] float momentum;
    [SerializeField] Transform camAttach;
    bool grounded;
    bool gliding = false;
    Rigidbody body;
    NewPlayerMovement movement;
    float terminalVelocity = 60;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        movement = GetComponent<NewPlayerMovement>();
    }
    private void FixedUpdate()
    {
        float jumpInput = space.Value;
        grounded = movement.GetGrounded();

        if (jumpInput > 0 && !grounded)
        {
            gliding = true;
            momentum = 2;
        }
        if (jumpInput < 1 || grounded || momentum < 1)
        {
            gliding = false;
            momentum = 0;
        }

        if (gliding)
        {
            if (camAttach.rotation.x > 0)
            {
                //momentum = Mathf.Lerp(momentum, terminalVelocity, cam.transform.rotation.x / 10);
                momentum = Mathf.Lerp(momentum, terminalVelocity, camAttach.rotation.x / 10);
            }
            else if (cam.transform.rotation.x < 0)
            {
                //momentum = Mathf.Lerp(momentum, 0, Mathf.Abs(cam.transform.rotation.x / 10));
                momentum = Mathf.Lerp(momentum, 0, Mathf.Abs(camAttach.rotation.x / 10));
            }

            body.velocity = camAttach.right * 0 + momentum * camAttach.forward + camAttach.up * 0;
        }

        //Debug.Log(camAttach.rotation.x / 10);
    }
}