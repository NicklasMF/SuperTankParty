using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayerSelectionPlayer : MonoBehaviour {

    MenuController menuController;

    public bool isSelected;

    [SerializeField] Transform spriteOverlayer;
    [SerializeField] Image spriteUpper;
    int playerIndex;

    Button button;

    void Start() {
        Setup();
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
        menuController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuController>();
    }

    void Setup() {
        playerIndex = transform.GetSiblingIndex();
        isSelected = false;
        spriteOverlayer.gameObject.SetActive(!isSelected);
        spriteUpper.color = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().playerColors[playerIndex];
    }

    void Select() {
        isSelected = !isSelected;

        spriteOverlayer.gameObject.SetActive(!isSelected);
        menuController.menuPlayerSelection.GetComponent<MenuPlayerSelection>().CheckIfMinimumTwoPlayers();
    }
}
