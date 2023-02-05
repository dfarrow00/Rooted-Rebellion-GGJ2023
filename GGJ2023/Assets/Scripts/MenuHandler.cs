using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject credits;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Credits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        howToPlay.SetActive(false);
        credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
