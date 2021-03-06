﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackScreen : MonoBehaviour {

    private GameObject pauseBtn, bgMusic, backBtn, lifes1, lifes2, lifes3, pauseText, gameOverMusic;
    public GameObject player;

    public void Back()
    {
        pauseBtn = GameObject.Find("PauseBtn");
        bgMusic = GameObject.Find("BackgroundMusic");
        backBtn = GameObject.Find("BackBtn");
        lifes1 = GameObject.Find("lifes1");
        lifes2 = GameObject.Find("lifes2");
        lifes3 = GameObject.Find("lifes3");
        pauseText = GameObject.Find("pauseTxt");

        bgMusic.GetComponent<AudioSource>().Stop();
        pauseBtn.SetActive(false);
        backBtn.SetActive(false);
        HideScore();
        if (lifes1 == null && lifes2 == null && lifes3 == null)
        {
            Time.timeScale = 1.0f;
            if (pauseText != null)
            {
                pauseText.SetActive(false);
            }
            //pauseText.SetActive(false);
            SceneManager.LoadScene("Title");
        }
        else {
            Time.timeScale = 1.0f;
            if(pauseText != null)
            {
                pauseText.SetActive(false);
            }
            lifes3.GetComponent<Renderer>().enabled = false;
            lifes2.GetComponent<Renderer>().enabled = false;
            lifes1.GetComponent<Renderer>().enabled = false;
            SceneManager.LoadScene("Title");
        }
        
    }

    public void SoundTestBack()
    {
        gameOverMusic = GameObject.Find("GameOverMusic");
        gameOverMusic.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Title");
    }

    void HideScore()
    {
        if(player.GetComponent<BunnyController>())
        {
            player.GetComponent<BunnyController>().HideScore();
        }
        else if (player.GetComponent<BunnyNatalController>())
        {
            player.GetComponent<BunnyNatalController>().HideScore();
        }
        else if (player.GetComponent<BunnieNatalController>())
        {
            player.GetComponent<BunnieNatalController>().HideScore();
        }
        else
        {
            player.GetComponent<BunnieController>().HideScore();
        }
    }
}
