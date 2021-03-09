using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 8f, distance, jumpForce = 10f, invulTime = 2f;
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

    private Animator anim;

    [HideInInspector]
    public bool canMove = true;
    bool bisaKena = true, invulnerable, attacking, kena;

    public AudioClip jump, kesetrum;
    

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
        if (Input.GetButtonDown("Fire1") && !attacking && canMove)
        {
            //AudioManager.audioManager.PlaySound(attack);
            StartCoroutine(Attack());
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
            SceneLoader.sceneLoader.PlaySound(jump);
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
        if (SceneLoader.sceneLoader.health <= 0 || !bisaKena) return;
        if (collision.gameObject.CompareTag("Electro"))
        {
            if (!invulnerable)
                StartCoroutine(SceneLoader.sceneLoader.ChangeHealth());
            kena = true;
            Electro electro = collision.GetComponent<Electro>();
            Vector2 direction = (transform.position - electro.transform.position).normalized;
            StartCoroutine(duar(direction * electro.pushForce, 1f, 1f));
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!invulnerable)
                StartCoroutine(SceneLoader.sceneLoader.ChangeHealth());
            
            kena = true;
            Obstacle obstacle = collision.GetComponent<Obstacle>();
            Vector2 direction = (transform.position - obstacle.transform.position).normalized;
            StartCoroutine(duar(direction * obstacle.pushForce, 0.5f, 0.5f));
        }

        else if (collision.gameObject.CompareTag("Electrocute"))
        {
            if (!invulnerable)
                StartCoroutine(SceneLoader.sceneLoader.ChangeHealth());
            kena = true;
            Obstacle obstacle = collision.GetComponentInParent<Obstacle>();
            Vector2 direction = (transform.position - obstacle.transform.position).normalized;
            StartCoroutine(duar(direction * obstacle.pushForce, 0.5f, 0.5f));
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        anim.SetTrigger("Attack");
        Collider2D[] destroyables = Physics2D.OverlapCircleAll(weapon.transform.position, atkRadius, obsLayer);

        foreach(Collider2D obs in destroyables)
        {
            
            Obstacle obstacle = obs.gameObject.GetComponent<Obstacle>();
            obstacle.takeDamage();
        }
        yield return new WaitForSeconds(0.33f);
        attacking = false;
    }

    IEnumerator invulnerability(float invulTime)
    {
        anim.SetBool("Invul", true);
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
        bisaKena = true;
        anim.SetBool("Invul", false);
    }

    IEnumerator duar(Vector2 force, float stunTime,float invulTime)
    {
        invulnerable = true;
        anim.SetBool("Invul", false);

        anim.SetBool("Kesetrum", true);
        SceneLoader.sceneLoader.PlaySound(kesetrum);
        canMove = kena = false;
        if(SceneLoader.sceneLoader.health <= 0)
        {
            yield return new WaitForSeconds(stunTime);
            anim.SetBool("Kesetrum", false);
            anim.SetTrigger("Mati");
            canMove = bisaKena = false;
            yield break;
        }
        rb.AddForce(force);
        yield return new WaitForSeconds(stunTime);
        if (kena) yield break;
        canMove = true;
        anim.SetBool("Kesetrum", false);
        bisaKena = false;
        StartCoroutine(invulnerability(invulTime));
    }
}