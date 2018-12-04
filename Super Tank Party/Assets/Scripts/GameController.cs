using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("Debug")]
    public bool isDebugging;
    [Range(1,4)] public int playerCount = 2;
    public KeyCode[] keys;

    [Header("General Settings")]
    public Color[] playerColors = new Color[4];
    public List<GameObject> players = new List<GameObject>();

    [Header("Game Settings")]
    public bool gameStarted;
    public int life = 100;
    public float speedStandard = 15f;
    public float rotateSpeed = 120f;
    public float bulletsRecharge = 1f;
    public int winCondition = 3;


    [Header("Prefabs and Stuff")]
    public GameObject playerPrefab;
    public GameObject bulletPrefab;
    public Transform bulletParent;

	void Awake () {
        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerDead(GameObject _object) {
        _object.SetActive(false);
        StartCoroutine(CheckForPlayersAlive());
    }

    IEnumerator CheckForPlayersAlive() {
        yield return new WaitForSeconds(2f);

        List<GameObject> playersAlive = new List<GameObject>();
        foreach(GameObject player in players) {
            if (player.GetComponent<PlayerController>().dead) {
                playersAlive.Add(player);
            }
        }
        if (playersAlive.Count == 0) {
            print("Draw");
            StartRound();
        } else if (playersAlive.Count == 1) {
            if (gameStarted) {
                players[0].GetComponent<Player>().points++;
                if (players[0].GetComponent<Player>().points >= winCondition) {
                    GetComponent<ScreenController>().EndGame();
                } else {
                    StartRound();
                }
            } else {
                GetComponent<ScreenController>().StartGame();
            }
        }
    }

    public void StartRound() {
        Transform positionParent = GameObject.FindGameObjectWithTag("StartingPositions").transform;
        foreach (GameObject player in players) {
            player.transform.position = positionParent.GetChild(player.GetComponent<Player>().index).position;
            player.transform.rotation = positionParent.GetChild(player.GetComponent<Player>().index).rotation;
            player.GetComponent<PlayerController>().SetupGame();
        }
        GetComponent<ScreenController>().uiController.ShowText("GO GO GO!");
    }
}
