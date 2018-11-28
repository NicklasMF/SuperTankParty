using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 3f;
    public float rotateSpeed = 60f;
    public bool rotateRight = true;

    public GameObject bulletPrefab;
    public Transform bulletParent;

	void Start () {
		
	}
	
	void FixedUpdate () {

        //Move();

        if (Input.GetKey(KeyCode.L)) {
            Turn();
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            Shoot();
        }
	}

    void Move() {
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
    }

    void Turn() {
        if (rotateRight) {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        } else {
            transform.Rotate(0, 0,rotateSpeed * Time.deltaTime);
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, bulletParent);
        bullet.transform.position = transform.position + (transform.rotation * new Vector3(0,2.02f));
        bullet.transform.rotation = transform.rotation;
    }
}
