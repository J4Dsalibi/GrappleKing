using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_pistol : MonoBehaviour
{
    public float zOffset = -1.4f;
    private bool isPickedUp = false;
    Transform playerTransform;
    public Transform targetObject; // Assign the target object in the Inspector.

    // Balle
    public GameObject bulletPrefab;
    public Transform gunMuzzle; // The point where bullets will be spawned.
    public float bulletSpeed = 14.0f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isPickedUp)
        {
            if (playerTransform != null && targetObject != null)
            {
                // Set the gun's position relative to the player's position, accounting for the height offset.
                Vector3 newPosition = playerTransform.position + new Vector3(0f, 0f, zOffset);
                transform.position = newPosition;

                // Calculate the direction vector from the gun to the target object.
                Vector3 direction = targetObject.position - transform.position;

                // Calculate the Z rotation to look at the target object.
                float newZRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Set the Z rotation while preserving the existing Y and X rotations.
                Vector3 currentRotation = transform.rotation.eulerAngles;

                transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZRotation);

                if (Input.GetMouseButtonDown(0)) // Assumes left mouse button for shooting
                {
                    Shoot();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isPickedUp = true;
            // Disable the gun's collider and renderer, or any other necessary actions.
            GetComponent<Collider>().enabled = false;
            transform.parent = playerTransform;
        }
    }

    void Shoot()
    {
        Vector3 direction = targetObject.position - gunMuzzle.position;

        // Keep the Z-component of the direction vector at 0.
        direction.z = 0;

        // Normalize the direction vector to make it a unit vector.
        direction.Normalize();

        // Create a bullet at the gun's muzzle.
        GameObject bullet = Instantiate(bulletPrefab, gunMuzzle.position, Quaternion.identity);

        // Get the bullet's Rigidbody.
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Apply a force to the bullet in the calculated direction.
        bulletRigidbody.velocity = direction * bulletSpeed;
    }
}


