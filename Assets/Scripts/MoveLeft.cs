using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour {

	public float speed= 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transforma a posiçao do cacto para que ele se mova para esquerda
		transform.position += Vector3.left * speed *(Time.deltaTime);
	}
}
