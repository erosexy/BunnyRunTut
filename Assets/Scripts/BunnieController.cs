﻿using UnityEngine;
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
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    public AudioSource eeSfx;

    //variáveis que contém objetos
    private GameObject lifes3;
    private GameObject lifes2;
    private GameObject lifes1;
    private GameObject topScoreText;
    private GameObject topEasterEggsText;
    private GameObject bgm;


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
        }

        //encontra os objetos
        topEasterEggsText = GameObject.Find("lblEasterEggsTop");
        topScoreText = GameObject.Find("lblScoreTop");
        lifes3 = GameObject.Find("lifes3");
        lifes2 = GameObject.Find("lifes2");
        lifes1 = GameObject.Find("lifes1");
        if (!lifes3.GetComponent<Renderer>().enabled && !lifes2.GetComponent<Renderer>().enabled && !lifes1.GetComponent<Renderer>().enabled)
        {
            lifes1.GetComponent<Renderer>().enabled = true;
            lifes2.GetComponent<Renderer>().enabled = true;
            lifes3.GetComponent<Renderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bgm.GetComponent<AudioSource>().Stop();
            lifes3.GetComponent<Renderer>().enabled = false;
            lifes2.GetComponent<Renderer>().enabled = false;
            lifes1.GetComponent<Renderer>().enabled = false;
            //Application.LoadLevel("Title");
            SceneManager.LoadScene("Title");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
            //Application.LoadLevel("Title");
        }

        if (BunnieHurtTime == -1)
        {

            //se a tecla "espaço" for pressionada, o sprite pula
            if (Input.GetButtonUp("Jump") && jumpsLeft > 0)
            {

                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }

                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * BunnieJumpForce * 0.75f);
                }
                else {
                    myRigidBody.AddForce(transform.up * BunnieJumpForce);
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
                SceneManager.LoadScene("GameBunnie");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
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
                StartCoroutine("GameOver");
                //SceneManager.LoadScene("GameOver");
            }


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
            deathSfx.Play();
            BunnieHurtTime = Time.time;
            myAnim.SetBool("bunnieHurt", true);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * BunnieJumpForce);
            myCollider.enabled = false;
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Itens"))
        {
            Destroy(collision.gameObject);
            eeSfx.Play();
            eggsCollected++;
            eggsText.text = eggsCollected.ToString();
            //scoreText.text += (scoreText + 100).ToString("0.0");
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.8f);

        //Application.LoadLevel("Title");
        SceneManager.LoadScene("GameOver");
    }
}
