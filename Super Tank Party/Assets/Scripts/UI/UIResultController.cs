using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultController : MonoBehaviour {

    [SerializeField] Transform playerParent;
    [SerializeField] GameObject playerPrefab;

    public void EndRound() {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndRound();
    }

    public void ShowResults(List<GameObject> players) {
        foreach(Transform child in playerParent) {
            Destroy(child.gameObject);
        }
        foreach(GameObject player in players) {
            GameObject go = Instantiate(playerPrefab, playerParent);
            go.GetComponent<UIResultPlayer>().Setup(player.GetComponent<Player>().color, player.GetComponent<Player>().points);
        }
        GetComponent<Animator>().SetTrigger("Show");
    }

}
