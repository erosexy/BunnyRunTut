using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {

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

	IEnumerator StartAgain()
	{
		yield return new WaitForSeconds(30);

		//Application.LoadLevel("Title");
        SceneManager.LoadScene("Title");
		Destroy(this.gameObject);
	}
}
