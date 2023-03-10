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
    //[SerializeField] Camera cam;
    //[SerializeField] float momentum;
    [SerializeField] Transform camAttach;
    [SerializeField] float baseGlide;
    bool grounded;
    //bool gliding = false;
    Rigidbody body;
    NewPlayerMovement movement;
    //float terminalVelocity = 60;
    [SerializeField] float boostMultiplier;
    float boost = 1;
    bool boosted;

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
            Glide();
            //gliding = true;
            //momentum = 2;
        }

        if (/*jumpInput < 1 || */grounded/* || momentum < 1*/)
        {
            boost = 1;
            //gliding = false;
            //momentum = 0;
        }
    }

    public void Glide()
    {
        float glideMultiplier = camAttach.rotation.x;
        //float boost = 1;
        Debug.Log(boost);
        body.velocity += Vector3.up * baseGlide * (-glideMultiplier) * boost;

        if (glideMultiplier >= 0)
        {
            if (body.velocity.y < -30 && !boosted)
            {
                body.velocity = new Vector3(0, 30, 0);
                boosted = true;
            }


            boost += Time.deltaTime * boostMultiplier;
        }
        else
        {
            
            boosted = false;
            boost -= Time.deltaTime * boostMultiplier;
        }
    }
}