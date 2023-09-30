using UnityEngine;
using UnityEngine.InputSystem;

// Jump mechanic between nodes. 
// "Nodes" are the 8 platform transforms. Index 0 is the bottom left node, increasing clockwise to the bottom right, index 7.

public class JumpBetweenTransforms : MonoBehaviour
{
    public Transform[] nodes;
    public int currentNode;

    public float jumpHeight = 2f;
    public float jumpDuration = 0.5f;
    public AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public bool isJumping = false;

    private float jumpTimer = 0f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Quaternion startRotation;
    private Quaternion endRotation;
    Animator animator;
    float tempDuration;

    private void Awake()
    {
        if (nodes.Length < 1)
        {
            Debug.LogError("No nodes assigned!");
            return;
        }

        animator = GetComponent<Animator>();
        currentNode = 0;
        tempDuration = jumpDuration;
    }

    private void Update()
    {
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float t = Mathf.Clamp01(jumpTimer / jumpDuration);

            float jumpProgress = jumpCurve.Evaluate(t);
            Vector3 jumpPosition = Vector3.Lerp(startPosition, endPosition, jumpProgress);  // Positional Lerp
            jumpPosition.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;                         // Sinusoidal jump path
            transform.position = jumpPosition;

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, jumpProgress); // Rotational Lerp

            if (jumpTimer >= jumpDuration)
            {
                isJumping = false;
                jumpTimer = 0f;
                jumpDuration = tempDuration;
            }
        }
        else
        {
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                JumpToPreviousNode();
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                JumpToNextNode();
            }
            else if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                jumpDuration = 1.5f;
                JumpToNode(currentNode);
            }
        }
    }

    public void JumpToNextNode()
    {
        currentNode = (currentNode + 1) % nodes.Length; // Wrap around at final index
        JumpToNode(currentNode);
    }

    public void JumpToPreviousNode()
    {
        currentNode = (currentNode - 1 + nodes.Length) % nodes.Length; // Wrap around at first index
        JumpToNode(currentNode);
    }

    public void JumpToNode(int nodeIndex)
    {
        if (nodes.Length < 1)
        {
            Debug.LogError("No nodes assigned!");
            return;
        }

        startPosition = transform.position;
        startRotation = transform.rotation;

        float nodeHeight = nodes[nodeIndex].GetComponent<Collider>().bounds.size.y;
        Vector3 nodePosition = nodes[nodeIndex].position;
        // endPosition = new Vector3(nodePosition.x, nodePosition.y + nodeHeight / 2, nodePosition.z);
        endPosition = new Vector3(nodePosition.x, 0.563f, nodePosition.z);
        endRotation = nodes[nodeIndex].rotation;
        isJumping = true;
        GetComponent<Animator>().Play("JumpStart");
    }
}
