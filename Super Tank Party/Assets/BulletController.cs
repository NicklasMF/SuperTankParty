using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 14f;

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Player hit");
        }
    }
}
