using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple_collision : MonoBehaviour
{
    private bool coliderHit = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits an object on layer 6.
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Bullet hit an object on layer 6.");
            
            Rigidbody rb = GetComponent<Rigidbody>();
            
            // Freeze the position in all axes.
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // Get the Collider component
            Collider myCollider = GetComponent<Collider>();

            // Disable the collider
            myCollider.enabled = false;
        }
    }
}