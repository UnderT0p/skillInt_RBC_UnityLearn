using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBomb : MonoBehaviour
{
    private float RotationSpeed;
    private Vector2 StartSpeed;
    private Rigidbody2D rb;

    void Awake() //метод инициализации всех скриптов, заранее.
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    public void SetForce(Vector3 force)
    {
        
        StartSpeed = force; // преобразование Vector3 in Vector2.
        
        rb.AddForce(StartSpeed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suricane"))
        {
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Enviroment"))
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
