using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] GameObject intro;
    [SerializeField] Transform playerControls4;
    [SerializeField] GameObject textWrapper;
    [SerializeField] GameObject resultWrapper;

    [HideInInspector] public bool showIntro;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (!showIntro) {
            Destroy(intro.gameObject);
        }
        foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
        }
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

    public void ShowResults(List<GameObject> _players) {
        resultWrapper.GetComponent<UIResultController>().ShowResults(_players);
    }

    public void ShowText(string _text, bool _keep = false) {
        textWrapper.GetComponentInChildren<Text>().text = _text;
        if (_keep) {
            textWrapper.GetComponent<Animator>().SetBool("Show", true);
        } else {
            textWrapper.GetComponent<Animator>().SetTrigger("TriggerShow");
        }
    }

    public void HideText() {
        textWrapper.GetComponent<Animator>().SetBool("Show", false);
    }
}
