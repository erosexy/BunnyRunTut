﻿using UnityEngine;
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
    public GameObject bubble;
    private string aux;
	private float startTime;
    private int eggsCollected;
    private float scoreAux;
	private int jumpsLeft = 0;
    private bool invincible = false;
	public AudioSource jumpSfx;
	public AudioSource deathSfx;
	public AudioSource eeSfx;
    private string topEggsCounter, presentEggsCounter, topScoreCounter, presentScoreCounter;
    //private bool destroy;

    //variáveis que contém objetos
    private GameObject lifes3, lifes2, lifes1, tempScoreText, tempEasterEggsText, bgm;

    public GameObject btnBack, btnPause, tempEasterEggsTxt, tempScoreTxt, EasterEggsTxt, ScoreTxt;    

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
        tempScoreText = GameObject.Find("lblScoreTemp");
        tempEasterEggsText = GameObject.Find("lblEasterEggsTemp");
        EasterEggsTxt = GameObject.Find("lblEggs");
        ScoreTxt = GameObject.Find("lblScore");
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
        //tempEasterEggsText.GetComponent<Text>().text = "0";
        //tempScoreText.GetComponent<Text>().text = "0.0";

        //if (tempEasterEggsText.GetComponent<Text>().text == "")
        //{
        //    tempEasterEggsText.GetComponent<Text>().text = "0";
        //}
        //if(tempScoreText.GetComponent<Text>().text == "")
        //{
        //    tempScoreText.GetComponent<Text>().text = "0.0";
        //}

        ShowScore();
        bubble.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bgm.GetComponent<AudioSource>().Stop();
            lifes3.GetComponent<Renderer>().enabled = false;
            lifes2.GetComponent<Renderer>().enabled = false;
            lifes1.GetComponent<Renderer>().enabled = false;
            SceneManager.LoadScene("Title");
            btnBack.SetActive(false);
            btnPause.SetActive(false);
            HideScore();
            //Application.LoadLevel("Title"); não recomendado para Unity 5.x em diante
        }

        if (bunnyHurtTime == -1) {

            //se a tecla "espaço" for pressionada, o sprite pula
            if (Input.GetButtonUp("Jump") && jumpsLeft > 0 || Input.GetMouseButtonDown(0) && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }

                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce * 0.75f);
                    Debug.Log("Second Jump");
                }
                else {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce);
                    Debug.Log("First Jump");
                }
                jumpSfx.Play();
                jumpsLeft--;
            }
            //myAnim.SetFloat ("vVelocity", myRigidBody.velocity.y);

            //usando o valor absoluto faz o sprite de pulo estar presente em todo o salto
            myAnim.SetFloat("vVelocity", Mathf.Abs(myRigidBody.velocity.y));
            scoreText.text = (Time.time - startTime).ToString("0.0");
            //totalScoreText.text = scoreText.ToString();
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

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && invincible)
        {
            Destroy(collision.gameObject);
            GameObject.Find("Bunny").transform.position = new Vector2(-3, transform.position.y);
        }

		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy") && !invincible) {
            //se o objeto tiver sido achado
            GameObject.Find("Bunny").transform.position = new Vector2(-3, transform.position.y);
            if (lifes3.GetComponent<Renderer>().enabled)
            {
                lifes3.GetComponent<Renderer>().enabled = false;
                //lifes3.transform.localScale = new Vector3(0, 0, 0);
                Debug.Log("Lifes3 desativado");

                //destroy = true;

                deathSfx.Play();
                myRigidBody.velocity = Vector2.zero;
                myRigidBody.AddForce(transform.up * bunnyJumpForce);
                invincible = true;
                bubble.GetComponent<Renderer>().enabled = true;
                StartCoroutine("InvencibiltyTime");
            }
            else if (lifes2.GetComponent<Renderer>().enabled)
            {
                lifes2.GetComponent<Renderer>().enabled = false;
                //lifes2.transform.localScale = new Vector3(0, 0, 0);
                Debug.Log("Lifes2 desativado");
                deathSfx.Play();
                myRigidBody.velocity = Vector2.zero;
                myRigidBody.AddForce(transform.up * bunnyJumpForce);
                invincible = true;
                bubble.GetComponent<Renderer>().enabled = true;
                StartCoroutine("InvencibiltyTime");
            }
            else if (lifes1.GetComponent<Renderer>().enabled)
            {
                lifes1.GetComponent<Renderer>().enabled = false;
                //lifes1.transform.localScale = new Vector3(0, 0, 0);
                //lifes1.SetActive(false); //desativa o objeto, recomendado a partir do Unity 5.x
                //Application.LoadLevel("GameOver");

                foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
                {
                    spawner.enabled = false;

                }
                foreach (EggPrefabSpawner spawner in FindObjectsOfType<EggPrefabSpawner>())
                {
                    spawner.enabled = false;
                }

                foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
                {
                    moveLefter.enabled = false;
                }

                ScoreTempCalculation();
                
                bunnyHurtTime = Time.time;
                myAnim.SetBool("bunnyHurt", true);
                deathSfx.Play();
                myRigidBody.velocity = Vector2.zero;
                myRigidBody.AddForce(transform.up * bunnyJumpForce);
                myCollider.enabled = false;
                bgm.GetComponent<AudioSource>().Stop();

                btnBack.SetActive(false);
                btnPause.SetActive(false);

                StartCoroutine("GameOver");
                //SceneManager.LoadScene("GameOver");
            }
        } 
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Itens")) {
            Destroy (collision.gameObject);
			eeSfx.Play ();
            eggsCollected++;
            eggsText.text = eggsCollected.ToString();
            GameObject.Find("Bunny").transform.position = new Vector2(-3, transform.position.y);
        }
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
            Debug.Log("Jumps recovered");
        }
        
    }

    private void ScoreTempCalculation()
    {
        topEggsCounter = tempEasterEggsText.GetComponent<Text>().text;
        presentEggsCounter = EasterEggsTxt.GetComponent<Text>().text;
        if (eggsCollected > 0)
        {
            if (tempEasterEggsText.GetComponent<Text>().text == "0")
            {
                tempEasterEggsText.GetComponent<Text>().text = eggsCollected.ToString();
            }
            else if (int.Parse(topEggsCounter) < int.Parse(presentEggsCounter))
            {
                aux = tempEasterEggsText.GetComponent<Text>().text;
                eggsCollected = int.Parse(aux);
                //aux = eggsText.text;
                //eggsCollected += int.Parse(aux);
                tempEasterEggsText.GetComponent<Text>().text = eggsCollected.ToString();
            }
            SaveEggsScore(tempEasterEggsText.GetComponent<Text>().text);
        }

        topScoreCounter = tempScoreText.GetComponent<Text>().text;
        presentScoreCounter = ScoreTxt.GetComponent<Text>().text;

        if (float.Parse(scoreText.text.ToString()) > 0.0)
        {
            if (tempScoreText.GetComponent<Text>().text == "0.0")
            {
                tempScoreText.GetComponent<Text>().text = scoreText.text.ToString();
            }
            else if(double.Parse(topScoreCounter) < double.Parse(presentScoreCounter))
            {
                aux = tempScoreText.GetComponent<Text>().text.ToString();
                scoreAux = float.Parse(aux); 
                //aux = scoreText.text.ToString();
                //scoreAux += float.Parse(aux);
                tempScoreText.GetComponent<Text>().text = scoreAux.ToString();
            }
            SaveScore(tempScoreText.GetComponent<Text>().text);
        }
    }

    IEnumerator StopDestruction()
    {
        yield return new WaitForSeconds(2);
        //destroy = false;
        Debug.Log("Not destroy");
    }

    IEnumerator InvencibiltyTime()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Not invincible!");
        bubble.GetComponent<Renderer>().enabled = false;
        invincible = false;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.8f);

        //Application.LoadLevel("Title");
        HideScore();
        SceneManager.LoadScene("GameOver");
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

    void SaveEggsScore(string eggsScore)
    {
        PlayerPrefs.SetString("Life Mode - Eggs Score", tempEasterEggsText.GetComponent<Text>().text);
    }

    string GetEggsScore()
    {
        return PlayerPrefs.GetString("Life Mode - Eggs Score");
    }

    void SaveScore(string Score)
    {
        PlayerPrefs.SetString("Life Mode - Score", tempScoreText.GetComponent<Text>().text);
    }

    string GetScore()
    {
        return PlayerPrefs.GetString("Life Mode - Score");
    }

    public void HideScore()
    {
        //tempEasterEggsText.SetActive(false);
        //tempScoreText.SetActive(false);
        //tempEasterEggsTxt.SetActive(false);
        //tempScoreTxt.SetActive(false);
        tempEasterEggsText.GetComponent<Text>().text = "";
        tempScoreText.GetComponent<Text>().text = "";
        tempEasterEggsTxt.GetComponent<Text>().text = "";
        tempScoreTxt.GetComponent<Text>().text = "";
    }

    void ShowScore()
    {
        tempEasterEggsTxt = GameObject.Find("txtEasterEggsTemp");
        tempScoreTxt = GameObject.Find("txtScoreTemp");
        tempScoreText = GameObject.Find("lblScoreTemp");
        tempEasterEggsText = GameObject.Find("lblEasterEggsTemp");
        tempEasterEggsText.GetComponent<Text>().text = GetEggsScore();
        tempScoreText.GetComponent<Text>().text = GetScore();

        if (tempEasterEggsText.GetComponent<Text>().text == "")
        {
            tempEasterEggsText.GetComponent<Text>().text = "0";
        }
        if (tempScoreText.GetComponent<Text>().text == "")
        {
            tempScoreText.GetComponent<Text>().text = "0.0";
        }
        tempEasterEggsTxt.GetComponent<Text>().text = "Easter Eggs Collected(lifes mode): ";
        tempScoreTxt.GetComponent<Text>().text = "Score(lifes mode): ";
    }

}
