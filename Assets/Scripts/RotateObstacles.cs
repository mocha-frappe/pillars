using UnityEngine;
using System.Collections;
using StarterAssets;

// Rotate the obstacles around a pivot axis (y-axis, central pillar). Place an empty parent GameObject at the 
// axis of rotation, attach this to it, and set the object you want to rotate as a child. 

public class RotateObstacles : MonoBehaviour
{
    public float rotationSpeed = 15f;
    float timer = 10f;
    float tempSpeed;

    void Awake()
    {
        tempSpeed = rotationSpeed;
    }

    // Rotate around central pillar.
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);   
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            timer = 10f;
            rotationSpeed += 8f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("GameOver");
            GameManager.Instance.OnDeath();
        }
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        rotationSpeed = tempSpeed;
    }
}
