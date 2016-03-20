using UnityEngine;
using System.Collections;

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
        Application.LoadLevel("GameBunny");
    }

    public void StartGameAsBunnie()
    {
        titleMusic.Stop();
        Application.LoadLevel("GameBunnie");
    }

}
