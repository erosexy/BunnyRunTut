using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    private GameObject pauseText;

    bool isPaused = false;

    public void Start()
    {
        pauseText = GameObject.Find("pauseTxt");
        pauseText.SetActive(false);
    }

    public void verifyStatus()
    {
        if (isPaused)
        {
            pauseText.SetActive(false);
            unpauseGame();
        }
        else
        {
            pauseText.SetActive(true);
            pauseGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        print("Game paused");
        isPaused = true;
    }

    public void unpauseGame()
    {
        Time.timeScale = 1.0f;
        print("Game unpaused");
        isPaused = false;
    }
}
