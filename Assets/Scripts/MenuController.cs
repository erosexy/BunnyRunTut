using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public AudioSource titleMusic;
    public GameObject audioOnIcon;
    public GameObject audioOffIcon;

    // Use this for initialization
    void Start () {
        SetSoundState();

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

    public void StartMusicTest()
    {
        titleMusic.Stop();
        SceneManager.LoadScene("SoundTest");
    }

    public void ToggleSound()
    {
        //verifica se o arquivo chamado 'Muted' existe e qual o valor dele
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            //se o valor for 0, ou seja, se o som estiver mutado, passa a ser 1
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
        SetSoundState();
    }

    private void SetSoundState()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            //se o valor do arquivo for 0, ou seja, se estiver mutado, desmuta
            AudioListener.volume = 1;
            audioOnIcon.SetActive(true);
            audioOffIcon.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            audioOnIcon.SetActive(false);
            audioOffIcon.SetActive(true);
        }
    }

    public void StartGameAsBunny()
    {
        titleMusic.Stop();
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
