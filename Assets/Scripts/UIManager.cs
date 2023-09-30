using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton Instance

    public GameObject mainMenu;
    public GameObject endPlate;
    TMP_Text score;

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

        score = transform.Find("Score").GetComponent<TMP_Text>();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Stage");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetScore(int x)
    {
        score.text = x.ToString();
    }
}
