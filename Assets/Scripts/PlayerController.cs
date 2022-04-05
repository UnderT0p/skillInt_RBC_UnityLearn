using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    [SerializeField] private float Speed, JumpForce, groundCheckRadius, SuricaneRotation, SuricaneForce, damageTime;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private GameObject Suricane;
    [SerializeField] private Transform firePoint;

    
    [SerializeField] private GameObject pauseMenu;
    

    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private float moveX, damagedTimer;
    private bool grounded, landing;
    private float firePointX;

    private bool damaged;

    void Start()
    {
        
        damaged = false; // получение урона
        rb = GetComponent<Rigidbody2D>();
        sp = Anim.gameObject.GetComponent<SpriteRenderer>();
        firePointX = firePoint.localPosition.x;
        
    }

    void Update()
    {
        if(damagedTimer > 0)
        {
            damagedTimer -= Time.deltaTime; //нивелируем разницу в частоте кадров
        }
        else
        {
            damaged = false;
            sp.color = Color.white;
        }

        landing = grounded;// переменна для опознавания преземления

        moveX = Input.GetAxis("Horizontal");
        grounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, WhatIsGround);

        if(grounded == true && landing == false)
        {
            EventManager.eventOnPlayerLanding();
        }

        Vector2 velo = Vector2.zero;
        velo.x = moveX * Speed;
        velo.y = rb.velocity.y;

        rb.velocity = velo;

        if (Input.GetButtonDown("Jump") && grounded && !pauseMenu.activeSelf)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            EventManager.eventOnPlayerJump();
        }

        StartCoroutine(FireCheck());
        Flip();
        AnimatorCheck();
    }
    void AnimatorCheck()
    {
        Anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        Anim.SetFloat("VelocityY", rb.velocity.y);
        Anim.SetBool("Grounded", grounded);
    }

    IEnumerator FireCheck()
    {
        
        if (Input.GetMouseButtonDown(0) && grounded && !pauseMenu.activeSelf)
        {
            Anim.SetTrigger("Attack"); 
            
            yield return new WaitForSeconds(0.1f); // позволяет сделать паузу при исполнении

            Suricane suricane = Instantiate(Suricane, firePoint.position, Quaternion.identity).GetComponent<Suricane>();// создаем сюрикен по префабу
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // поиск точки на экране, перевод в мировые координаты и передача в вектор
            Vector3 force = mousePos - firePoint.position; //вычисление ветора силы для сюрикена. Т.е. из вектора с позицией курсора нужно вычесть вектор точки вылета сюрикена 
            suricane.SetForceAndRotation(force.normalized * SuricaneForce, SuricaneRotation); // передаем силу и вращение
            EventManager.eventOnPlayerSuricaneAttack();
            
        }
    }

    void Flip()
    {
        if (moveX > 0)
        {
            sp.flipX = false;
            firePoint.localPosition = new Vector3(firePointX, firePoint.localPosition.y, 0);
        }
        else if (moveX < 0)
        {
            sp.flipX = true;
            firePoint.localPosition = new Vector3(-firePointX, firePoint.localPosition.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            EventManager.eventOnBonusFind();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Finish"))
        {
            EventManager.eventOnPlayerWin();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.CompareTag("Enemy") && !damaged)||(collision.gameObject.CompareTag("Bomb") && !damaged))
        {
            damagedTimer = damageTime;
            damaged = true;
            sp.color = new Color(1, 0.5f, 0.5f, 0.5f);
            EventManager.eventOnHealthDecrease();
        }
        if (collision.gameObject.CompareTag("Hole"))
        {
            EventManager.eventOnPlayerDead();
            
        }
        if (collision.gameObject.name.Equals("movePlatform"))
        {
            this.transform.parent = collision.transform;
        }
        else
        {
            this.transform.SetParent(null);
        }
    }

   

}
