using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Instance

    public int score;
    public GameObject yellow;
    public GameObject blue;
    public RotateObstacles[] obstacles;

    public JumpBetweenTransforms player;
    private Vector3 initPos;
    private Quaternion initRot;
    private GameObject pickup;

    void Awake()
    {
        // Set Instance to this, Destroy if another instance is in the scene.

        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else
        { 
            Instance = this;
        }

        if (player == null)
            player = GameObject.FindWithTag("Player").transform.GetComponent<JumpBetweenTransforms>();
        initPos = player.transform.position;
        initRot = player.transform.rotation;
        player.gameObject.SetActive(false);
    }

    void Update()
    {
        UIManager.Instance.SetScore(score);
    }

    public void StartGame()
    {
        player.gameObject.SetActive(true);
        foreach (RotateObstacles obstacle in obstacles)
        {
            obstacle.gameObject.SetActive(true);
        }
        SetPickup();
        UIManager.Instance.mainMenu.SetActive(false);
    }

    public void AddScore(int x)
    {
        score += x;
    }

    // Place a pickup on a random node, excluding the current one. 
    public void SetPickup()
    {
        int randomNode = Random.Range(0, player.nodes.Length);
        if (randomNode == player.currentNode)
        {
            // Debug.Log("randomNode = currentNode!");
            SetPickup();
            return;
        }
        // Debug.Log(randomNode);

        Vector3 nodePosition = player.nodes[randomNode].position;
        if (score % 150 == 0 && score != 0)
            pickup = Instantiate(blue, new Vector3(nodePosition.x, 0.5f, nodePosition.z), Quaternion.identity);
        else
            pickup = Instantiate(yellow, new Vector3(nodePosition.x, 0.5f, nodePosition.z), Quaternion.identity);
    }

    public void ResetAll()
    {
        score = 0;
        player.gameObject.SetActive(true);
        player.currentNode = 0;
        player.transform.position = initPos;
        player.transform.rotation = initRot;
        foreach (RotateObstacles obstacle in obstacles)
        {
            obstacle.gameObject.SetActive(true);
            obstacle.Reset();
        }
        SetPickup();
    }

    public void OnDeath()
    {
        player.isJumping = false;
        player.transform.Find("ParticleSystem").GetComponent<ParticleSystem>().Play();
        player.gameObject.SetActive(false);
        foreach (RotateObstacles obstacle in obstacles)
        {
            obstacle.gameObject.SetActive(false);
        }
        UIManager.Instance.endPlate.SetActive(true);
        Destroy(pickup);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
