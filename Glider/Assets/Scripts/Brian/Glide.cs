using ReferenceVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{
    [Header("Lucas Input")]
    [SerializeField] FloatVariable space;
    [SerializeField] FloatVariable shift;

    [Header("Gliding")]
    [SerializeField] float glideSpeed = 1;
    [SerializeField] float diveSpeed = 10;
    Rigidbody body;
    NewPlayerMovement movement;
    bool isDiving;
    float boost = 1;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        movement = GetComponent<NewPlayerMovement>();
    }

    private void FixedUpdate()
    {
        float jumpInput = space.Value;
        float glideInput = shift.Value;

        if (jumpInput > 0 && !movement.GetGrounded())
        {
            if (glideInput > 0)
            {
                isDiving = true;;
            }
            else
            {
                isDiving = false;
            }

            if (isDiving)
            {
                body.velocity = Vector3.down * diveSpeed;
                boost -= Time.deltaTime * 10;
            }
            else
            {
                body.velocity = Vector3.down * glideSpeed * boost;

                if (boost < 1)
                {
                    boost += Time.deltaTime * 10;
                }
                else
                {
                    boost = 1;
                }
            }
        }

        if (movement.GetGrounded())
        {
            isDiving = false;
            boost = 1;
        }
    }
}
