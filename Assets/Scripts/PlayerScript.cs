using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private int scoreValue = 0;
    private int lives = 3;
    Animator anim;
    private bool facingRight = true;
    private bool isJumping = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (lives == 0)
        {
            winText.text = "You lose.";
            Destroy(gameObject);
        }
        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
        if (isJumping == false && vertMovement == 0)
            {
                anim.SetInteger("State", 0);
            }
        else if (isJumping == true && vertMovement > 0)
            {
                anim.SetInteger("State", 2);
            }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
           Destroy(collision.collider.gameObject);
                if (scoreValue == 4)
                     {
                         transform.position = new Vector2(87f, 16f);
                         lives = 3;
                         livesText.text = "Lives: " + lives.ToString();
                     }
                if (scoreValue >= 8)
                      {
                         winText.text = "You win! Game created by Ester Agas.";
                        musicSource.clip = musicClipTwo;
                         musicSource.Play();
                        }
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0,3), ForceMode2D.Impulse);
            }
        }

    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
