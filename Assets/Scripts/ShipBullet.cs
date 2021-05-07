using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    [SerializeField] private float fireSpeed = 10f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * fireSpeed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
