using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IEnemy
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject miniBombPrafab;
    [SerializeField] private float playerDistanceForShut;
    [SerializeField] private float playerDistanceForTurbo;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float forceMiniBomb;
    [SerializeField] public SpriteRenderer Sprite;
    [SerializeField] private AudioClip enemyBrokenClip;
    private Rigidbody2D rb;
    private bool forward;
    private float attackTimer;
    public float Speed;
    private float enemyHP = 100;
    private bool enemyDead=false;
    private bool AttackModeOn=false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackTimer = 0f;
    }

    void Update()
    {
        if (!enemyDead)
        {
            if (AttackModeOn)
            {
                if (CheckPlayerPosition()==1)
                {
                    Shut();
                }
                else 
                {
                    ChangeSpeed(CheckPlayerPosition());
                }
                
            }
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AttackModeOn = true;
        }
        if(other.CompareTag("Wall"))
        {
            forward = !forward;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            gameObject.GetComponentInChildren<ParticleSystem>().transform.localScale = new Vector3(-gameObject.GetComponentInChildren<ParticleSystem>().transform.localScale.x, 1, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AttackModeOn = false;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suricane"))
        {
            if (!enemyDead)
            {
                enemyHP -= 10;
                if (enemyHP <= 0)
                {
                    Break();
                }
            }
        }
       
    }

    public byte CheckPlayerPosition()
    {
        byte result=0;
        if (Vector2.Distance(GetComponent<Transform>().position, player.position) < playerDistanceForShut & player.position.y > firePoint.position.y)
        {
            result = 1;
        }
        if (Vector2.Distance(GetComponent<Transform>().position, player.position) < playerDistanceForTurbo & player.position.y < firePoint.position.y)
        {
            result = 2;
        }
        
        return result;
    }
    

public void Move()
    {
        Vector2 velo = Vector2.right * Speed;

        if (!forward)
            velo = Vector2.left * Speed;

        velo.y = rb.velocity.y;
        rb.velocity = velo;
    }

    public void Shut()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
           
        }
        else
        {
            attackTimer = 1.2f;
            MiniBomb miniBomb = Instantiate(miniBombPrafab, firePoint.position, Quaternion.identity).GetComponent<MiniBomb>();
            Vector3 vectorToPlayer = player.position - firePoint.position;
            miniBomb.SetForce(vectorToPlayer.normalized * forceMiniBomb);
        }
    }
    public void ChangeSpeed(byte indexSpeed)
    {
        if (indexSpeed == 2)
        {
            Speed = 15f;
        }
        else
        {
            Speed = 5f;
        }
        
    }
    public void Break()
    {
        enemyDead = true;
        gameObject.GetComponent<Animator>().enabled = false;
        Sprite.transform.localPosition = new Vector2(Sprite.transform.localPosition.x, -0.8f);
        gameObject.GetComponentInChildren<AudioSource>().clip = enemyBrokenClip;
        gameObject.GetComponentInChildren<AudioSource>().volume = 0.2f;
        gameObject.GetComponentInChildren<AudioSource>().Play();
        var emissionParticle = gameObject.GetComponentInChildren<ParticleSystem>().emission;
        emissionParticle.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rb.velocity = Vector3.zero;
        gameObject.layer = 3;
        gameObject.tag = "Untagged";
        rb.isKinematic = true;
    }
}
