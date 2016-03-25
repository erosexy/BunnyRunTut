using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundTestController : MonoBehaviour {
    public AudioSource titleMusic;
    public AudioSource gameMusic;
    public AudioSource gameOverMusic;

    public void TitleMusic()
    {
        titleMusic.Play();
        gameMusic.Stop();
        gameOverMusic.Stop();
    }

    public void GameMusic()
    {
        titleMusic.Stop();
        gameMusic.Play();
        gameOverMusic.Stop();
    }

    public void GameOverMusic()
    {
        titleMusic.Stop();
        gameMusic.Stop();
        gameOverMusic.Play();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            titleMusic.Stop();
            gameMusic.Stop();
            gameOverMusic.Stop();
            SceneManager.LoadScene("Title");
        }
    }
}
