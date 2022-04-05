using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitleCar : MonoBehaviour
{
    
    [SerializeField] GameObject Boss;
    [SerializeField] private GameObject miniBombPrafab;
    [SerializeField] private float forceMiniBomb;
    [SerializeField] private Transform firePoint;
    private Rigidbody2D rb;
    private bool forward;
    public float Speed;
    public Animator Anim;
    public SpriteRenderer Sprite;
    private float attackTimer = 1f;
    private GameObject player;
    private bool AttackModeOn=false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (AttackModeOn)
        {
            Shut();
        }
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player=collision.gameObject;
            AttackModeOn = true;
        }
        if (collision.CompareTag("Wall"))
        {
            forward = !forward;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AttackModeOn = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suricane"))
        {
            Destroy(gameObject);
        }

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
            attackTimer = 1f;
            MiniBomb miniBomb = Instantiate(miniBombPrafab, firePoint.position, Quaternion.identity).GetComponent<MiniBomb>();
            Vector3 vectorToPlayer = player.transform.position - firePoint.position;
            miniBomb.SetForce(vectorToPlayer.normalized * forceMiniBomb);
        }
    }

}
