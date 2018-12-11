using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlaceFingers : MonoBehaviour {

    ScreenController controller;
    void Start() {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScreenController>();
    }

    public void StartGame() {
        controller.StartGame();
    }
}
