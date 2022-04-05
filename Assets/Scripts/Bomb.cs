using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float RotationSpeed;
    private Vector2 StartSpeed;
    private Rigidbody2D rb;

    void Awake() //метод инициализации всех скриптов, заранее.
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    public void SetForceAndRotation(Vector3 force, float rot)
    {
        RotationSpeed = rot;
        StartSpeed = force;
        rb.AddTorque(RotationSpeed);
        rb.AddForce(StartSpeed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suricane"))
        {
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Enviroment")&& !collision.gameObject.CompareTag("Enemy"))
        {
            Explouse();
        }
    }
    public void Explouse()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        EventManager.eventOnBombExplouse();
        Destroy(gameObject, 1f);
    }
}
