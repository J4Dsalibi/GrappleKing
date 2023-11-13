using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_bullet : MonoBehaviour
{
    private bool coliderHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        // Check if the bullet hits an object on layer 6.
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Bullet hit an object on layer 6.");
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 11)
        {
            // Destroy the object on layer 11.
            Destroy(collision.gameObject);

            // Optionally, you can also destroy the bullet itself.
            Destroy(gameObject);
        }
    }
}







