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
    public bool roundStarted;
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
        players.ForEach((player) => {
            if (player.GetComponent<PlayerController>().dead) {
                playersAlive.Add(player);
            }
        });
        if (playersAlive.Count == 0) {
            // Display something about a draw
            PrepareNewRound();
        } else if (playersAlive.Count == 1) {
            players.ForEach((player) => {
                player.SetActive(false);
            });
            if (gameStarted) {
                players[0].GetComponent<Player>().points++;
                GetComponent<ScreenController>().ShowResults();
            } else {
                if (!gameStarted) {
                    GetComponent<ScreenController>().uiController.HideText();
                    gameStarted = true;
                }
                EndRound();
            }
        }
    }

    public void EndRound() {
        bool endGame = false;
        foreach(GameObject player in players) {
            if (player.GetComponent<Player>().points >= winCondition) {
                GetComponent<ScreenController>().EndGame();
                endGame = true;
            }
        }
        if (!endGame) {
            PrepareNewRound();
        }
    }

    public void PrepareNewRound() {
        roundStarted = false;
        Transform positionParent = GameObject.FindGameObjectWithTag("StartingPositions").transform;
        players.ForEach((player) => {
            player.transform.position = positionParent.GetChild(player.GetComponent<Player>().index).position;
            player.transform.rotation = positionParent.GetChild(player.GetComponent<Player>().index).rotation;
            player.GetComponent<PlayerController>().SetupGame();

        });
        StartCoroutine(CountdownToStartRound());
    }

    IEnumerator CountdownToStartRound() {
        yield return new WaitForSeconds(1f);

        if (gameStarted) {
            GetComponent<ScreenController>().uiController.ShowText("GO GO GO!");
        } else {
            GetComponent<ScreenController>().uiController.ShowText("Kill to begin", true);
        }
        roundStarted = true;
        players.ForEach((player) => {
            player.GetComponent<PlayerController>().canMove = true;
        });
    }
}
