using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 60f;
    public int damage = 100;

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerController>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
