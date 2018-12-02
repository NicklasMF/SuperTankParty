using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlayerSelection : MonoBehaviour {

    public bool isSelected;

    [SerializeField] Transform spriteOverlayer;
    [SerializeField] Image spriteUpper;
    int playerIndex;

    Button button;

    void Start() {
        Setup();
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);

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
    }
}
