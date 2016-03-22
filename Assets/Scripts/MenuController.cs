using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public AudioSource titleMusic;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        //se Esc ou botão "back" do Android for pressionado
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //sai do jogo
            Application.Quit();
        }
	
	}

    public void StartGameAsBunny()
    {
        titleMusic.Pause();
        SceneManager.LoadScene("GameBunny");
        //Application.LoadLevel("GameBunny");
    }

    public void StartGameAsBunnie()
    {
        titleMusic.Stop();
        SceneManager.LoadScene("GameBunnie");
        //Application.LoadLevel("GameBunnie");
    }
}
