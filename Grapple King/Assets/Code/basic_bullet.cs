using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_bullet : MonoBehaviour 
{
    bool coliderHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        // Check if the bullet hits an object on layer 6.
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Bullet hit an object on layer 6.");
            Destroy(gameObject);
        }
    }
}
