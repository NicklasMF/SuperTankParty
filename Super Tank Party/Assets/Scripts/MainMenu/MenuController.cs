using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] Transform menuMain;
    public Transform menuPlayerSelection;
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
        ShowMenu();
    }

    public void ShowMenu() {
        HideAll();
        animator.SetBool("ShowMainMenu", true);
    }

    public void ShowPlayerSelection() {
        HideAll();
        animator.SetBool("ShowSelectPlayers", true);
    }

    public void StartGame() {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ScreenController>().StartGame();
    }

    void HideAll() {
        animator.SetBool("ShowSelectPlayers", false);
        animator.SetBool("ShowMainMenu", false);
    }
}
