using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("Debug")]
    public KeyCode[] keys;

    [Header("General Settings")]
    public Color[] playerColors = new Color[4];
    public List<GameObject> players = new List<GameObject>();

    [Header("Game Settings")]
    public int life = 100;
    public float speedStandard = 15f;
    public float rotateSpeed = 120f;
    public float bulletsRecharge = 1f;


    [Header("Prefabs and Stuff")]
    public GameObject playerPrefab;
    public GameObject bulletPrefab;
    public Transform bulletParent;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
