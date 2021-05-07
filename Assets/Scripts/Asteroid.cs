using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player Bullet")
        {
            collision.collider.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
