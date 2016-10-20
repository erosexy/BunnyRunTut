using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    private GameObject pauseText, player, backButton;

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
            player = GameObject.Find("Bunnie");
            player.GetComponent("bunnieController");
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
    }

    public void unpauseGame()
    {
        Time.timeScale = 1.0f;
        print("Game unpaused");
        backButton.SetActive(false);
        isPaused = false;
    }
}
