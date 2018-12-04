using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerControls : MonoBehaviour {

    [SerializeField] Button rotateButton;
    [SerializeField] Button shootButton;

    public void SetupButtons(GameObject player) {
        ColorBlock newColor = rotateButton.colors;
        newColor.normalColor = player.GetComponent<Player>().color;
        rotateButton.colors = newColor;
        shootButton.colors = newColor;
        rotateButton.GetComponent<UIRotateButton>().player = player.GetComponent<PlayerController>();
        shootButton.onClick.AddListener(delegate {
            player.GetComponent<PlayerController>().Shoot();
        });

    }

}
