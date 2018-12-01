using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("General Settings")]
    public Color[] playerColors = new Color[4];

    [Header("Game Settings")]
    public int life = 100;
    public float speedStandard = 15f;
    public float rotateSpeed = 120f;
    public float bulletsRecharge = 1f;


    [Header("Prefabs and Stuff")]
    [SerializeField] GameObject playerPrefab;
    public GameObject bulletPrefab;
    public Transform bulletParent;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
