using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultController : MonoBehaviour {

    [SerializeField] Transform playerParent;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Text headerTxt;

    public void EndRound() {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndRound();
    }

    public void ShowResults(List<GameObject> players) {
        foreach(Transform child in playerParent) {
            Destroy(child.gameObject);
        }
        int winCondition = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().winCondition;
        bool isDone = false;
        foreach (GameObject player in players) {
            GameObject go = Instantiate(playerPrefab, playerParent);
            go.GetComponent<UIResultPlayer>().Setup(player.GetComponent<Player>().color, player.GetComponent<Player>().points);
            if (player.GetComponent<Player>().points >= winCondition) {
                isDone = true;
            }
        }
        headerTxt.text = isDone ? "We have a winner!" : "First to " + winCondition + " wins";

        GetComponent<Animator>().SetTrigger("Show");
    }

}
