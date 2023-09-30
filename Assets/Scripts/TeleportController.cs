using UnityEngine;
using UnityEngine.InputSystem;

// Teleport between nodes, spawning particles in the player's wake.
// Replaced with JumpBetweenTransforms. 

public class TeleportController : MonoBehaviour
{
    public Transform[] nodes;
    public int currentNode;
    ParticleSystem particles;

    private void Start()
    {
        if (nodes.Length < 1)
        {
            Debug.LogError("No nodes assigned!");
            return;
        }
        particles = transform.Find("ParticleSystem").GetComponent<ParticleSystem>();

        currentNode = 0;
        TeleportToNode(currentNode);
    }

    private void Update()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            MoveToPreviousNode();
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            MoveToNextNode();
        }
    }

    private void MoveToNextNode()
    {
        currentNode = (currentNode + 1) % nodes.Length; // Wrap around at final index
        TeleportToNode(currentNode);
    }

    private void MoveToPreviousNode()
    {
        currentNode = (currentNode - 1 + nodes.Length) % nodes.Length;
        TeleportToNode(currentNode);
    }

    private void TeleportToNode(int nodeIndex)
    {
        particles.Play();
        if (nodes.Length < 1)
        {
            Debug.LogError("No nodes assigned!");
            return;
        }

        float nodeHeight = nodes[nodeIndex].GetComponent<Collider>().bounds.size.y;
        Vector3 nodePosition = nodes[nodeIndex].position;
        transform.position = new Vector3(nodePosition.x, nodePosition.y + nodeHeight / 2, nodePosition.z);
        transform.rotation = nodes[nodeIndex].rotation;
    }
}
