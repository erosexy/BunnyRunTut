using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseGame : MonoBehaviour {

    private GameObject pauseText, player, backButton;

    public GameObject unPausedIcon;
    public GameObject pausedIcon;

    bool isPaused = false;

    public void Start()
    {
        pauseText = GameObject.Find("pauseTxt");

        pauseText.SetActive(false);
        player = GameObject.Find("Bunny");
        backButton = GameObject.Find("BackBtn");
        if (backButton != null)
            backButton.SetActive(false);
        if(player == null)
        {
            player = GameObject.Find("BunnyNatal");
            if (player == null)
            {
                player = GameObject.Find("Bunnie");
                if(player == null)
                {
                    player = GameObject.Find("BunnieNatal");
                    player.GetComponent("bunnieNatalController");
                }
                player.GetComponent("bunnieController");
            }
            player.GetComponent("bunnyNatalController");
        }
        else
        {
            player.GetComponent("bunnyController");
        }
        
    }

    public void verifyStatus()
    {
        if (isPaused)
        {
            if (player != null)
            {
                player.SetActive(true);
                print("script ativado");
            }
            pauseText.SetActive(false);
            unpauseGame();
        }
        else
        {
            if (player != null)
            {
                player.SetActive(false);
                print("script desativado");
            }

            pauseText.SetActive(true);
            pauseGame();
        }
    }

        
    public void pauseGame()
    {
        Time.timeScale = 0.0f;
        print("Game paused");
        backButton.SetActive(true);
        isPaused = true;
        unPausedIcon.SetActive(false);
        pausedIcon.SetActive(true);
}

public void unpauseGame()
    {
        Time.timeScale = 1.0f;
        print("Game unpaused");
        backButton.SetActive(false);
        isPaused = false;
        unPausedIcon.SetActive(true);
        pausedIcon.SetActive(false);
    }
}
