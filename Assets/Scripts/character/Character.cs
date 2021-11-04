using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public Animator anim;
    float speed = 10f;
    float jump_v = 30f;
    bool is_jumping = false;
    bool is_right = true;
    int jump_count = 0;
    public AudioSource jump_audio;

    void OnEnable()
    {
    }
    //PhotonView view;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        //view = GetComponent<PhotonView>();
    }
    void Update()
    {
        //if (view.IsMine)
        //{
        if (Time.timeScale > 0f)
        {
            walk();
            if (Input.GetKeyDown(KeyCode.Space) && (!is_jumping || jump_count < 2))
            {
                jump_count++;
                if (jump_count == 2)
                {
                    anim.SetBool("is_double_jump", true);
                }
                is_jumping = true;
                anim.SetBool("is_jumping", true);
                jump();
            }

        }
        //}

        //flip();
    }
    //transform.localScale = new Vector3(transform.localScale.x* -1, transform.localScale.y);
    private void flip()
    {
        if (is_right == true)
        {
        }
        else if (is_right == false)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }
    private void walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(moveVector.x, rb.velocity.y);
        if (moveVector.x != 0)
        {
            anim.SetBool("is_running", true);
        }
        else
        {
            anim.SetBool("is_running", false);
        }
        if (moveVector.x > 0 && is_right != false)
        {
            is_right = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (moveVector.x < 0 && is_right != true)
        {
            is_right = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    private void jump()
    {
        jump_audio.Play();
        rb.velocity = Vector2.up * jump_v;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //if (view.IsMine)
        //{
        if (col.gameObject.tag == "Floor")
        {
            jump_count = 0;
            is_jumping = false;
            anim.SetBool("is_jumping", false);
            anim.SetBool("is_double_jump", false);
        }
        if (col.gameObject.tag == "Drop")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Water")
        {
            rb.gravityScale = 1f;
        }
        //}
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Water")
        {
            rb.gravityScale = 15f;
        }
    }
}
