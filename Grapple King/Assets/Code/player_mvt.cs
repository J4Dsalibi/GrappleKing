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
    private int speed = 20;
    

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

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        // Adjust the player's velocity based on horizontal input.
        rigidbodyComponent.velocity = new Vector3(horizontalInput * speed, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundcheckTransform.position, 0.1f).Length == 2)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 17, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }
}