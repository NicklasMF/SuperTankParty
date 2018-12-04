using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRotateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [HideInInspector] public PlayerController player;

    bool pressed;

    void FixedUpdate() {
        if (pressed) {
            player.Rotate();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        pressed = false;
    }
}
