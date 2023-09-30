using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndPlate : MonoBehaviour
{
    public TMP_Text score;

    void OnEnable()
    {
        score.text = GameManager.Instance.score.ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Stage");
    }

    public void Reset()
    {
        GameManager.Instance.ResetAll();
        gameObject.SetActive(false);
    }
}
