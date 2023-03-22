using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public TextMeshProUGUI score;
    public TextMeshProUGUI Lives;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private int scoreValue = 0;
    private int LivesValue = 3;
   
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;
    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins:" + scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        Lives.text = "Lives:" + LivesValue.ToString();

        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal"); 
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

         if (hozMovement > 0)
        {
            anim.SetInteger("State", 2);
        }
        

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (isOnGround == false && vertMovement > 0)
        {
            anim.SetInteger("State", 1);
        }
        else if (isOnGround == true && vertMovement < 0)
        {
            anim.SetInteger("State", 0);
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       

       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        
        if (scoreValue == 4)
        {
            transform.position = new Vector3(-12.44f, 48.57f, -7.0f);

        }

        if (scoreValue >= 9)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
           
    
        }


        if (collision.collider.tag == "Enemy")
        {
            LivesValue -= 1;
            Lives.text = "Lives:" + LivesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (LivesValue == 0)
        {
            loseTextObject.SetActive(true);
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
            }
        }
    }
}
