using UnityEngine;
using ReferenceVariables;

public class EthanGlide : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] EthanNewPlayerMovement movement;
    [SerializeField] Camera cam;
    [SerializeField] FloatVariable space;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float playerHeight = 1.01f;
    [SerializeField] float gainAmount;
    [SerializeField] float loseAmount;
    [SerializeField] float angle;
    [SerializeField] float terminalVelocity = 60;
    float momentum;
    bool grounded, gliding = false;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float jumpInput = space.Value;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);

        if (movement.GetReadyToJump() && jumpInput > 0 && !grounded)
        {
            gliding = true;
            momentum = 2;
        }
        if (grounded)
        {
            gliding = false;
            momentum = 0;
        }

        if (gliding)
        {
            Vector3 rotation = cam.transform.rotation.eulerAngles;
            if (rotation.x < angle)
            {
                float change = Mathf.Lerp(0, gainAmount, rotation.x / 90);
                momentum += change;
            }
            else if (rotation.x > angle)
            {
                rotation.x = 360 - rotation.x;
                float change = Mathf.Lerp(0, loseAmount, Mathf.Abs(rotation.x / 90));
                momentum -= change;
            }
            momentum = Mathf.Clamp(momentum, 0, terminalVelocity);
            bool glide = false;
            if (body.velocity.magnitude < 1)
            {
                return;
            }
            if (momentum > 1)
            {
                glide = true;
            }
            if (glide)
            {
                body.velocity = momentum * cam.transform.forward;
            }
        }
    }
}
