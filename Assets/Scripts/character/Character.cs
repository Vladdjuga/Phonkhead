using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public Animator anim;
    float speed = 20f;
    float jump_v = 50f;
    bool is_jumping = false;
    int jump_count = 0;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        walk();
        if (Input.GetKeyDown(KeyCode.Space) && (!is_jumping||jump_count<2))
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
        if (moveVector.x < 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveVector.x > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
    private void jump()
    {
        rb.velocity = Vector2.up * jump_v;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            jump_count=0;
            is_jumping = false;
            anim.SetBool("is_jumping", false);
            anim.SetBool("is_double_jump", false);
        }
    }
}
