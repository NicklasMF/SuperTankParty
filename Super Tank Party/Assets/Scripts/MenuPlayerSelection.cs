using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayerSelection : MonoBehaviour {

    [SerializeField] Button startButton;
    public Transform playerWrapper;

    void Awake() {
        CheckIfMinimumTwoPlayers();
    }

    public void CheckIfMinimumTwoPlayers() {
        int i = 0;
        foreach(Transform child in playerWrapper) {
            if (child.GetComponent<MenuPlayerSelectionPlayer>().isSelected) {
                i++;
            }
        }
        startButton.interactable = i > 1;
    }
}
