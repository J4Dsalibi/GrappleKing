using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_mvt : MonoBehaviour
{
    // Basic Mvt
    [SerializeField] private Transform groundcheckTransform;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    // Grapple Gun
    private bool spaceWasPressed = false;
    public Transform targetObject;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        // Adjust the player's velocity based on horizontal input.
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 10, rigidbodyComponent.velocity.y, 0);

        if (spaceWasPressed)
        {
            Debug.Log("Space bar pressed");
            Vector3 direction = targetObject.position - transform.position;
            Debug.Log("Direction: " + direction);
            direction.z = 0;
            direction.Normalize();
            Debug.Log("Normalized Direction: " + direction);
            rigidbodyComponent.AddForce(direction * 20, ForceMode.Impulse);
            spaceWasPressed = false;
        }

        if (Physics.OverlapSphere(groundcheckTransform.position, 0.1f).Length == 2)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 12, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }
}