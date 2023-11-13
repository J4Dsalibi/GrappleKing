using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;  // Assign the player's transform in the inspector
    public float moveSpeed = 5f;

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the enemy to the player
            float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);

            // Move the enemy towards the player along the X-axis
            transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

            // Set the z-component to zero to ensure it remains on the XZ plane
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
        }
    }
}