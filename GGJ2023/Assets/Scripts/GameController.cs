using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject loseMenu;

    private bool gameOver;

    public void PlayerWin()
    {
        if (!gameOver)
        {
            gameOver = true;
            winMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        
    }

    public void PlayerLose()
    {
        if (!gameOver)
        {
            gameOver = true;
            loseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
