using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    private Rigidbody2D playerRigidbody;
    private AudioSource playerAudio;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;

            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerRigidbody.velocity = Vector2.zero;
            playerAudio.Play();

        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        else if (!Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity *1.015f ;

        }
        animator.SetBool("Grounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dead" && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        //playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

}
