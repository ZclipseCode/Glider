using System;
using InputManagement;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class TestMove : MonoBehaviour
{
    [SerializeField] private BidirectionalFloat wasd;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float xInput = wasd.Right.Value - wasd.Left.Value;
        float yInput = wasd.Up.Value - wasd.Down.Value;

        body.velocity = xInput * transform.right + yInput * transform.forward;
    }

}
