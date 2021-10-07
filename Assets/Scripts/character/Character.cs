using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public Animator anim;
    float speed = 10f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        walk();
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
}
