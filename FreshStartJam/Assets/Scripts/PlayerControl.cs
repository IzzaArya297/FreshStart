using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 8f, distance, jumpForce = 10f, invulTime = 2f;
    public int health = 2;
    bool lookRight = true;

    [HideInInspector]
    public Rigidbody2D rb;

    BoxCollider2D boxCollider;
    private LayerMask m_WhatIsGround;   

    [HideInInspector]
    public GameObject currentInput;

    //private AudioSource audioSource;
    public AudioClip jumpSound;

    public Animator anim;
    public AnimationClip kesetrum;

    public bool canMove = true, invulnerable;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        m_WhatIsGround = LayerMask.GetMask("Ground");
        //audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove) PlayerMove();
    }

    private void Update()
    {
        PlayerJump();
        if(health == 0)
        {
            //matii
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck() && canMove)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            //audioSource.PlayOneShot(jumpSound);
        }
    }


    void PlayerMove()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (move != 0)
        {
            //animasi jalan
            //anim.SetBool("Walk", true);
            if (move < 0 && lookRight) Flip();
            else if (move > 0 && !lookRight) Flip();
        }
        else
        {
            //idle
            //anim.SetBool("Walk", false);
        }
    }

    bool GroundCheck()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, distance, m_WhatIsGround);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        //Buat debug biar bisa liat collidernya
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + distance), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + distance), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + distance), Vector2.right * (boxCollider.bounds.extents.x * 2f), rayColor);

        return raycastHit.collider != null;
    }

    void Flip()
    {
        lookRight = !lookRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!invulnerable && collision.gameObject.tag == "Electro")
        {
            Electro electro = collision.GetComponent<Electro>();
            health--;
            Vector2 direction = (transform.position - electro.transform.position).normalized;
            StartCoroutine(duar(direction * electro.pushForce));
            StartCoroutine(invulnerability(invulTime));
        }
    }

    IEnumerator invulnerability(float invulTime)
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
    }

    IEnumerator duar(Vector2 force)
    {
        canMove = false;
        rb.velocity = (force);
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (collision.gameObject.CompareTag("Electro"))
        //{
        //    anim.SetTrigger("Kesetrum");
        //    GameManager.gameManager.Invoke("GameOver", kesetrum.length);
        //}
    }
}
