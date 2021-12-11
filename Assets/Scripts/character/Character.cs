//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public Animator anim;
    public float speed = 10f;
    public float jump_v = 30f;
    public bool is_jumping = false;
    public bool is_right = true;
    public int jump_count = 0;
    public AudioSource jump_audio;
    public Inventory inventory;
    public TilesList atlas;
    public float move=0f;
    //public PhotonView view;
    //public PlayerTransform player_transform;

    void OnEnable()
    {
        //player_transform.player = this.gameObject;
    }
    //PhotonView view;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        //view = GetComponent<PhotonView>();
        //if (view.IsMine)
        //{
        //    GameObject playerCam = PhotonNetwork.Instantiate("Main Camera", new Vector3(this.transform.position.x, this.transform.position.y + 3f, -50), Quaternion.identity, 0);
        //    playerCam.GetComponent<Camera>().enabled = true;
        //    playerCam.GetComponent<CameraController>().enabled = true;
        //    playerCam.GetComponent<CameraController>().player_transform = this.transform;
        //}
    }
    float doubleTapTime = 0f;
    bool doubleTap = false;
    void Update()
    {
        //if (view.IsMine)
        //{
        if (Time.timeScale > 0f)
        {
            walk(move);
            if (Input.GetKeyDown(KeyCode.Space) && (!is_jumping || jump_count < 2))
            {
                jump_count++;
                if (jump_count == 2)
                {
                    anim.SetBool("is_double_jump", true);
                }
                is_jumping = true;
                anim.SetBool("is_jumping", true);
                jump(jump_v);
            }

            if (Input.GetAxisRaw("Horizontal")!=0)
            {
                walk();
            }

            //if (Input.GetButtonDown("Horizontal") && !doubleTap)
            //{
            //    doubleTap = true;
            //    doubleTapTime = Time.time;
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
                   inventory.AddItem("dirt");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
                inventory.AddItem("stone");
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
    public void walk(float axis)
    {
        moveVector.x = axis * speed;
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
    public void jump(float freg)
    {
        jump_audio.Play();
        rb.velocity = Vector2.up * freg;
    }
    private void strike(float freg,bool is_right)
    {
        //jump_audio.Play();
        Vector2 hor = !is_right ? Vector2.right : Vector2.left;
        rb.AddRelativeForce(hor * freg,ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //if (view.IsMine)
        //{
        if (col.gameObject.tag == "Floor"&&col.gameObject.transform.position.y<this.transform.position.y)
        {
            jump_count = 0;
            is_jumping = false;
            anim.SetBool("is_jumping", false);
            anim.SetBool("is_double_jump", false);
        }
        if (col.gameObject.tag == "Drop")
        {
            Destroy(col.gameObject);
            inventory.AddItem(col.gameObject.name);
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
