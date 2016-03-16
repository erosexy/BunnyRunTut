using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

	private Rigidbody2D myRigidBody;
	private Animator myAnim;
	public float bunnyJumpForce = 500f;
	private float bunnyHurtTime = -1;
	private Collider2D myCollider;
	public Text scoreText;
    public Text eggsText;
	public Text livesText;
	private float startTime;
    private int eggsCollected;
	private int jumpsLeft = 2;
	public AudioSource jumpSfx;
	public AudioSource deathSfx;
	public AudioSource eeSfx;
    
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myCollider = GetComponent<Collider2D> ();

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Title");
        }

		if (bunnyHurtTime == -1) {

			//se a tecla "espaço" for pressionada, o sprite pula
			if (Input.GetButtonUp ("Jump") && jumpsLeft > 0) {

				if(myRigidBody.velocity.y < 0){
					myRigidBody.velocity = Vector2.zero;
				}

				if(jumpsLeft == 1){
					myRigidBody.AddForce (transform.up * bunnyJumpForce * 0.75f);
				}
				else{
					myRigidBody.AddForce (transform.up * bunnyJumpForce);
				}
				jumpSfx.Play();
				jumpsLeft --;
			}
			//myAnim.SetFloat ("vVelocity", myRigidBody.velocity.y);

			//usando o valor absoluto faz o sprite de pulo estar presente em todo o salto
			myAnim.SetFloat ("vVelocity", Mathf.Abs (myRigidBody.velocity.y));
			scoreText.text = (Time.time - startTime).ToString("0.0");
		} else {
			//apos um tempo, reinicia a cena
			if(Time.time > bunnyHurtTime + 2){
				//LoadLevel serve para carregar outras cenas do jogo
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){

		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {

			foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>()) {
				spawner.enabled = false;
			}

			foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			deathSfx.Play();
			bunnyHurtTime = Time.time;
			myAnim.SetBool ("bunnyHurt", true);
			livesText.text = (int.Parse (livesText.text) - 1).ToString ();
			myRigidBody.velocity = Vector2.zero;
			myRigidBody.AddForce (transform.up * bunnyJumpForce);
			myCollider.enabled = false;
            
        } else if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			jumpsLeft = 2;
		}

		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Itens")) {
            Destroy (collision.gameObject);
			eeSfx.Play ();
            eggsCollected++;
            eggsText.text = eggsCollected.ToString();
            //scoreText.text += (scoreText + 100).ToString("0.0");
        }
	}
}
