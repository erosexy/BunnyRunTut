using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

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

    public void StartGame()
    {
        Application.LoadLevel("GameBunny");
    }
    
}
