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
        }
        
    }

    public void PlayerLose()
    {
        if (!gameOver)
        {
            gameOver = true;
            loseMenu.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
