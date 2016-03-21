using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine("Wait");
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            //Application.LoadLevel("Title");
            SceneManager.LoadScene("Title");
            Destroy(this.gameObject);
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(6);

		//Application.LoadLevel("Title");
        SceneManager.LoadScene("Title");
		Destroy(this.gameObject);
	}
}
