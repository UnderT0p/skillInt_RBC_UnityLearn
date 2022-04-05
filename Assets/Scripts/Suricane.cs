using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suricane : MonoBehaviour
{
    private float RotationSpeed;
    private Vector2 StartSpeed;
    private Rigidbody2D rb;

    void Awake() //метод инициализации всех скриптов, заранее.
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.5f);
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
        if (!collision.gameObject.CompareTag("Player")&& !collision.gameObject.CompareTag("Suricane"))
        {
            rb.velocity = Vector2.zero;
            rb.freezeRotation = true;
            rb.isKinematic = true;
        }
        if (collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        
    }

}
