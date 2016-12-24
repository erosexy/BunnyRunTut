using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BunnieNatalController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public float BunnieJumpForce = 500f;
    private float BunnieHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    public Text presentsText;
    private string topCounter;
    private string presentCounter;
    private float startTime;
    private int presentsCollected;
    private int jumpsLeft = 0;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    public AudioSource eeSfx;

    //variáveis que contém objetos
    private GameObject topScoreText, topPresentsText, bgm;
    public GameObject topScoreTxt, topPresentsTxt, btnBack, btnPause;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;

        //encontra o objeto que tem a música de fundo
        bgm = GameObject.Find("BackgroundMusic");
        //se a música estiver parada/pausada
        if (!bgm.GetComponent<AudioSource>().isPlaying)
        {
            //a música toca de novo
            bgm.GetComponent<AudioSource>().Play();
            bgm.GetComponent<AudioSource>().volume = 0.5f;
        }
        ShowScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bgm.GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("Title");
            btnBack.SetActive(false);
            btnPause.SetActive(false);
            HideScore();

        }

        if (BunnieHurtTime == -1)
        {
            //se a tecla "espaço" for pressionada, o sprite pula
            if (Input.GetButtonUp("Jump") && jumpsLeft > 0 || Input.GetMouseButtonDown(0) && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }

                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * BunnieJumpForce * 0.75f);
                    Debug.Log("Second Jump");
                }
                else{
                    myRigidBody.AddForce(transform.up * BunnieJumpForce);
                    Debug.Log("First Jump");
                }
                jumpSfx.Play();
                jumpsLeft--;
            }
            //myAnim.SetFloat ("vVelocity", myRigidBody.velocity.y);

            //usando o valor absoluto faz o sprite de pulo estar presente em todo o salto
            myAnim.SetFloat("vVelocity", Mathf.Abs(myRigidBody.velocity.y));
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else {
            //apos um tempo, reinicia a cena
            if (Time.time > BunnieHurtTime + 2)
            {
                //LoadLevel serve para carregar outras cenas do jogo
                //Application.LoadLevel(Application.loadedLevel);
                bgm.GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // SceneManager.LoadScene("GameBunnie");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameObject.Find("BunnieNatal").transform.position = new Vector2(-3, transform.position.y);
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (PresentPrefabSpawner spawner in FindObjectsOfType<PresentPrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            ScoreCalculation();
            deathSfx.Play();
            BunnieHurtTime = Time.time;
            myAnim.SetBool("bunnieNatalHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * BunnieJumpForce);
            myCollider.enabled = false;
        }
        
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Itens"))
        {
            GameObject.Find("BunnieNatal").transform.position = new Vector2(-3, transform.position.y);
            Destroy(collision.gameObject);
            eeSfx.Play();
            presentsCollected++;
            presentsText.text = presentsCollected.ToString();
            //scoreText.text += (scoreText + 100).ToString("0.0");
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

    void ScoreCalculation()
    {
        topCounter = topPresentsText.GetComponent<Text>().text;
        presentCounter = presentsText.text;

        if (topPresentsText.GetComponent<Text>().text == "")
        {
            topPresentsText.GetComponent<Text>().text = presentsCollected.ToString();
        }
        else if (int.Parse(topCounter) < int.Parse(presentCounter))
        {
            topCounter = presentCounter;
            topPresentsText.GetComponent<Text>().text = topCounter.ToString();
        }

        SavePresentsScore(topPresentsText.GetComponent<Text>().text);

        topCounter = topScoreText.GetComponent<Text>().text;
        presentCounter = scoreText.text;

        if (topScoreText.GetComponent<Text>().text == "")
        {
            topScoreText.GetComponent<Text>().text = scoreText.text.ToString();
        }
        else if (float.Parse(topCounter) < float.Parse(presentCounter))
        {
            topCounter = presentCounter;
            topScoreText.GetComponent<Text>().text = topCounter.ToString();
        }

        SaveScore(topScoreText.GetComponent<Text>().text);
    }

    void SavePresentsScore(string eggsScore)
    {
        PlayerPrefs.SetString("Presents Score", topPresentsText.GetComponent<Text>().text);
    }

    string GetPresentsScore()
    {
        return PlayerPrefs.GetString("Presents Score");
    }

    void SaveScore(string Score)
    {
        PlayerPrefs.SetString("Score", topScoreText.GetComponent<Text>().text);
    }

    string GetScore()
    {
        return PlayerPrefs.GetString("Score");
    }
    public void HideScore()
    {
        topPresentsText.GetComponent<Text>().text = "";
        topScoreText.GetComponent<Text>().text = "";
        topPresentsTxt.GetComponent<Text>().text = "";
        topScoreTxt.GetComponent<Text>().text = "";
    }

    void ShowScore()
    {
        //encontra os objetos
        topPresentsText = GameObject.Find("lblPresentsTop");
        topScoreText = GameObject.Find("lblScoreTop");

        topPresentsText.GetComponent<Text>().text = GetPresentsScore();
        topScoreText.GetComponent<Text>().text = GetScore();

        if (topPresentsText.GetComponent<Text>().text == "")
        {
            topPresentsText.GetComponent<Text>().text = "0";
        }
        if (topScoreText.GetComponent<Text>().text == "")
        {
            topScoreText.GetComponent<Text>().text = "0.0";
        }
        topPresentsTxt.GetComponent<Text>().text = "Top Presents: ";
        topScoreTxt.GetComponent<Text>().text = "Top Score: ";
    }
}
