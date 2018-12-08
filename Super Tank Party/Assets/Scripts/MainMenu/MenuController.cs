using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] Transform menuMain;
    public Transform menuPlayerSelection;

    void Awake() {
        ShowMenu();
    }

    public void ShowMenu() {
        HideAll();
        menuMain.gameObject.SetActive(true);
    }

    public void ShowPlayerSelection() {
        HideAll();
        menuPlayerSelection.gameObject.SetActive(true);
    }

    void HideAll() {
        menuMain.gameObject.SetActive(false);
        menuPlayerSelection.gameObject.SetActive(false);
    }
}
