using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    GameController gameController;

    //Debugging
    KeyCode keyShooting;
    KeyCode keyRotating;

    // Basics
    int life;
    float speed;
    float rotateSpeed;
    bool rotateRight;
    public bool dead;


    // Shooting
    int maxBulletCount = 1;
    int bulletCount;
    float timeUntilNewBullet;
    float rechargeTime;

    [Header("Prefabs")]
    [SerializeField] GameObject graphics;
    public SpriteRenderer graphicColor;

    GameObject bulletPrefab;
    Transform bulletParent;

    void Awake() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start () {
        Init();
        SetupGame();
	}

    public void SetupKeys(KeyCode _rotate, KeyCode _shoot) {
        keyRotating = _rotate;
        keyShooting = _shoot;
    }

    void Init() {
        bulletPrefab = gameController.bulletPrefab;
        bulletParent = gameController.bulletParent;
    }

    public void SetupGame() {
        gameObject.SetActive(true);
        dead = false;
        life = gameController.life;
        bulletCount = maxBulletCount;
        rechargeTime = gameController.bulletsRecharge;
        timeUntilNewBullet = rechargeTime;
        speed = gameController.speedStandard;
        rotateSpeed = gameController.rotateSpeed;
        rotateRight = true;
    }
	
	void FixedUpdate () {

        Move();
        RechargeShoot();

        if (Input.GetKey(keyRotating)) {
            Rotate();
        }

        if (Input.GetKeyDown(keyShooting)) {
            Shoot();
        }
	}

    void Move() {
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
    }

    public void Rotate() {
        if (rotateRight) {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        } else {
            transform.Rotate(0, 0,rotateSpeed * Time.deltaTime);
        }
    }

    public void Shoot() {
        if (bulletCount > 0) {
            bulletCount--;
            GameObject bullet = Instantiate(bulletPrefab, bulletParent);
            bullet.transform.position = transform.position + (transform.rotation * new Vector3(0, 2.02f));
            bullet.transform.rotation = transform.rotation;
        }
    }

    void RechargeShoot() {
        if (bulletCount < maxBulletCount) {
            if (timeUntilNewBullet < 0) {
                bulletCount++;
                timeUntilNewBullet = rechargeTime;
            } else {
                timeUntilNewBullet -= Time.deltaTime;
            }
        }
    }

    public void Hit(int _damage) {
        life -= _damage;

        if (life <= 0) {
            Die();
        }
    }

    void Die() {
        dead = true;
        gameController.PlayerDead(gameObject);
    }
}
