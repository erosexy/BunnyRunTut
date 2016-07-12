using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BunnieController : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public float BunnieJumpForce = 500f;
    private float BunnieHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    public Text eggsText;
    private string topCounter;
    private string presentCounter;
    private float startTime;
    private int eggsCollected;
    private int jumpsLeft = 0;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    public AudioSource eeSfx;

    //variáveis que contém objetos
    private GameObject topScoreText, topEasterEggsText, bgm;
    public GameObject topScoreTxt, topEasterEggsTxt;

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
            GameObject.Find("Bunnie").transform.position = new Vector2(-3, transform.position.y);
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

            ScoreCalculation();
            deathSfx.Play();
            BunnieHurtTime = Time.time;
            myAnim.SetBool("bunnieHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * BunnieJumpForce);
            myCollider.enabled = false;
        }
        
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Itens"))
        {
            GameObject.Find("Bunnie").transform.position = new Vector2(-3, transform.position.y);
            Destroy(collision.gameObject);
            eeSfx.Play();
            eggsCollected++;
            eggsText.text = eggsCollected.ToString();
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

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.8f);

        //Application.LoadLevel("Title");
        SceneManager.LoadScene("GameOver");
    }

    void ScoreCalculation()
    {
        topCounter = topEasterEggsText.GetComponent<Text>().text;
        presentCounter = eggsText.text;

        if (topEasterEggsText.GetComponent<Text>().text == "")
        {
            topEasterEggsText.GetComponent<Text>().text = eggsCollected.ToString();
        }
        else if (int.Parse(topCounter) < int.Parse(presentCounter))
        {
            topCounter = presentCounter;
            topEasterEggsText.GetComponent<Text>().text = topCounter.ToString();
        }

        SaveEggsScore(topEasterEggsText.GetComponent<Text>().text);

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

    void SaveEggsScore(string eggsScore)
    {
        PlayerPrefs.SetString("Eggs Score", topEasterEggsText.GetComponent<Text>().text);
    }

    string GetEggsScore()
    {
        return PlayerPrefs.GetString("Eggs Score");
    }

    void SaveScore(string Score)
    {
        PlayerPrefs.SetString("Score", topEasterEggsText.GetComponent<Text>().text);
    }

    string GetScore()
    {
        return PlayerPrefs.GetString("Score");
    }
    public void HideScore()
    {
        topEasterEggsText.GetComponent<Text>().text = "";
        topScoreText.GetComponent<Text>().text = "";
        topEasterEggsTxt.GetComponent<Text>().text = "";
        topScoreTxt.GetComponent<Text>().text = "";
    }

    void ShowScore()
    {
        //encontra os objetos
        topEasterEggsText = GameObject.Find("lblEasterEggsTop");
        topScoreText = GameObject.Find("lblScoreTop");

        topEasterEggsText.GetComponent<Text>().text = GetEggsScore();
        topScoreText.GetComponent<Text>().text = GetScore();

        if (topEasterEggsText.GetComponent<Text>().text == "")
        {
            topEasterEggsText.GetComponent<Text>().text = "0";
        }
        if (topScoreText.GetComponent<Text>().text == "")
        {
            topScoreText.GetComponent<Text>().text = "0.0";
        }
        topEasterEggsTxt.GetComponent<Text>().text = "Top Easter Eggs: ";
        topScoreTxt.GetComponent<Text>().text = "Top Score: ";
    }
}
