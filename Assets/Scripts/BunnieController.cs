using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BunnieController : MonoBehaviour
{

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public float BunnieJumpForce = 500f;
    private float BunnieHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    public Text eggsText;
    private float startTime;
    private int eggsCollected;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    public AudioSource eeSfx;
    

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Title");
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
                Application.LoadLevel(Application.loadedLevel);
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
}
