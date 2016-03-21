using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //necessário para usar SceneManager, por exemplo

public class BunnyController : MonoBehaviour {

	private Rigidbody2D myRigidBody;
	private Animator myAnim;
	public float bunnyJumpForce = 500f;
	private float bunnyHurtTime = -1;
	private Collider2D myCollider;
	public Text scoreText;
    public Text eggsText;
	private float startTime;
    private int eggsCollected;
	private int jumpsLeft = 2;
	public AudioSource jumpSfx;
	public AudioSource deathSfx;
	public AudioSource eeSfx;

    //variáveis que contém objetos
    private GameObject lifes3;
    private GameObject lifes2;
    private GameObject lifes1;
    private GameObject bgm;

    // Use this for initialization
    void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myCollider = GetComponent<Collider2D> ();

		startTime = Time.time;

        //encontra o objeto que tem a música de fundo
        bgm = GameObject.Find("BackgroundMusic");
        //se a música estiver parada/pausada
        if (!bgm.GetComponent<AudioSource>().isPlaying)
        {
            //a música toca de novo
            bgm.GetComponent<AudioSource>().Play();
        }

        //encontra os objetos
        lifes3 = GameObject.Find("lifes3");
        lifes2 = GameObject.Find("lifes2");
        lifes1 = GameObject.Find("lifes1");
        if(!lifes3.GetComponent<Renderer>().enabled && !lifes2.GetComponent<Renderer>().enabled && !lifes1.GetComponent<Renderer>().enabled)
        {
            lifes1.GetComponent<Renderer>().enabled = true;
            lifes2.GetComponent<Renderer>().enabled = true;
            lifes3.GetComponent<Renderer>().enabled = true;
        }

    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            bgm.GetComponent<AudioSource>().Stop();
            lifes3.transform.localScale = new Vector3(1, 1, 1);
            lifes2.transform.localScale = new Vector3(1, 1, 1);
            lifes1.transform.localScale = new Vector3(1, 1, 1);
            SceneManager.LoadScene("Title");
            //Application.LoadLevel("Title"); não recomendado para Unity 5.x em diante
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
				//Application.LoadLevel (Application.loadedLevel);
                SceneManager.LoadScene("GameBunny");
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
            
            //se o objeto tiver sido achado
            if (lifes3.GetComponent<Renderer>().enabled)
            {
                lifes3.GetComponent<Renderer>().enabled = false;
                //lifes3.transform.localScale = new Vector3(0, 0, 0);
                Debug.Log("Lifes3 desativado");
            }
            else if (lifes2.GetComponent<Renderer>().enabled)
            {
                lifes2.GetComponent<Renderer>().enabled = false;
                //lifes2.transform.localScale = new Vector3(0, 0, 0);
                Debug.Log("Lifes2 desativado");
            }
            else if (lifes1.GetComponent<Renderer>().enabled)
            {
                lifes1.GetComponent<Renderer>().enabled = false;
                //lifes1.transform.localScale = new Vector3(0, 0, 0);
                //lifes1.SetActive(false); //desativa o objeto, recomendado a partir do Unity 5.x
                //Application.LoadLevel("GameOver");
                bgm.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("GameOver");
            }

            deathSfx.Play();
			bunnyHurtTime = Time.time;
			myAnim.SetBool ("bunnyHurt", true);
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

    ///<summary>
    ///método chamado sempre que a tela é carregada
    ///<param name="index">é o número da tela que foi carregada</param>
    ///Você pode alterar os valores indo em File > Build Settings
    ///Lá você configura a ordem que as telas do seu jogo serão exibidas (de 0 a n)
    ///Número é o index
    /// </summary> 

    void OnLevelWasLoaded(int index)
    {
        if(index == 0)
        {
            Debug.Log("Woohoo");
        }
    }
}
