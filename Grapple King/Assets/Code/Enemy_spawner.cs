using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spawner : MonoBehaviour
{
    private float timer;
    private float resetTime = 10f;
    public GameObject enemy;
    private Vector3 spawnPosition; // Change the type to Vector3

    private void Start()
    {
        // Create a new Vector3 for spawn position
        spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {
        // Increment the timer with the time elapsed since the last frame.
        timer += Time.deltaTime;

        // Check if the timer exceeds the reset time.
        if (timer >= resetTime)
        {
            // Reset the timer.
            timer = 0f;

            // Call a function or perform actions when the timer resets.
            OnTimerReset();
        }
    }

    // Example function to be called when the timer resets.
    void OnTimerReset()
    {
        // Instantiate the enemy at the specified spawn position.
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
