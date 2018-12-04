using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] Transform playerControls4;
    [SerializeField] GameObject textWrapper; 

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void SetupControls(List<GameObject> players) {
        for (int i = 0; i < 4; i++) {
            bool added = false;
            foreach (GameObject player in players) {
                if (player.GetComponent<Player>().index == i) {
                    playerControls4.GetChild(i).GetComponent<UIPlayerControls>().SetupButtons(player);
                    added = true;
                    break;
                }
            }
            if (!added) {
                playerControls4.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ShowText(string _text) {
        textWrapper.GetComponentInChildren<Text>().text = _text;
        textWrapper.GetComponent<Animator>().SetTrigger("Show");
    }
}
