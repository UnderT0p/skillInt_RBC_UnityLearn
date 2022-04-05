using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour,IEnemy
{
    [SerializeField] private float forceBomb;
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bomb;
    [SerializeField] private float HPBoss;
    [SerializeField] private GameObject car;
    [SerializeField] private TextMeshProUGUI bossHealth;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] public GameObject WinMenuUi;
    [SerializeField] public SpriteRenderer Sprite;
    [SerializeField] private AudioClip enemyBrokenClip;
    [SerializeField] private float playerDistanceForShut;
    private bool AttackModeOn = false;

    private float attackTimer;
    private Color firstColor;
    LitleCar litleCar;
    private float hpBoss;
    private bool litleCarAcses=false;
    private bool enemyDead = false;

    // Start is called before the first frame update
    void Start()
    {
        hpBoss = HPBoss;
        attackTimer = 1.3f;
        bossHealth.text = "HP: " + hpBoss;
        firstColor = sp.color;
        litleCarAcses = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyDead)
        {
            if (AttackModeOn)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    sp.color = firstColor;
                    attackTimer = 1.3f;
                    if (CheckPlayerPosition() == 1)
                    {
                        Shut();
                    }
                    if ( litleCarAcses )
                    {
                        litleCar = Instantiate(car, firePoint.position, Quaternion.identity).GetComponent<LitleCar>();
                        litleCarAcses = false;
                    }
                    if (litleCar == null)
                    {
                        litleCarAcses = true;
                    }
                }
                
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AttackModeOn = true;
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
        if (collision.gameObject.CompareTag("Suricane")&& !enemyDead)
        {
            Destroy(collision.gameObject);
            sp.color = new Color(1, 0.5f, 0.5f, 0.5f);
            hpBoss -= 10;
            bossHealth.text = "HP: " + hpBoss;

            if (hpBoss <= 0)
            {

                Break();
            }

        }
    }

    public byte CheckPlayerPosition()
    {
        byte result = 0;
        if (Vector2.Distance(GetComponent<Transform>().position, player.position) < playerDistanceForShut & player.position.y > firePoint.position.y)
        {
            result = 1;
        }
        return result;
    }

    public void Move()
    {
       
    }

    public void Shut()
    {
        Bomb bomb1 = Instantiate(bomb, firePoint.position, Quaternion.identity).GetComponent<Bomb>();
        Vector3 force1 = player.position - firePoint.position;
        bomb1.SetForceAndRotation(force1 * forceBomb, 0.2f);
    }

    public void ChangeSpeed(byte speed)
    {
       
    }

    public void Break()
    {
        enemyDead = true;
        gameObject.GetComponent<Animator>().enabled = false;
        Sprite.transform.localPosition = new Vector2(Sprite.transform.localPosition.x, -0.8f);
        gameObject.GetComponentInChildren<AudioSource>().clip = enemyBrokenClip;
        gameObject.GetComponentInChildren<AudioSource>().volume = 0.2f;
        gameObject.GetComponentInChildren<AudioSource>().loop = false;
        gameObject.GetComponentInChildren<AudioSource>().Play();
        gameObject.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(gameObject, 1.5f);
    }

   
}
