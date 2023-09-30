using UnityEngine;
using System.Collections;
using StarterAssets;

public enum PickupType
{
    Yellow,
    Blue,
    Red
}

// Pickup uses the same script for yellow, blue, and red interactable objects
// with behavior OnTriggerEnter determined by the PickupType type. 

public class Pickup : MonoBehaviour
{
    public ParticleSystem particles;
    public PickupType type;
    public float rotationSpeed = 10f;

    // Slowly rotate on its y-axis
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            particles.Play();

            switch(type)
            {
                case PickupType.Yellow:
                    GameManager.Instance.AddScore(50);
                    break;
                case PickupType.Blue:
                    GameManager.Instance.AddScore(200);
                    break;
                case PickupType.Red:
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
            GameManager.Instance.SetPickup();
        }
    }
}
