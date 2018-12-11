using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 50f;
    public int damage = 100;
    [HideInInspector] public GameObject sender;


    void FixedUpdate() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            Destroy(gameObject);
        } else if (coll.gameObject.CompareTag("Player") && coll.gameObject != sender) {
            coll.gameObject.GetComponent<PlayerController>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
