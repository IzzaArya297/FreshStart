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

    public BoxCollider2D boxCollider;
    private LayerMask m_WhatIsGround;   

    [HideInInspector]
    public GameObject currentInput;

    public GameObject weapon;
    public float atkRadius;
    public LayerMask obsLayer;

    //private AudioSource audioSource;
    public AudioClip jumpSound;

    private Animator anim;
    public AnimationClip kesetrum;

    public bool canMove = true, invulnerable;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
        m_WhatIsGround = LayerMask.GetMask("Ground");
        anim = GetComponent<Animator>();
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
        else anim.SetBool("Jalan", false);
    }

    private void Update()
    {
        PlayerJump(GroundCheck());
        if(health <= 0)
        {
            //matii
            canMove = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void PlayerJump(bool canJump)
    {
        if (!canJump)
            return;
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            anim.SetTrigger("Loncat");
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
            anim.SetBool("Jalan", true);
            if (move < 0 && lookRight) Flip();
            else if (move > 0 && !lookRight) Flip();
        }
        else
        {
            //idle
            anim.SetBool("Jalan", false);
        }
    }



    bool GroundCheck()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, distance, m_WhatIsGround);
        //Color rayColor;
        if (raycastHit.collider != null)
        {
            anim.SetBool("DiTanah", true);
            //rayColor = Color.green;
        }
        else
        {
            anim.SetBool("DiTanah", false);
            //rayColor = Color.red;
        }

        //Buat debug biar bisa liat collidernya
        //Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + distance), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + distance), rayColor);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + distance + 0.5f), Vector2.right * (boxCollider.bounds.extents.x * 2f), rayColor);

        return raycastHit.collider != null;
    }

    void Flip()
    {
        lookRight = !lookRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!invulnerable)
        {
            if (collision.gameObject.tag == "Electro")
            {
                Electro electro = collision.GetComponent<Electro>();
                health--;
                Vector2 direction = (transform.position - electro.transform.position).normalized;
                StartCoroutine(duar(direction * electro.pushForce, 1f));
                StartCoroutine(invulnerability(invulTime));
                SceneLoader.sceneLoader.ChangeHealth();
            }
            if(collision.gameObject.tag == "Obstacle")
            {
                Obstacle obstacle = collision.GetComponent<Obstacle>();
                health--;
                //Debug.Log("derrrr");
                Vector2 direction = (transform.position - obstacle.transform.position).normalized;
                StartCoroutine(duar(direction * obstacle.pushForce, 0.25f));
                StartCoroutine(invulnerability(invulTime - invulTime + 0.1f));
                SceneLoader.sceneLoader.ChangeHealth();
            }

            if(collision.gameObject.tag == "Electrocute")
            {
                Obstacle obstacle = collision.GetComponentInParent<Obstacle>();
                health--;
                //Debug.Log("derrrr");
                Vector2 direction = (transform.position - obstacle.transform.position).normalized;
                StartCoroutine(duar(direction * obstacle.pushForce, 0.25f));
                StartCoroutine(invulnerability(invulTime));
                SceneLoader.sceneLoader.ChangeHealth();
            }
            
        }

    }

    void Attack()
    {
        Collider2D[] destroyables = Physics2D.OverlapCircleAll(weapon.transform.position, atkRadius, obsLayer);

        foreach(Collider2D obs in destroyables)
        {
            
            Obstacle obstacle = obs.gameObject.GetComponent<Obstacle>();
            obstacle.takeDamage();
        }
        
        
    }

    IEnumerator invulnerability(float invulTime)
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
    }

    IEnumerator duar(Vector2 force, float stunned)
    {
        canMove = false;
        rb.AddForce(force);
        yield return new WaitForSeconds(stunned);
        canMove = true;
    }
}
