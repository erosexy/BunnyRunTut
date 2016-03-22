using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {

    public AudioSource gameOverMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine("StartAgain");
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            //Application.LoadLevel("Title");
            SceneManager.LoadScene("Title");
            Destroy(this.gameObject);
		}
	}

    public void GoToTitleScreen()
    {
        gameOverMusic.Stop();
        SceneManager.LoadScene("Title");
    }

	IEnumerator StartAgain()
	{
		yield return new WaitForSeconds(30);

		//Application.LoadLevel("Title");
        SceneManager.LoadScene("Title");
		Destroy(this.gameObject);
	}
}
